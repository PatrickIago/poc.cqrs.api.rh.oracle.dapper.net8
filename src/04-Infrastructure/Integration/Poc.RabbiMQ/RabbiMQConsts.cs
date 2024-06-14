namespace Poc.Auth.TwilioWhatsApp;

public class RabbiMQConsts
{
    // RabbiMQ
    public const string Hostname = "RabbitMQ:Hostname";
    public const string Port = "RabbitMQ:Port";
    public const string Username = "RabbitMQ:Username";
    public const string Password = "RabbitMQ:Password";
    public const string VirtualHost = "RabbitMQ:VirtualHost";

    // SENDGRID
    public const string QUEUE_SENDGRID = "RabbitMQ:QUEUE:QUEUE_SENDGRID";

    // TWILIO
    public const string QUEUE_TWILIO_SMS = "RabbitMQ:QUEUE:QUEUE_TWILIO_SMS";
    public const string QUEUE_TWILIO_WHATSAPP = "RabbitMQ:QUEUE:QUEUE_TWILIO_WHATSAPP";
}