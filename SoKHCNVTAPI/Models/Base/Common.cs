using System;
namespace SoKHCNVTAPI.Models.Base
{
	public class Common
	{
        
    }

    public class ResultTwoValCompare<T>
    {
        public required List<T> firstArray { get; set; }
        public required List<T> secondArray { get; set; }
    }
}

    