using System.Collections.Generic; 

namespace eticaret.Services.bulletinServices
{
    public interface IbulletinService
    {
        public Dictionary<string, object> updateBulletin(string email);
    }
}
