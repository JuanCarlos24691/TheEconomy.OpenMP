using System.Configuration;

namespace TheEconomy.Database
{
    public class ConectionString 
    {
        public string ConnectionStrings { get; set; }

        public ConectionString()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStrings"].ConnectionString;
            ConnectionStrings = string.IsNullOrWhiteSpace(connectionString) ? "unknown" : connectionString;
        }
    }
}