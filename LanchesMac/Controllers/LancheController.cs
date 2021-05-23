using LanchesMac.Models;
using LanchesMac.Repositories;
using LanchesMac.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LanchesMac.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILancheRepository lancheRepository;
        private readonly ICategoriaRepository categoriaRepository;

        public LancheController(
            ILancheRepository lancheRepository, 
            ICategoriaRepository categoriaRepository)
        {
            this.lancheRepository = lancheRepository;
            this.categoriaRepository = categoriaRepository;
        }

        public IActionResult List()
        {
            ViewBag.Lanche = "Lanches";
            ViewData["Categoria"] = "Categoria";

            //IEnumerable<Lanche> lanches = lancheRepository.Lanches;
            //return View(lanches);

            var lancheListViewModel = new LancheListViewModel();
            lancheListViewModel.Lanches = lancheRepository.Lanches;
            lancheListViewModel.CategoriaAtual = "Categoria Atual";
            return View(lancheListViewModel);

        }

    }
}
