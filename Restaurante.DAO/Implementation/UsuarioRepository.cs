using Microsoft.EntityFrameworkCore;
using Restaurante.Models;
using Restaurante.Models.Enums;

namespace Restaurante.DAO
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {

        public UsuarioRepository(IRepository<Usuario> repository) : base(repository)
        {

        }
        public Usuario SearchUserActiveByEmail(string email) 
        {
            var usuario = Where(u => u.DeletedAt == null && u.Email == email)
                .Include(u => u.Persona)
                .Include(u => u.Persona.Domicilio)
                .Include(u => u.Persona.Telefono)
                .Include(u => u.Rol)
                .FirstOrDefault();
            return usuario;
        }

        public async Task<Usuario> SearchUserActiveByEmailWithDelivery(string email) {
            var u = SearchUserActiveByEmail(email);
            var p = await UnitOfWork.Pedido.Where(x => x.UsuarioId == u.Id && x.Estado == EstadoPedido.Buscando).FirstOrDefaultAsync();
            
            if(p == null)
            {
                p = new Pedido { Estado = EstadoPedido.Buscando, UsuarioId = u.Id };
                await UnitOfWork.Pedido.Insert(p);
                await UnitOfWork.SaveChangesAsync();
                u.PedidoActual = p;
            }
            else
                u.PedidoActual = p;

            return u;
        }
    }
}
