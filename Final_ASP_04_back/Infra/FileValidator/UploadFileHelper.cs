using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Final_ASP_04_back.Infra.FileValidator
{
	public interface IFileValidator
	{
		void Validate(HttpPostedFileBase file); // 若失敗就傳回錯誤訊息
	}
	public static class UploadFileHelper
	{
		public static string Save(HttpPostedFileBase file, string path, IFileValidator[] validators)
		{
			// 驗證檔案
			if (validators != null)
			{
				foreach (var validator in validators)
				{
					validator.Validate(file);
				}
			}

			// 如果沒有檔案, 就結束
			if (file == null || file.ContentLength == 0) return string.Empty;

			// 取一個隨機檔名
			string ext = Path.GetExtension(file.FileName);
			string fileName = Path.GetRandomFileName() + ext;
			string fullPath = Path.Combine(path, fileName);
			file.SaveAs(fullPath);

			return fileName;
		}
	}
}