using LanchesMac.Models;
using LanchesMac.Repositories;
using LanchesMac.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LanchesMac.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILancheRepository lancheRepository;

        public HomeController(ILancheRepository lancheRepository)
        {
            this.lancheRepository = lancheRepository;
        }

        public IActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel()
            {
                LanchesPreferidos = lancheRepository.LanchesPreferidos
            };

            return View(homeViewModel);
        }

    }
}
