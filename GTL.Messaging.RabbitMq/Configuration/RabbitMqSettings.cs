namespace GTL.Messaging.RabbitMq.Configuration;

public class RabbitMqSettings
{
    public string Host { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public ushort Port { get; set; }
}