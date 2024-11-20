using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Restaurante.DAO;
using Restaurante.Models;
using System.Linq;

namespace Restaurante.Services
{
    public class ProductoIngredienteServices : IProductoIngredienteServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductoIngredienteServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ResultResponse> Asociar(ProductoIngredienteDTO dTO)
        {
            var resultResponse = new ResultResponse();

            if (_unitOfWork.Producto.ExistsActive(dTO.ProductoId))
            {
                var producto = await _unitOfWork.Producto.ProductoWithIngrediente(dTO.ProductoId);
                var grouped = dTO.Data.GroupBy(x => x.IngredienteId);
                if (!grouped.Any(x => x.Count() > 1))
                {
                    var toUpdate = producto.Ingredientes.Where(x => dTO.Data.Any(y => y.Id == x.Id && (y.IngredienteId != x.IngredienteId || x.Cantidad != y.Cantidad || x.Unidad != y.Unidad))).ToList();
                    var toDelete = producto.Ingredientes.Where(x => dTO.Data.Any(y => x.Id != y.Id)).ToList();
                    var toInsert = dTO.Data.Where(x => producto.Ingredientes.Any(y => y.IngredienteId != x.IngredienteId && y.Id != x.Id))
                        .Select(x => new ProductoIngrediente 
                        { 
                            Id = x.Id, 
                            IngredienteId = x.IngredienteId, 
                            ProductoId = dTO.ProductoId,
                            Unidad = x.Unidad,
                            Cantidad = x.Cantidad,
                        }).ToList();
                    _unitOfWork.ProductoIngrediente.Update(toUpdate);
                    _unitOfWork.ProductoIngrediente.Delete(toDelete);
                    await _unitOfWork.ProductoIngrediente.Insert(toInsert);
                    await _unitOfWork.SaveChangesAsync();
                    resultResponse.Message = $@"ENTITY_CHANGES ""PRODUCT_INGREDIENT,{toUpdate.Count},{toDelete.Count},{toInsert.Count},{dTO.Data.Count}""";
                    resultResponse.StatusCode = StatusCodes.Status200OK;
                }
                else
                {
                    var strRepeated = string.Empty;
                    foreach (var g in grouped.Where(x => x.Count() > 1))
                    {
                        var t = g.FirstOrDefault();
                        strRepeated = !string.IsNullOrEmpty(strRepeated) ? $"{strRepeated}, {t.IngredienteId}" : t.IngredienteId;
                    }
                    resultResponse.StatusCode = 400;
                    resultResponse.Message = $@"REPEATED ""INGREDIENTS,{strRepeated}""";
                }
            }
            else
            {
                resultResponse.StatusCode = 404;
                resultResponse.Message = $@"ENTITY_NOT_FOUND ""PRODUCT,{dTO.ProductoId}""";
            }

            return resultResponse;
        }
    }
}
