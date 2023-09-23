using Final_ASP_04.Models.EFModels;
using Final_ASP_04.Models.Infra;
using Final_ASP_04.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_ASP_04.Models.Exts
{
	public static class MemberExts
	{
		public static Member RegisterVmToMember(this RegisterVm vm)
		{
			var salt = HashUtility.GetSalt();
			var hashedPassword = HashUtility.ToSHA256(vm.Password, salt);
			var confirmCode = Guid.NewGuid().ToString("N");

			return new Member
			{
				Name = vm.Name,
				Account = vm.Account,
				EncryptedPassword = hashedPassword,
				PhoneNumber = vm.PhoneNumber,
				Email = vm.Email,
				ConfirmCode = confirmCode,
				IsConfirmed = false,
				Enabled = true
			};
		}

		public static EditProfileVm MemberToEditProfileVm(this Member model)
		{
			return new EditProfileVm
			{
				Id = model.Id,
				Name = model.Name,
				Account = model.Account,
				Email = model.Email,
				PhoneNumber = model.PhoneNumber,
			};
		}
	}
}