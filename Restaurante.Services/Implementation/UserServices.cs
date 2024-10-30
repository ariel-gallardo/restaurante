using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Restaurante.DAO;
using Restaurante.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Restaurante.Services
{
    public class UserServices : IUserServices
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordServices _passServices;

        public UserServices(IMapper mapper, IUnitOfWork unitOfWork, IPasswordServices passServices)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _passServices = passServices;
        }

        public async Task<ResultResponse> Info(string token)
        {
            var result = new ResultResponse();
            var tData = new JwtSecurityToken(token.Replace("Bearer ",string.Empty));
            var claims = tData.Claims;
            result.Content = _mapper.Map<IEnumerable<Claim>, UserInfoDTO>(claims);
            result.StatusCode = 200;
            result.Message = "TOKEN_INFO";
            return result;
        }

        public async Task<ResultResponse> Login(LoginDTO dto)
        {
            var response = new ResultResponse();
            var usuario = _unitOfWork.Usuario.SearchUserActiveByEmail(dto.Correo);
            var token = string.Empty;
            if(usuario != null)
            {
                if(_passServices.Ok(dto.Password, usuario.Password))
                {
                    response.StatusCode = 200;
                    var userInfo = _mapper.Map<Usuario, UserInfoDTO>(usuario);
                    (token,var expTime) = _passServices.GenerateToken(usuario);
                    userInfo.CaducaEn = expTime;
                    response.Content = (userInfo,token);
                    response.Message = $"USER_WELCOME {response.Content.Item1.NombreCompleto}";
                }
                else
                {
                    response.StatusCode = 401;
                    response.Message = $"USER_WRONG_PASSWORD {dto.Correo}";
                }
            }
            else
            {
                response.StatusCode = 401;
                response.Message = $"USER_EMAIL_NOT_FOUND {dto.Correo}";
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
                await _unitOfWork.Usuario.Repository.Insert(user);
                await _unitOfWork.SaveChangesAsync();
                userCreated = _unitOfWork.Usuario.SearchUserActiveByEmail(dto.Correo);
                var userInfoDTO = _mapper.Map<Usuario, UserInfoDTO>(userCreated);
                result.Content = userInfoDTO;
                result.StatusCode = 201;
                result.Message = $"USER_CREATED_SUCCESSFULLY {dto.Correo}";
            }
            else
            {
                result.Content = dto;
                result.StatusCode = 400;
                result.Message = $"USER_ALREADY_EXISTS {dto.Correo}";
            }

            return result;
        }

    }
}
