﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TataGamedom.Models.ViewModels.GameComments;

namespace TataGamedom.Models.Interfaces
{
	public interface IGameCommentRepository
	{
		IEnumerable<GameCommentIndexVM> Get(int? id);

		GameCommentDetailVM GetCommentById(int id);

		bool Delete(GameCommentDetailVM vm);

		bool Restore(GameCommentDetailVM vm);

	}
}
