using eticaret.DLL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace eticaret.Web.Controllers.api
{
	[Route("api/[controller]")]
	[ApiController]
	public class categoriesController : ControllerBase
	{
		[Route("[action]"), HttpPost]
		public IActionResult getCategoriList([FromForm] int id, [FromForm] int page, [FromForm] int itemsPerPage, [FromForm] string search, [FromForm] string price, [FromForm] int listSorting)
		{
			try
			{
				using (eticaretContext ec = new eticaretContext())
				{
					int startingPrice = 0;
					int endPrice = 0;
					bool filterPrice = false;
					if (!string.IsNullOrEmpty(price) & price != "0;10000")
					{
						filterPrice = true;
						startingPrice = Convert.ToInt32(price.Split(";")[0]);
						endPrice = Convert.ToInt32(price.Split(";")[1]);
					}

					var query = ec.products.Where(x =>
					(listSorting <=4 | (listSorting == 5 && x.stock > 0)) &
					(filterPrice == false | (filterPrice == true & x.salePrice >= startingPrice & x.salePrice <= endPrice)) &
					x.isActive == true &
					x.categoriID == id &
					(string.IsNullOrEmpty(search) | (!string.IsNullOrEmpty(search) &
					((x.name.Contains(search)) |
					(x.tags.Contains(search)) |
					(x.details.Contains(search)))
					)));

					switch (listSorting)
					{
						case 1:
							query = query.OrderBy(x => x.salePrice);
							break;
						case 2:
							query = query.OrderByDescending(x => x.salePrice);
							break;
						case 3:
							query = query.OrderBy(x => x.name);
							break;
						case 4:
							query = query.OrderByDescending(x => x.name);
							break;
						default:
							query = query.OrderByDescending(x => x.id);
							break;
					}

					var count = query.Count();
					var response = query.Skip((page - 1) * itemsPerPage).Take(itemsPerPage).ToList();
					var categoryName = ec.categories.FirstOrDefault(x => x.id == id)?.name ?? "";
					var tags = ec.products.Where(x => x.isActive == true && x.categoriID == id).Select(x => x.tags).ToList();

					return Ok(new { type = "success", message = "", data = response, c = count, name = categoryName, tags = tags });
				}
			}
			catch { }
			return Ok(new { type = "error", message = "" });
		}
	}
}
