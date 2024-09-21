using System.Globalization;

namespace SoKHCNVTAPI.Helpers
{
    public class RepositoryException : Exception
    {
        public RepositoryException() : base() { }

        public RepositoryException(string message) : base(message) { }

        public RepositoryException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args)) { }
    }
}
