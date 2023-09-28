using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_ASP_04_back.Infra.FileValidator
{
	public class FileSizeValidator : IFileValidator
	{
		private readonly int _maxBytes, _maxKBs;
		public FileSizeValidator(int maxKBs)
		{
			if (maxKBs <= 0)
			{
				throw new ArgumentException("maxKBs must be greater than zero");
			}
			_maxKBs = maxKBs;
			_maxBytes = maxKBs * 1024;
		}

		public void Validate(HttpPostedFileBase file)
		{
			if (file == null || file.ContentLength == 0) return;

			if (file.ContentLength > _maxBytes)
			{
				throw new Exception($"您的檔案太大({file.ContentLength / 1024}KB)了, 必須小於 " + _maxKBs + "KB.");
			}
		}
	}
}