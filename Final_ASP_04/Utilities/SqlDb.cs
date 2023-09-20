﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Final_ASP_04.Utilities
{
	public class SqlDb
	{
		public string GetConnString(string key)
		{
			var setting = System.Configuration.ConfigurationManager
								.ConnectionStrings[key];
			if (setting == null)
			{
				throw new Exception($"找不到連線字串{key}，請您確認後再試一次");
			}

			return setting.ConnectionString;
		}

		public SqlConnection GetConnection(string key)
		{
			var connString = GetConnString(key);
			return new SqlConnection(connString);
		}
	}
}