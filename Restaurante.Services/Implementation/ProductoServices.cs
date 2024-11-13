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
            if(await _unitOfWork.Producto.Where(x => x.Nombre.ToLowerInvariant() == dTO.Nombre.ToLowerInvariant()).CountAsync() < 1)
            {
                await _unitOfWork.Producto.Insert(newProduct);
                if(newProduct.Ingredientes != null)
                {
                    foreach (var i in newProduct.Ingredientes)
                    {

                    }
                }
            }
            else
            {

            }
            return result;
        }

        public async Task<ResultResponse> Editar(EditarProductoDTO dTO)
        {
            var result = new ResultResponse();
            var srcProduct = _mappper.Map<EditarProductoDTO, Producto>(dTO);
            var currentProduct = _unitOfWork.Producto.Where(x => x.Id == dTO.ProductoId).FirstOrDefault();
            if(currentProduct != null)
            {
                currentProduct = _mappper.Map(srcProduct, currentProduct);

            }
            else
            {

            }
            return result;
        }

        public async Task<ResultResponse> Eliminar(string productId)
        {
            var result = new ResultResponse();
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
