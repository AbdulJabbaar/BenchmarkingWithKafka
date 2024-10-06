using BenchmarkDotNet.Attributes;
using Confluent.Kafka;

namespace MessagingWithKafka
{
    [MemoryDiagnoser]
    [MarkdownExporter]
    public class KafkaProducerBenchmarks
    {
        private readonly IProducer<string, string> kafka_produce_sync;
        private readonly IProducer<string, string> kafka_produce_async;

        private readonly string errorMessage = @"ErrorDetails:- Code - {0}, Reason - {1}";

        public KafkaProducerBenchmarks()
        {
            var producerConfig = new ProducerConfig()
            {
                BootstrapServers = "localhost:9092",
                QueueBufferingMaxMessages = 500_000
            };

            kafka_produce_sync = new ProducerBuilder<string, string>(producerConfig)
                            .SetErrorHandler((p, e) =>
                                        Console.WriteLine(errorMessage, e.Code, e.Reason))
                            .Build();

            kafka_produce_async = new ProducerBuilder<string, string>(producerConfig)
                             .SetErrorHandler((p, e) =>
                                        Console.WriteLine(errorMessage, e.Code, e.Reason))
                            .Build();
        }

        [Benchmark]
        public void KafkaProducerSync()
        {
            var kafkaMessage = new KafkaMessage
            {
                Id = Guid.NewGuid().ToString(),
                ActionType = "NewRecord",
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                Payload = "this will contain some json object or any other message",
            };

            var msg = new Message<string, string>()
            {
                Key = kafkaMessage.Id,
                Value = kafkaMessage.ToString(),
            };

            kafka_produce_sync.Produce("kafka_topic_produce_sync", msg, d =>
            {
                if (d.Error.IsError)
                    throw new InvalidOperationException($"{d.Error.Code}:{d.Error.Reason}");
            });
        }

        [Benchmark]
        public async Task KafkaProducerAsync()
        {
            var kafkaMessage = new KafkaMessage
            {
                Id = Guid.NewGuid().ToString(),
                ActionType = "NewRecord",
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                Payload = "this will contain some json object or any other message",
            };

            var msg = new Message<string, string>()
            {
                Key = kafkaMessage.Id,
                Value = kafkaMessage.ToString(),
            };

            await kafka_produce_async.ProduceAsync("kafka_topic_produce_async", msg);
        }
    }
}
