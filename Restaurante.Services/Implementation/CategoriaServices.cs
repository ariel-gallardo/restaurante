using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Restaurante.DAO;
using Restaurante.Migrations.Migrations;
using Restaurante.Models;

namespace Restaurante.Services
{
    public class CategoriaServices : ICategoriaServices
    {
        private readonly ICategoriaRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CategoriaServices(ICategoriaRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultResponse> Crear(CrearCategoriaDTO dTO)
        {
            var result = new ResultResponse();
            Categoria newCategory = _mapper.Map<CrearCategoriaDTO, Categoria>(dTO);
            newCategory = await _unitOfWork.Categoria.Crear(newCategory);
            if (newCategory != null) 
            {
                result.Message = $@"ENTITY_CREATED ""CATEGORY,{newCategory.Nombre}""";
                result.StatusCode = 200;
            }
            else
            {
                result.Message = $@"ENTITY_EXISTS ""CATEGORY,{dTO.Nombre}""";
                result.StatusCode = 400;
            }
            return result;
        }

        public async Task<ResultResponse> Editar(EditarCategoriaDTO dTO)
        {
            var result = new ResultResponse();
            var srcCategory = _mapper.Map<EditarCategoriaDTO, Categoria>(dTO);
            var currentCategory = await _unitOfWork.Categoria.WhereActive(x => x.Id == dTO.CategoriaId).Take(1).FirstOrDefaultAsync();
            var editCategory = await _unitOfWork.Categoria.EditarCategory(currentCategory, _mapper.Map(srcCategory, currentCategory));
            if (editCategory)
            {
                result.Message = $@"ENTITY_UPDATED_SUCESSFULLY ""CATEGORY,{dTO.Nombre}""";
                result.StatusCode = 200;
            }
            else
            {
                result.Message = $@"ENTITY_NOT_EXISTS ""CATEGORY,{dTO.Nombre}""";
                result.StatusCode = 400;
            }
            return result;
        }

        public async Task<ResultResponse> Eliminar(long categoriaId)
        {
            var result = new ResultResponse();
            if (_repository.ExistsActive(categoriaId))
            {
                var cat = await _repository.WhereActive(x => x.Id == categoriaId).FirstOrDefaultAsync();
                _repository.Delete(cat);
                result.Message = $@"ENTITY_DELETED ""CATEGORY,{cat.Nombre}""";
                result.StatusCode = 200;
            }
            else
            {
                result.Message = $@"ENTITY_NOT_EXISTS_ID ""CATEGORY,{categoriaId}""";
                result.StatusCode = 400;
            }
            return result;
        }

        public async Task<ResultResponse> Listar(int paginaNum = 1, bool ascendente = true, string nombreClave = "", string catPadreId = "")
        {
            var result = new ResultResponse();
            var content = await _repository.ListarCategorias(paginaNum, ascendente, nombreClave,catPadreId);
            result.Content = content;
            result.Message = content.CantidadActual > 0 ? $@"ENTITY_HAS_DATA ""CATEGORIES""" : $@"EMPTY_DATA ""CATEGORIES""";
            result.StatusCode = content.CantidadActual > 0 ? StatusCodes.Status200OK : StatusCodes.Status404NotFound;
            return result;
        }

        public async Task<ResultResponse> Restaurar(long categoriaId)
        {
            var result = new ResultResponse();
            var op = await _repository.Restore(categoriaId);
            result.Message =  op ? $@"ENTITY_RESTORED ""CATEGORY,{categoriaId}""" : $@"ENTITY_CANNOT_BE_RESTORED ""CATEGORY,{categoriaId}""";
            result.StatusCode = op ? StatusCodes.Status200OK : StatusCodes.Status404NotFound;
            return result;
        }
    }
}
