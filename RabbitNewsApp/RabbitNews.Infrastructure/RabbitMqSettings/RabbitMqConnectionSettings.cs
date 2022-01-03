namespace RabbitNews.Infrastructure.RabbitMqSettings
{
    public interface IRabbitMqConnectionSettings
    {
        string Hostname { get; set; }
        int Port { get; set; }
        string Username { get; set; }
        string Password { get; set; }
    }

    public class RabbitMqConnectionSettings : IRabbitMqConnectionSettings
    {
        public string Hostname { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
