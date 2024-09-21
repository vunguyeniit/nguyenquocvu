using EasyCaching.Core;
using System.ComponentModel.DataAnnotations;

namespace SoKHCNVTAPI.Models
{
	public class APIKeyModel
    {
        public required string Code { get; set; }
        public required string Name { get; set; }

        public required string Key { get; set; }

        public string IP { get; set; } = "";

        public int Rate { get; set; } = 30;

        public short Status { get; set; } = 1;

        public long TimeStamp { get; set; } = DateTime.Now.Ticks;
    }


    public class APIKeyFilter
    {
        public string TuKhoa { get; set; } = "";
        public string? Keyword { get; set; } = "";

        public string Key { get; set; } = "";

        public string IP { get; set; } = "";

        public long Rate { get; set; } = 30;

        public short Status { get; set; } = 1;
    }

}