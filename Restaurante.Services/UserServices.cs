using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Restaurante.DAO;
using Restaurante.Hubs;
using Restaurante.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace Restaurante.Services
{
    public class UserServices : IUserServices
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordServices _passServices;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPedidoRepository _pRepository;
        private readonly IMessageServices _smsServices;
        private HubCallerContext _hubCallerContext;

        public UserServices(IMapper mapper, IUnitOfWork unitOfWork, IPasswordServices passServices, IHttpContextAccessor httpContextAccessor, IPedidoRepository pRepostiory, IMessageServices smsServices)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _passServices = passServices;
            _httpContextAccessor = httpContextAccessor;
            _pRepository = pRepostiory;
            _smsServices = smsServices;
        }

        public IEnumerable<Claim> CurrentUserClaims { 

            get {
                var httpContextUser = _httpContextAccessor?.HttpContext?.User;
                var hubCallerUser = _hubCallerContext?.User;
                return httpContextUser != null ? httpContextUser.Claims ?? new Claim[] { } 
                : hubCallerUser.Claims ?? new Claim[] {};
            } 
        }

        public T CurrentUserClaim<T>(string type){
            var claim = CurrentUserClaims.FirstOrDefault(x => x.Type == type);
            dynamic t = default(T);
            if (claim != null && !string.IsNullOrWhiteSpace(claim.Value))
            {
                try
                {
                    var baseType = typeof(T);
                    if (baseType.IsGenericType && baseType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        baseType = Nullable.GetUnderlyingType(baseType);
                    t = Convert.ChangeType(claim.Value, Type.GetTypeCode(baseType));
                }
                catch (Exception ex) 
                {
                    try
                    {
                        if (!t.GetType().IsPrimitive && t.GetType().IsClass)
                        {
                            t = JsonSerializer.Deserialize<T>(claim.Value);
                        }
                    }
                    catch (Exception ex2) { 

                    }
                }
            }
            return t;
        }

        public string CurrentRol
        => CurrentUserClaim<string>("Rol") ?? "Ninguno";

        public long? CurrentId
        => CurrentUserClaim<long?>("Id");

        public HubCallerContext HubContext => _hubCallerContext;
        public async Task<ResultResponse> Info(string token)
        {
            var result = new ResultResponse();
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = System.Text.Encoding.UTF8.GetBytes(Restaurante.Infraestructure.AppSettings.JWTSecretKey);
            tokenHandler.ValidateToken(token.Replace("Bearer ", string.Empty), new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out Microsoft.IdentityModel.Tokens.SecurityToken validatedToken);
            var tData = validatedToken as JwtSecurityToken;
            var claims = tData.Claims;
            var content = _mapper.Map<IEnumerable<Claim>, UserInfoDTO>(claims);
            content.Pedido = _mapper.Map<Pedido,PedidoDTO>(await _pRepository.PedidoActual(long.Parse(content.UsuarioId)));
            result.Content = content;
            result.StatusCode = 200;
            result.Message = "TOKEN_INFO";
            await _smsServices.SendOperation(result.Message);
            return result;
        }

        public async Task<ResultResponse> Login(LoginDTO dto)
        {
            var response = new ResultResponse();
            var usuario = await _unitOfWork.Usuario.SearchUserActiveByEmailWithDelivery(dto.Correo);
            var token = string.Empty;
            if(usuario != null)
            {
                if(_passServices.Ok(dto.Password, usuario.Password))
                {
                    response.StatusCode = 200;
                    var userInfo = _mapper.Map<Usuario, UserInfoDTO>(usuario);
                    (token,var expTime) = _passServices.GenerateToken(usuario);
                    userInfo.Token = $"Bearer {token}";
                    userInfo.CaducaEn = expTime;
                    userInfo.Rol = usuario.Rol.Descripcion;
                    response.Content = userInfo;
                    response.Message = $"USER_WELCOME {userInfo.NombreCompleto}";
                    await _smsServices.SendMessage(response.Message, response.StatusCode);
                }
                else
                {
                    response.StatusCode = 401;
                    response.Message = "INVALID_CREDENTIALS";
                    await _smsServices.SendMessage(response.Message, response.StatusCode);
                }
            }
            else
            {
                response.StatusCode = 401;
                response.Message = "INVALID_CREDENTIALS";
                await _smsServices.SendMessage(response.Message,response.StatusCode);
            }
            return response;
        }

        public async Task<ResultResponse> Register(RegisterDTO dto)
        {
            var result = new ResultResponse();
            var user = _mapper.Map<RegisterDTO, Usuario>(dto);
            user.Password = _passServices.Encrypt(user.Password);
            var userCreated = _unitOfWork.Usuario.SearchUserActiveByEmail(dto.Correo);
            if(userCreated == null)
            {
                await _unitOfWork.BeginTransactionAsync();
                await _unitOfWork.Usuario.Insert(user);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
                userCreated = _unitOfWork.Usuario.SearchUserActiveByEmail(dto.Correo);
                var userInfoDTO = _mapper.Map<Usuario, UserInfoDTO>(userCreated);
                result.Content = userInfoDTO;
                result.StatusCode = 201;
                result.Message = $"USER_CREATED_SUCCESSFULLY {dto.Correo}";
                await _smsServices.SendMessage(result.Message,result.StatusCode);
            }
            else
            {
                result.Content = dto;
                result.StatusCode = 400;
                result.Message = $"USER_ALREADY_EXISTS {dto.Correo}";
                await _smsServices.SendMessage(result.Message,result.StatusCode);
            }

            return result;
        }

        public void AddHubContext(HubCallerContext context)
        {
            _hubCallerContext = context;
        }
    }
}
