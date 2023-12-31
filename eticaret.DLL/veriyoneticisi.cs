﻿using eticaret.Domain.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic; 
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Iyzipay;

public class veriyoneticisi
{
    public static Dictionary<short, string> yetkiler = new Dictionary<short, string> { { 0, "Supervisor" }, { 1, "Personel" } };
    public readonly static short siteId = 1; // 1-Site, 2-Site 
    public static bool isDevelopment = false;
	public static bool isActive = true; 
	public static bool isLogin = false;
	public static Guid userID = Guid.Empty;
    public static Setting setting = new Setting();
	public static Dictionary<string, string> projectSettings
    {
        get
        {
            if(isDevelopment)
            {
                return new Dictionary<string, string> {
                    { "projectName", "eticaret"},
                    { "author", "ibrahim KONÇ"},
                    { "logo", "/uploads/logo/logo.png"},
                    { "favicon", "/uploads/logo/favicon.png"},
                    { "siteUrl", "https://localhost:5001" },
                    { "adminUrl", "https://localhost:5001" },
                    { "BotApi", "5517710931:AAHpUPbMVcdbmozyjVcfFTEhE2nrw76-6bk"},
                };
            }
            if (siteId == 2)
            {
                return new Dictionary<string, string> {
                    { "projectName", "eticaret2"},
                    { "logo", "/uploads/logo/logo.png"},
                    { "favicon", "/uploads/logo/favicon.png"},
                    { "siteUrl", "https://eticaret2.com" },
                    { "adminUrl", "https://admin.eticaret2.com" },
                    { "BotApi", "xxxxxxxxxxxxxxxxxxxxxxxxxx"},
                };
            }
            return new Dictionary<string, string> {
                    { "projectName", "eticaret"},
                    { "author", "ibrahim KONÇ"},
					{ "logo", "/uploads/logo/logo.png"},
                    { "favicon", "/uploads/logo/favicon.png"},
                    { "siteUrl", "https://localhost:5001" },
                    { "adminUrl", "https://localhost:5001" },
                    { "BotApi", "5517710931:AAHpUPbMVcdbmozyjVcfFTEhE2nrw76-6bk"},
            };
        }
    }

    public static Iyzipay.Options GetOptionsForPaymentMethod(string paymentMethod)
    {
        Iyzipay.Options options = new Iyzipay.Options();

        if (paymentMethod == "iyzipay")
        {
            options.ApiKey = "sandbox-mmnHVN63RvYLMeZl3r53QIb2up2m5QOw";
            options.SecretKey = "4vlq8WSFfVttxgSz3iw1C4ul9M66h5oZ";
            options.BaseUrl = "https://sandbox-api.iyzipay.com";
        }
        else if (paymentMethod == "bank_transfer")
        {
            options.ApiKey = "sandbox-another-api-key";
            options.SecretKey = "sandbox-another-secret-key";
            options.BaseUrl = "https://sandbox-api-another.iyzipay.com";
        }
        // Eğer daha fazla ödeme yöntemi eklemek isterseniz buraya yeni şartlar ekleyebilirsiniz.

        return options;
    }

    public static string ratingChange(double rating)
    {
        var ratingHtml = "";
        for (var i = 0; i < 5; i++)
        {
            if (rating > i)
            {
                ratingHtml += "<div class=\"star on\"></div>";
            }
            else
            {
                ratingHtml += "<div class=\"star off\"></div>";
            }
        }
        return ratingHtml;
    }
    public static string GenerateSharedKey
    {
        get
        {
            Random rand = new Random();
            string letters = "QWERTYUIOPASDFGHJKLZXCVBNM1234567890";
            string key = "";
            for (int i = 0; i < 20; i++)
            {
                key += letters[rand.Next(letters.Length)];
            }
            return key;
        }
    }
    public static bool emailChecker(string emailaddress)
    {
        try
        {
            var m = new System.Net.Mail.MailAddress(emailaddress);

            return true;
        }
        catch
        {
            return false;
        }
    }
    public static bool passwordChecker(string passwrd)
    {
		Regex regex = new Regex(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).{8,}$");
        return regex.IsMatch(passwrd);
		//return passwrd.Any(char.IsUpper) && passwrd.Any(char.IsLower) && passwrd.Any(char.IsDigit);
    }
	 
	public static string MD5Hash(string text)
    {
        MD5 md5 = new MD5CryptoServiceProvider();
        md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

        byte[] result = md5.Hash;

        StringBuilder strBuilder = new StringBuilder();
        for (int i = 0; i < result.Length; i++)
        {
            strBuilder.Append(result[i].ToString("x2"));
        }

        return strBuilder.ToString();
    }

}
