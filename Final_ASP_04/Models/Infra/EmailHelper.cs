using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace Final_ASP_04.Models.Infra
{
	public class EmailHelper
	{
		private string senderEmail = "justin52608486@gmail.com";

		public void SenderForgetPasswordEmail(string url, string name, string email)
		{
			var subject = "[重設密碼通知]";
			var body = $@"您好 {name},
<br />
請點擊此[<a href='{url}' target='_blank'>連結</a>,來重設密碼, 若沒有提出申請, 請忽略本信, 謝謝]";

			var from = senderEmail;
			var to = email;

			SendViaGoogle(from, to, subject, body);
		}

		public void SenderConfirmRegisterEmail(string url, string name, string email)
		{
			var subject = "[新會員確認信]";
			var body = $@"您好 {name},
<br />
請點擊此[<a href='{url}' target='_blank'>連結</a>,已提出重設密碼, 若沒有提出申請, 請忽略本信, 謝謝]";

			var from = senderEmail;
			var to = email;

			SendViaGoogle(from, to, subject, body);

		}

		public virtual void SendViaGoogle(string from, string to, string subject, string body)
		{
			var smtp = new SmtpClient();
			smtp.Host = "smtp.gmail.com";
			smtp.Port = 587;
			smtp.EnableSsl = true;
			smtp.UseDefaultCredentials = false;
			smtp.Credentials = new NetworkCredential(from, "haaw jbuh wmab rliw");
			smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

			var mail = new MailMessage(from, to);
			mail.Subject = subject;
			mail.Body = body;
			mail.IsBodyHtml = true;
			smtp.Send(mail);
			return;
		}

		private void CreateTextFile(string path, string from, string to, string subject, string body)
		{
			var fileName = $"{to.Replace("@", "_")}{DateTime.Now.ToString("yyyyMMdd")}.txt";

			var fullPath = Path.Combine(path, fileName);

			var contents = $@"from:{from}
to:{to}
subject:{subject}

{body}";

			File.WriteAllText(fullPath, contents, Encoding.UTF8);
		}
	}
}