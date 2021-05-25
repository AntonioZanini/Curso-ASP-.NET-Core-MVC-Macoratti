using LanchesMac.Context;
using LanchesMac.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LanchesMac.Repositories
{
    public class LancheRepository : ILancheRepository
    {
        private readonly AppDBContext context;

        public IEnumerable<Lanche> Lanches => context.Lanches.Include(l => l.Categoria);
        public IEnumerable<Lanche> LanchesPreferidos => context.Lanches.Where(l => l.IsLanchePreferido).Include(l => l.Categoria);

        public LancheRepository(AppDBContext appDBContext)
        {
            context = appDBContext;
        }

        public Lanche GetLancheById(int lancheId)
        {
            return Lanches.First(l => l.LancheId == lancheId);
        }
    }
}