using LanchesMac.Models;
using System.Collections.Generic;

namespace LanchesMac.Repositories
{
    public interface ICategoriaRepository
    {
        IEnumerable<Categoria> Categorias { get; }
    }
}