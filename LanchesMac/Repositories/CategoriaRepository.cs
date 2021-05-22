using LanchesMac.Context;
using LanchesMac.Models;
using System.Collections.Generic;

namespace LanchesMac.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDBContext context;
        public IEnumerable<Categoria> Categorias => context.Categorias;

        public CategoriaRepository(AppDBContext appDBContext)
        {
            context = appDBContext;
        }
    }
}