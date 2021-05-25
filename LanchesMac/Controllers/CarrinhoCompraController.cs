using LanchesMac.Models;
using LanchesMac.Repositories;
using LanchesMac.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace LanchesMac.Controllers
{
    public class CarrinhoCompraController : Controller
    {
        private readonly ILancheRepository lancheRepository;
        private readonly CarrinhoCompra carrinhoCompra;

        public CarrinhoCompraController(
            ILancheRepository lancheRepository,
            CarrinhoCompra carrinhoCompra)
        {
            this.lancheRepository = lancheRepository;
            this.carrinhoCompra = carrinhoCompra;
        }

        public IActionResult Index()
        {
            List<CarrinhoCompraItem> itens = carrinhoCompra.GetCarrinhoCompraItens();
            carrinhoCompra.CarrinhoCompraItens = itens;

            var carrinhoCompraViewModel = new CarrinhoCompraViewModel()
            {
                CarrinhoCompra = carrinhoCompra,
                CarrinhoCompraTotal = carrinhoCompra.GetCarrinhoCompraTotal()
            };
            return View(carrinhoCompraViewModel);
        }

        public RedirectToActionResult AdicionarItemNoCarrinhoCompra(int lancheId)
        {
            Lanche lancheSelecionado = lancheRepository.Lanches.FirstOrDefault(l => l.LancheId == lancheId);

            if (lancheSelecionado != null)
            {
                carrinhoCompra.AdicionarAoCarrinho(lancheSelecionado, 1);
            }
            return RedirectToAction("Index");
        }


        public RedirectToActionResult RemoverItemNoCarrinhoCompra(int lancheId)
        {
            Lanche lancheSelecionado = lancheRepository.Lanches.FirstOrDefault(l => l.LancheId == lancheId);

            if (lancheSelecionado != null)
            {
                carrinhoCompra.RemoverDoCarrinho(lancheSelecionado);
            }
            return RedirectToAction("Index");
        }

    }
}
