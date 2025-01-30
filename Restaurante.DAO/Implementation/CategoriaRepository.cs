using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Restaurante.Infraestructure;
using Restaurante.Models;

namespace Restaurante.DAO
{
    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(IRepository<Categoria> repository) : base(repository)
        {
        }

        public async Task<Categoria> Crear(Categoria categoria)
        {
            Categoria catPadre = null;
            if (categoria.CategoriaPadre != null && !string.IsNullOrWhiteSpace(categoria.CategoriaPadre.Nombre) && categoria.CategoriaPadreId == null)
            {
                catPadre = await WhereActive(x => x.Nombre == categoria.CategoriaPadre.Nombre).FirstOrDefaultAsync();
                if (catPadre == null)
                {
                    catPadre = new Categoria { Nombre = categoria.CategoriaPadre.Nombre };
                    await Insert(catPadre);
                    await UnitOfWork.SaveChangesAsync();
                    categoria.CategoriaPadre = catPadre;
                }
                else
                {
                    categoria.CategoriaPadre = catPadre;
                }
            }
            if ((await WhereActive(x => x.Nombre == categoria.Nombre).FirstOrDefaultAsync()) == null)
            {
                await Insert(categoria);
                await UnitOfWork.SaveChangesAsync();
                return categoria;
            }
            return null;
        }

        public async Task<bool> EditarCategory(Categoria? entity, Categoria newProperties)
        {
            if (entity != null)
            {
                Update(newProperties);
                return true;
            }
            return false;
        }

        public async Task<Paginacion<Categoria>> ListarCategorias(int paginaNum = 1, bool ascendente = true, string nombreClave = "", string catPadreId = "")
        {
            var resultList = new List<Categoria>();
            Expression<Func<Categoria, bool>> baseQuerie = x => true;
            long? catPadreIdS = !string.IsNullOrWhiteSpace(catPadreId) ? long.Parse(catPadreId) : null;
            (var total, var querie) = WhereAsPaginateQuerie(
                x => 
                   (!string.IsNullOrWhiteSpace(nombreClave) ? EF.Functions.Like(x.Nombre,$"%{nombreClave}%") : true)
                && (catPadreIdS != null ? x.CategoriaPadreId == catPadreIdS : x.CategoriaPadreId == null)
                , x => x.Nombre, ascendente, paginaNum
            );

            if (total > 0)
                resultList.AddRange(await querie.ToListAsync());
            return Paginacion<Categoria>.Crear(resultList, total, paginaNum);
        }
    }
}
