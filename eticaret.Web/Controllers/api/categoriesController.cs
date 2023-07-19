using eticaret.DLL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace eticaret.Web.Controllers.api
{
	[Route("api/[controller]")]
	[ApiController]
	public class categoriesController : ControllerBase
	{
		[Route("[action]"), HttpPost] 
		public IActionResult getCategoriList([FromForm] int id, [FromForm] int page, [FromForm] int itemsPerPage, [FromForm] string search)
		{
			try
			{
				using (eticaretContext ec = new eticaretContext())
				{
					var query = ec.products.Where(x =>
					x.isActive == true &
					x.categoriID == id &
					(string.IsNullOrEmpty(search) | (!string.IsNullOrEmpty(search) &
					((x.name.Contains(search)) |
					(x.tags.Contains(search)) |
					(x.details.Contains(search)))
					)));
					var count = query.Count();
					var response = query.OrderByDescending(x => x.id).Skip((page - 1) * itemsPerPage).Take(itemsPerPage).ToList(); 
					var categoryName = ec.categories.FirstOrDefault(x => x.id == id)?.name ?? ""; 

					return Ok(new { type = "success", message = "", data = response, c = count, name = categoryName });
				}
			}
			catch { }
			return Ok(new { type = "error", message = "" });
		}
	}
}
