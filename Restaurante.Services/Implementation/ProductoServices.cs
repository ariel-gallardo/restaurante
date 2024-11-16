using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurante.DAO;
using Restaurante.Models;
using System.Linq.Expressions;

namespace Restaurante.Services
{
    public class ProductoServices : IProductoServices
    {
        private readonly IMapper _mappper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductoServices(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mappper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ResultResponse> Crear(CrearProductoDTO dTO)
        {
            var result = new ResultResponse();
            var newProduct = _mappper.Map<CrearProductoDTO, Producto>(dTO);
            if(await _unitOfWork.Producto.Where(x => EF.Functions.Like(x.Nombre, dTO.Nombre) && x.DeletedAt == null).CountAsync() < 1)
            {
                await _unitOfWork.Producto.Insert(newProduct);
                await _unitOfWork.ProductoIngrediente.Insert(newProduct.Ingredientes);
                await _unitOfWork.SaveChangesAsync();
                result.Message = $@"ENTITY_CREATED ""PRODUCT,{newProduct.Nombre}""";
                result.StatusCode = 200;
            }
            else
            {
                result.Message = $@"ENTITY_EXISTS ""PRODUCT,{newProduct.Nombre}""";
                result.StatusCode = 400;
            }
            return result;
        }

        public async Task<ResultResponse> Editar(EditarProductoDTO dTO)
        {
            var result = new ResultResponse();
            var srcProduct = _mappper.Map<EditarProductoDTO, Producto>(dTO);
            var currentProduct = _unitOfWork.Producto.WhereActive(x => x.Id == dTO.ProductoId && x.DeletedAt == null).Take(1).FirstOrDefault();
            if(currentProduct != null)
            {
                var newOptions = _mappper.Map(srcProduct, currentProduct);
                _unitOfWork.Producto.Update(newOptions);
                _unitOfWork.ProductoIngrediente.Update(newOptions.Ingredientes.Where(x => currentProduct.Ingredientes.Contains(x)));
                _unitOfWork.ProductoIngrediente.Delete(currentProduct.Ingredientes.Where(x => !newOptions.Ingredientes.Contains(x) && x.CreatedAt != null));
                await _unitOfWork.ProductoIngrediente.Insert(newOptions.Ingredientes.Where(x => !currentProduct.Ingredientes.Contains(x) && x.CreatedAt == null));
                await _unitOfWork.SaveChangesAsync();
                
            }
            else
            {
                result.Message = $@"ENTITY_NOT_EXISTS ""PRODUCT,{dTO.Nombre}""";
                result.StatusCode = 400;
            }
            return result;
        }

        public async Task<ResultResponse> Eliminar(string productId)
        {
            var result = new ResultResponse();

            if (_unitOfWork.Producto.ExistsActive(productId))
            {
                var currentProduct = _unitOfWork.Producto.WhereActive(x => x.Id == productId).FirstOrDefault();
                _unitOfWork.Producto.Delete(currentProduct);
                await _unitOfWork.SaveChangesAsync();
                result.Message = $@"ENTITY_DELETED ""PRODUCT,{currentProduct.Nombre}""";
                result.StatusCode = 400;
            }
            else
            {
                result.Message = $@"ENTITY_NOT_EXISTS_ID ""PRODUCT,{productId}""";
                result.StatusCode = 400;
            }

            return result;
        }

        public async Task<ResultResponse> Listar(Expression<Func<Producto, bool>> whereExpression, int page)
        {
            var result = new ResultResponse();
            return result;
        }

        public async Task<ResultResponse> Restaurar(string productId)
        {
            var result = new ResultResponse();
            return result;
        }
    }
}
