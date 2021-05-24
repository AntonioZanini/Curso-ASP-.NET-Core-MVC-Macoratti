using LanchesMac.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanchesMac.Models
{
    public class CarrinhoCompra
    {
        private readonly AppDBContext context;

        public string CarrinhoCompraId { get; set; }
        public List<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }

        public CarrinhoCompra(AppDBContext appDBContext)
        {
            context = appDBContext;
        }

        public static CarrinhoCompra GetCarrinho(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

            session.SetString("CarrinhoId", carrinhoId);

            return new CarrinhoCompra(services.GetService<AppDBContext>()) { CarrinhoCompraId = carrinhoId };
        }

        public void AdicionarAoCarrinho(Lanche lanche, int quantidade)
        {
            CarrinhoCompraItem carrinhoCompraItem = context.CarrinhoCompraItens
                .FirstOrDefault(c => c.Lanche.LancheId == lanche.LancheId &&
                                     c.CarrinhoCompraId == CarrinhoCompraId);

            if (carrinhoCompraItem == null)
            {
                carrinhoCompraItem = new CarrinhoCompraItem()
                {
                    CarrinhoCompraId = CarrinhoCompraId,
                    Lanche = lanche,
                    Quantidade = quantidade
                };
                context.CarrinhoCompraItens.Add(carrinhoCompraItem);
            }
            else
            {
                carrinhoCompraItem.Quantidade++;
            }
            context.SaveChanges();
        }

        public int RemoverDoCarrinho(Lanche lanche)
        {
            CarrinhoCompraItem carrinhoCompraItem = context.CarrinhoCompraItens
                .FirstOrDefault(c => c.Lanche.LancheId == lanche.LancheId &&
                                     c.CarrinhoCompraId == CarrinhoCompraId);

            var quantidadeLocal = 0;

            if (carrinhoCompraItem != null)
            { 
                if (carrinhoCompraItem.Quantidade > 1)
                {
                    carrinhoCompraItem.Quantidade--;
                    quantidadeLocal = carrinhoCompraItem.Quantidade;
                }
                else
                {
                    context.CarrinhoCompraItens.Remove(carrinhoCompraItem);
                }
            }

            context.SaveChanges();

            return quantidadeLocal;
        }

        public List<CarrinhoCompraItem> GetCarrinhoCompraItens()
        {
            return CarrinhoCompraItens ?? (CarrinhoCompraItens = context.CarrinhoCompraItens.Where(c => c.CarrinhoCompraId).Include(s => s.Lanche).ToList());
        }

        public void LimparCarrinho()
        {
            var carrinhoItens = context.CarrinhoCompraItens.Where(c => c.CarrinhoCompraId = CarrinhoCompraId);

            context.CarrinhoCompraItens.RemoveRange(carrinhoItens);
            context.SaveChanges();
        }

        public decimal GetCarrinhoCompraTotal()
        {
            var total = context.CarrinhoCompraItens
                .Where(c => c.CarrinhoCompraId = CarrinhoCompraId)
                .Select(c => c.Lanche.Preco * c.Quantidade).Sum();

            return total;
        }
    }
}
