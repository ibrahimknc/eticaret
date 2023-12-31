﻿using eticaret.Services.userServices.Dto;
using System;
using System.Collections.Generic;

namespace eticaret.Services.userServices
{
    public interface IuserService
    {
        public Dictionary<string, object> getUserProfile(Guid id);
        public Dictionary<string, object> updateUser(userDto user); 
        public Dictionary<string, object> getUserFavorites(Guid userID, int page, int itemsPerPage, string search, int listSorting);
        public Dictionary<string, object> updateUserFavorite(Guid userID, Guid productID, Guid favoriteID);
        public Dictionary<string, object> updateUserPassword(userDto user, string newPassword);
        public Dictionary<string, string> register(userDto user);
        public Dictionary<string, string> login(userDto user);
    }
}
