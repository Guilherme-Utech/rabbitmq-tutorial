using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory { HostName = "localhost"};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "hello"
                    , durable: false
                    , exclusive: false
                    , autoDelete: false
                    , arguments: null);

while (true)
{
    Console.WriteLine("Type a message ... (Type END to close the program)");
    var msg = Console.ReadLine();

    if (msg == "END")
        return;

    var body = Encoding.UTF8.GetBytes(msg);

    channel.BasicPublish(exchange: string.Empty
                        , routingKey: "hello"
                        , basicProperties: null
                        , body: body)
    ;
}