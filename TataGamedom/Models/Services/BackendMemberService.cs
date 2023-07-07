﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TataGamedom.Models.Dtos;
using TataGamedom.Models.Infra;
using TataGamedom.Models.Interfaces;
using TataGamedom.Models.ViewModels;

namespace TataGamedom.Models.Services
{
	public class BackendMemberService
	{
		private IBackendMemberRepositiry _repo;
		public BackendMemberService(IBackendMemberRepositiry repo)
		{
			_repo = repo;
		}

		public Result ValidLogin(LoginDto dto)
		{
			var backendmember = _repo.ValidLogin(dto);

			if (backendmember == null)
			{
				return Result.Fail("帳密錯誤");
			}
			if (backendmember.ActiveFlag == false)
			{
				return Result.Fail("權限不足無法登入");
			}

			var salt = HashUtility.GetSalt();
			var hashPwd = HashUtility.ToSHA256(dto.Password, salt);

			return string.Compare(backendmember.Password, hashPwd) == 0 ? Result.Success() : Result.Fail("帳密有誤");
		}

		public EditProfileVM GetBackendMemberProfile(string account)
		{
			var member = _repo.GetBackendMemberProfile(account);

			if (member == null)
			{
				return null;
			}

			var editProfileVM = new EditProfileVM
			{
				Id = member.Id,
				Email = member.Email,
				Name = member.Name,
				Phone = member.Phone
			};

			return editProfileVM;
		}


		public Result Register(RegisterDto dto)
		{
			//判斷帳號是否已被使用
			if (_repo.ExistAccount(dto.Account))
			{
				//丟出異常，或者傳回Result
				return Result.Fail($"帳號{dto.Account}已存在，請更換後再試一次");
			}
			//將密碼進行雜湊
			var salt = HashUtility.GetSalt();
			var hashPwd = HashUtility.ToSHA256(dto.Password, salt);
			dto.Password = hashPwd;

			//新增一筆紀錄
			_repo.Register(dto);

			//todo 寄發email
			return Result.Success();
		}
	}
}