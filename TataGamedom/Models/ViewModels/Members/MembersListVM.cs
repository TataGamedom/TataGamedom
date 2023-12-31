﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TataGamedom.Models.ViewModels.Members
{
	public class MembersListVM
	{
		public int Id { get; set; }
		[Display(Name = "會員名稱")]

		[StringLength(50)]
		public string Name { get; set; }
		[Display(Name = "會員帳號")]

		[StringLength(30)]
		public string Account { get; set; }

		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
		[Display(Name = "生日")]
		public DateTime Birthday { get; set; }

		[Display(Name = "信箱")]
		[StringLength(150)]
		public string Email { get; set; }
		[Display(Name = "手機")]

		[StringLength(10)]
		public string Phone { get; set; }

		[Display(Name = "帳號狀態")]
		public bool ActiveFlag { get; set; }

		public string ActiveFlagText
		{
			get
			{
				return ActiveFlag == true ? "使用中" : "停權";
			}
		}

		[Display(Name="創建時間")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
		public DateTime RegistrationDate { get; set; }
	}
}