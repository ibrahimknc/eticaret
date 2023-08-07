using eticaret.Data;
using eticaret.Domain.Entities;
using eticaret.Services.viewsFavoriteServices.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace eticaret.Services.viewsFavoriteServices
{
    public class viewsFavoriteService : IviewsFavoriteService
    {
        readonly dbeticaretContext _dbeticaretContext;
        public viewsFavoriteService(dbeticaretContext dbeticaretContext)
        {
            _dbeticaretContext = dbeticaretContext;
        }

        public List<viewsFavoriteDto> GetFavoriteProducts(string whichDay)
        {
            var result = _dbeticaretContext.products
         .Where(p => p.isActive)
         .Join(_dbeticaretContext.userFavorites,
             p => p.id,
             uf => uf.productID,
             (p, uf) => new { Product = p, Favorite = uf })
         .GroupBy(x => new
         {
             x.Product.id,
             x.Product.name,
             x.Product.tags,
             x.Product.stock,
             x.Product.details,
             x.Product.basePrice,
             x.Product.salePrice,
             x.Product.categoriID,
             CategoryName = x.Product.Category.name,
             x.Product.image
         })
         .Where(g => g.Count() > 0)
         .OrderByDescending(g => g.Count())
         .Select(g => new viewsFavoriteDto
         {
             FavCount = g.Count(),
             ProductID = g.Key.id,
             Name = g.Key.name,
             Tags = g.Key.tags,
             CreatingTime = g.Min(x => x.Favorite.creatingTime),
             Stock = g.Key.stock,
             Details = g.Key.details,
             BasePrice = g.Key.basePrice,
             SalePrice = g.Key.salePrice,
             CategoryID = g.Key.categoriID,
             CategoryName = g.Key.CategoryName,
             Image = g.Key.image,
             commentCount = _dbeticaretContext.comments.Count(c => c.productID == g.Key.id),
             averageRating = _dbeticaretContext.comments
                            .Where(c => c.productID == g.Key.id)
                            .Average(c => (double?)c.rating) ?? 0
         })
         .ToList();

            // Burada whichDay gelen değere göre bugünün mü en çok favoriye eklenen 5 ürünleri listeleme işlemi
            // Dünün mü en çok favoriye eklenen 5 ürünleri listeleme işlemi
            // Yoksa genel olarak mı en çok favoriye eklenen 5 ürünleri listeleme işlemi yapıyoruz.
            DateTime filterDate = whichDay == "Today" ? DateTime.Today :
                     whichDay == "Yesterday" ? DateTime.Today.AddDays(-1) :
                     DateTime.MinValue;

            var response = result.AsQueryable()
                      .Where(x => x.CreatingTime >= filterDate)
                      .OrderByDescending(x => x.FavCount)
                      .Take(10)
                      .ToList();

            if (response.Count < 5)
            {
                //Burada bugun en çok favoriye eklenen ürünler 5 ten az ise bunu tamamalamak için en çok favoriye eklenen ürünleri
                //listeleyerek liste sayısını 5'e tamamlıyoruz.
                DateTime yesterdayFilterDate = whichDay == "Today" ? DateTime.Today :
                      whichDay == "Yesterday" ? DateTime.Today.AddDays(-1) :
                      DateTime.MaxValue;

                var yesterdaysRecords = result.AsQueryable()
                                              .Where(x => x.CreatingTime < yesterdayFilterDate)
                                              .OrderByDescending(x => x.FavCount)
                                              .Take(5 - response.Count)
                                              .ToList();
                response.AddRange(yesterdaysRecords);
            }
            return response; 
        }
    }
}
