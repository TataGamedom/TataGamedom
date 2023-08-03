﻿namespace TataGamedomWebAPI.Models.Dtos
{
	public class CommentReadDto
	{
        public int CommentId { get; set; }
		public string CommentContent { get; set; }
		public DateTime DateTime { get; set; }
		public string MemberAccount { get; set; }
		public string MemberName { get; set; }
        public int VoteUp { get; set; }
		public int VoteDown { get; set; }
	}
}