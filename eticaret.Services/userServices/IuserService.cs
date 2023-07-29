using eticaret.Services.userServices.Dto;
using System.Collections.Generic; 

namespace eticaret.Services.userServices
{
	public interface IuserService
	{
		public Dictionary<string, string> register(userDto user);
		public Dictionary<string, string> login(userDto user);
	}
}
