using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurante.DAO;
using Restaurante.Infraestructure;
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

        public Task<ResultResponse> Consumir(IList<ConsumirProductoDTO> dto)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultResponse> Crear(CrearProductoDTO dTO)
        {
            var result = new ResultResponse();
            var newProduct = _mappper.Map<CrearProductoDTO, Producto>(dTO);
            newProduct = await _unitOfWork.Producto.CrearProducto(newProduct);
            if(newProduct != null)
            {
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
            var editProduct = await _unitOfWork.Producto.EditarProducto(currentProduct, _mappper.Map(srcProduct, currentProduct));
            if(editProduct)
            {
                result.Message = $@"ENTITY_UPDATED_SUCESSFULLY ""PRODUCT,{dTO.Nombre}""";
                result.StatusCode = 200;
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

        public async Task<ResultResponse> Listar(int? paginaNum = 1, string? ordenarPor = "", bool? ascendente = true, string? nombreClave = "", double? precioMin = 0.0, double? precioMax = 0.0)
        {
            var result = new ResultResponse();

            var resultData = await _unitOfWork.Producto.ListarProductos(paginaNum, ordenarPor, ascendente, nombreClave, precioMin, precioMax);
            if(resultData.Total > 0)
                result.Content = resultData;
            result.StatusCode = 200;
            result.Message = resultData.Total > 0 ? @"ENTITY_HAS_DATA ""PRODUCTS""" : $@"ENTITY_HAS_NOT_DATA ""PRODUCTS""";
            return result;
        }

        public async Task<ResultResponse> Restaurar(string productId)
        {
            var result = new ResultResponse();
            if (await _unitOfWork.Producto.Restore(productId))
            {
                await _unitOfWork.SaveChangesAsync();
                result.Content = await _unitOfWork.Producto.WhereActive(x => x.Id == productId).FirstOrDefaultAsync();
                result.Message = $@"ENTITY_RESTORED ""PRODUCTS,{productId}""";
                result.StatusCode = 200;
            }
            else
            {
                result.Message = $@"ENTITY_CANNOT_BE_RESTORED ""PRODUCTS,{productId}""";
                result.StatusCode = 404;
            }
            return result;
        }
    }
}
