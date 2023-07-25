using eticaret.DLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;


public class response
{
    public static List<viewsFavorite> productFavorites(string whichDay)
    {
        List<viewsFavorite> response = new List<viewsFavorite> { };
        using (dbeticaretContext ec = new dbeticaretContext())
        {
            // Burada whichDay gelen değere göre bugünün mü en çok favoriye eklenen 5 ürünleri listeleme işlemi
            // Dünün mü en çok favoriye eklenen 5 ürünleri listeleme işlemi
            // Yoksa genel olarak mı en çok favoriye eklenen 5 ürünleri listeleme işlemi yapıyoruz.
            response = ec.viewsFavorites.AsQueryable().Where(x => whichDay == "Today" ? (x.creatingDate >= DateTime.Today) : whichDay == "Yesterday" ? (x.creatingDate < DateTime.Today && x.creatingDate >= DateTime.Today.AddDays(-1)) : x.productID > 0).OrderByDescending(x => x.favCount).Take(10).ToList();

            if (response.Count < 5)
            {
                //Burada bugun en çok favoriye eklenen ürünler 5 ten az ise bunu tamamalamak için en çok favoriye eklenen ürünleri
                //listeleyerek liste sayısını 5'e tamamlıyoruz.
                var yesterdaysRecords = ec.viewsFavorites.AsQueryable().Where(x => whichDay == "Today" ? (x.creatingDate < DateTime.Today) : whichDay == "Yesterday" ? (x.creatingDate < DateTime.Today.AddDays(-1)) : x.productID > 0).OrderByDescending(x => x.favCount).Take(5 - response.Count).ToList();
                response.AddRange(yesterdaysRecords);
            }
        }
        return response;
    }

    public static List<slider> getSliders()
    {
        List<slider> response = new List<slider> { };
        using (dbeticaretContext ec = new dbeticaretContext())
        {
            // Burada aktif olan tüm slider görsellerini getiren ve rank isteğe göre listeleyen komut.
            response = ec.sliders.AsQueryable().Where(x => x.isActive == true).OrderBy(x => x.rank).ToList();
        }
        return response;
    }

    public static List<viewsCategory> geCategories()
    {
        List<viewsCategory> response = new List<viewsCategory> { };
        using (dbeticaretContext ec = new dbeticaretContext())
        {
            // Burada aktif olan tüm kategorileri ve her kategoriden kaç tane ürün olduğunu getirir(db de views yazıldı)
            response = ec.viewsCategories.AsQueryable().ToList();
        }
        return response;
    }
}

