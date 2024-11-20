using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurante.DAO;
using Restaurante.Models;

namespace Restaurante.Services
{
    public class IngredienteServices : IIngredienteServices
    {
        private readonly IMapper _mappper;
        private readonly IUnitOfWork _unitOfWork;

        public IngredienteServices(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mappper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ResultResponse> Crear(CrearIngredienteDTO dTO)
        {
            var result = new ResultResponse();
            var newIngredient = _mappper.Map<CrearIngredienteDTO, Ingrediente>(dTO);
            
            if (!(await _unitOfWork.Ingrediente.WhereActive(x => EF.Functions.Like(x.Nombre,$"{newIngredient.Nombre}")).CountAsync() > 0))
            {
                await _unitOfWork.Ingrediente.Insert(newIngredient);
                result.Message = $@"ENTITY_CREATED ""INGREDIENT,{newIngredient.Nombre}""";
                result.StatusCode = 200;
            }
            else
            {
                result.Message = $@"ENTITY_EXISTS ""INGREDIENT,{newIngredient.Nombre}""";
                result.StatusCode = 400;
            }
            return result;
        }

        public async Task<ResultResponse> Editar(EditarIngredienteDTO dTO)
        {
            var result = new ResultResponse();
            var srcIngrediente = _mappper.Map<EditarIngredienteDTO, Ingrediente>(dTO);

            if (_unitOfWork.Ingrediente.ExistsActive(srcIngrediente.Id))
            {
                var crrIngrediente = _unitOfWork.Ingrediente.WhereActive(x => x.Id == srcIngrediente.Id).FirstOrDefault();
                crrIngrediente = _mappper.Map(srcIngrediente, crrIngrediente);
                result.Message = $@"ENTITY_UPDATED_SUCESSFULLY ""INGREDIENT,{crrIngrediente.Nombre}""";
                result.StatusCode = 200;
            }
            else
            {
                result.Message = $@"ENTITY_NOT_EXISTS ""INGREDIENT,{srcIngrediente.Id}""";
                result.StatusCode = 400;
            }
            return result;
        }

        public async Task<ResultResponse> Eliminar(string ingredienteId)
        {
            var result = new ResultResponse();

            if (_unitOfWork.Ingrediente.ExistsActive(ingredienteId))
            {
                var crrIngrediente = _unitOfWork.Ingrediente.WhereActive(x => x.Id == ingredienteId).FirstOrDefault();
                _unitOfWork.Ingrediente.Delete(crrIngrediente);
                await _unitOfWork.SaveChangesAsync();
                result.Message = $@"ENTITY_DELETED ""INGREDIENT,{crrIngrediente.Nombre}""";
                result.StatusCode = 400;
            }
            else
            {
                result.Message = $@"ENTITY_NOT_EXISTS_ID ""INGREDIENT,{ingredienteId}""";
                result.StatusCode = 400;
            }

            return result;
        }

        public async Task<ResultResponse> Listar(int? paginaNum = 1, string? productoId = "", string? ordenarPor = "", bool? ascendente = true, string? nombreClave = "", double? precioMin = 0, double? precioMax = 0)
        {
            var result = new ResultResponse();

            var resultData = await _unitOfWork.Ingrediente.ListarIngredientes(paginaNum, productoId, ordenarPor, ascendente, nombreClave, precioMin, precioMax);
            if (resultData.Total > 0)
                result.Content = resultData;
            result.StatusCode = 200;
            result.Message = resultData.Total > 0 ? @"ENTITY_HAS_DATA ""INGREDIENTS""" : $@"ENTITY_HAS_NOT_DATA ""INGREDIENTS""";
            return result;
        }

        public async Task<ResultResponse> Restaurar(string ingredienteId)
        {
            var result = new ResultResponse();
            if (await _unitOfWork.Ingrediente.Restore(ingredienteId))
            {
                await _unitOfWork.SaveChangesAsync();
                result.Content = await _unitOfWork.Ingrediente.WhereActive(x => x.Id == ingredienteId).FirstOrDefaultAsync();
                result.Message = $@"ENTITY_RESTORED ""INGREDIENT,{ingredienteId}""";
                result.StatusCode = 200;
            }
            else
            {
                result.Message = $@"ENTITY_CANNOT_BE_RESTORED ""INGREDIENT,{ingredienteId}""";
                result.StatusCode = 404;
            }
            return result;
        }
    }
}
