using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Final_ASP_04.Models.Infra
{
	public static class HashUtility
	{
		public static string ToSHA256(string plainText, string salt)
		{
			using (var mySHA256 = SHA256.Create())
			{
				var passwordBytes = Encoding.UTF8.GetBytes(plainText + salt);
				var hash = mySHA256.ComputeHash(passwordBytes);
				var sb = new StringBuilder();
				foreach (var c in hash)
				{
					sb.Append(c.ToString("X2"));
				}

				return sb.ToString();
			}
		}

		internal static string GetSalt()
		{
			return System.Configuration.ConfigurationManager.AppSettings["Salt"];
		}
	}
}