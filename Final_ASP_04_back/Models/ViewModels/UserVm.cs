using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Final_ASP_04_back.Models.ViewModels
{
    public class UserVm
    {
        public string Account { get; set; }
        public string Name { get; set; }
		[DataType(DataType.Password)]
		public string Password { get; set; }
        public bool Enabled { get; set; }
    }
}