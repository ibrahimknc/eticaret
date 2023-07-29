using eticaret.Services.logServices.Dto;
using Microsoft.AspNetCore.Http;  

namespace eticaret.Services.logServices
{ 
	public interface IlogService
	{
		public bool addLog(logDto logDto, HttpContext context); 
	}
}
