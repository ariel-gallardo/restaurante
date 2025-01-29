using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurante.DAO;
using Restaurante.Models;
using UnitsNet;
using UnitsNet.Units;
using static System.Net.Mime.MediaTypeNames;

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

        public async Task<ResultResponse> Consumir(ConsumirProductoDTO dto)
        {
            var resultResponse = new ResultResponse();

            var ids = dto.Data.Select(x => x.Id);
            if(ids.Count() > 0)
            {
                var products = new List<Producto>();
                var ingredients = new List<Ingrediente>();
                
                products.AddRange(await _unitOfWork.Producto.WhereActive(x => ids.Contains(x.Id))
                                .Include(x => x.Ingredientes)
                                .ThenInclude(x => x.Ingrediente)
                                .ToArrayAsync());
                if(products.Count > 0)
                {
                    var consumo = products.VerConsumoDeProductos(dto);
                    var ingredientes = products.SelectMany(x => x.Ingredientes.Select(y => y.Ingrediente)).Distinct().ToList();
                    var calculoTotalConsumo = new Dictionary<string, (double, bool)>();
                    var prodIdCannotConsume = new List<string>();

                    foreach (var cK in consumo.Keys)
                    {
                        if(consumo.TryGetValue(cK,out (double,bool) output))
                        {
                            (var cantidad, var esIngrediente) = output;
                            if (esIngrediente)
                            {
                                var iBase = ingredientes.FirstOrDefault(x => x.Id == cK);
                                var calculo = (iBase.StockActual ?? 0.0 - cantidad);
                                calculoTotalConsumo.Add(cK, (calculo, esIngrediente));
                                if (calculo < 0.0)
                                    prodIdCannotConsume.AddRange(products.Where(x => x.Ingredientes.Any(y => y.IngredienteId == cK)).Select(x => x.Id));
                                else
                                    iBase.StockActual = calculo;
                            }
                            else
                            {
                                var cProduct = products.FirstOrDefault(x => x.Id == cK);
                                var calculo = (cProduct.StockActual ?? 0.0 - cantidad);
                                calculoTotalConsumo.Add(cK, (calculo, esIngrediente));
                                if(calculo < 0.0)
                                    prodIdCannotConsume.Add(cK);
                                else
                                    cProduct.StockActual = calculo;
                            }
                        }
                    }

                    if(prodIdCannotConsume.Count > 0)
                    {
                        prodIdCannotConsume = prodIdCannotConsume.Distinct().ToList();
                        _unitOfWork.ClearChanges();
                    }
                    else
                    {
                        await _unitOfWork.SaveChangesAsync();
                    }

                    resultResponse.Content = prodIdCannotConsume.Count > 0 ? prodIdCannotConsume : null;
                    resultResponse.StatusCode = prodIdCannotConsume.Count > 0 ? 400 : 200;
                    resultResponse.Message = prodIdCannotConsume.Count > 0 ? $"STOCK_ERROR_PRODUCTS" : "CONSUME_PRODUCTS";
                }
                else
                {
                    resultResponse.StatusCode = 404;
                    resultResponse.Message = $@"EMPTY_DATA ""PRODUCTS""";
                }
            }
            else
            {
                resultResponse.StatusCode = 404;
                resultResponse.Message = $@"EMPTY_DATA ""PRODUCTS""";
            }
            return resultResponse;
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
                result.StatusCode = 200;
            }
            else
            {
                result.Message = $@"ENTITY_NOT_EXISTS_ID ""PRODUCT,{productId}""";
                result.StatusCode = 400;
            }

            return result;
        }

        public async Task<ResultResponse> Listar(int? paginaNum = 1, string? ordenarPor = "", bool? ascendente = true, string? nombreClave = "", double? precioMin = 0.0, double? precioMax = 0.0, long? categoria = 0)
        {
            var result = new ResultResponse();

            var resultData = await _unitOfWork.Producto.ListarProductos(paginaNum, ordenarPor, ascendente, nombreClave, precioMin, precioMax, categoria);
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
