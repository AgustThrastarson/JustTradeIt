using System;
using System.Text;
using JustTradeIt.Software.API.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;


namespace JustTradeIt.Software.API.Services.Implementations
{
    public class QueueService : IQueueService, IDisposable
    {
        private ConnectionFactory _factory;
        private IModel _channel;
        private IConnection _connection;
        private byte[] ConvertJsonToBytes(object obj) => Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj));
        private readonly string _hostname = Environment.GetEnvironmentVariable("QUEUE_HOST") ?? "localhost";

        public QueueService()
        {
            _factory = new ConnectionFactory() { HostName = _hostname, UserName = "guest", Password = "guest" };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }
        

        public void PublishMessage(string routingKey, object body)
        {

            _channel.BasicPublish(exchange: "trade_exchange",
                routingKey: routingKey,
                basicProperties: null,
                body: ConvertJsonToBytes(body));
        }
    }
}