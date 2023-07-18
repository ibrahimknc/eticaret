using eticaret.DLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;


public class response
{
	public static List<viewsFavorite> todayFavorites()
	{
		
		List<viewsFavorite> response = new List<viewsFavorite> { };
		using (eticaretContext ec = new eticaretContext())
		{
			//Burada bugun en çok favoriye eklenen 5 ürünleri listeleme işlemi yapıyoruz.
			response = ec.viewsFavorites.AsQueryable().Where(x => x.creatingDate >= DateTime.Today).OrderByDescending(x => x.favCount).Take(10).ToList();

			if (response.Count < 5)
			{
				//Burada bugun en çok favoriye eklenen ürünler 5 ten az ise bunu tamamalamak için en çok favoriye eklenen ürünleri
				//listeleyerek liste sayısını 5'e tamamlıyoruz.
				var yesterdaysRecords = ec.viewsFavorites.AsQueryable().Where(x => x.creatingDate < DateTime.Today).OrderByDescending(x => x.favCount).Take(5 - response.Count).ToList();
				response.AddRange(yesterdaysRecords);
			}
		}
		return response;
	}
}

