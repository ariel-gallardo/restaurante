using AutoMapper;
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
        public Task<ResultResponse> Crear(CrearIngredienteDTO dTO)
        {
            throw new NotImplementedException();
        }

        public Task<ResultResponse> Editar(EditarIngredienteDTO dTO)
        {
            throw new NotImplementedException();
        }

        public Task<ResultResponse> Eliminar(string ingredienteId)
        {
            throw new NotImplementedException();
        }

        public Task<ResultResponse> Listar(int? paginaNum = 1, string? productoId = "", string? ordenarPor = "", bool? ascendente = true, string? nombreClave = "", double? precioMin = 0, double? precioMax = 0)
        {
            throw new NotImplementedException();
        }

        public Task<ResultResponse> Restaurar(string ingredienteId)
        {
            throw new NotImplementedException();
        }
    }
}
