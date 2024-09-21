using System;
namespace SoKHCNVTAPI.Models
{
	public class StepUserModel
	{
		public required long UserId { get; set; }
		public required long StepId { get; set; }
        public short Status { get; set; }
    }
}

