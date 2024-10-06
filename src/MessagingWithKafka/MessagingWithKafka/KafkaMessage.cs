using System.Text.Json;

namespace MessagingWithKafka
{
    public class KafkaMessage
    {
        public string Id { get; set; }
        public string ActionType { get; set; }
        public long Timestamp { get; set; }
        public string Payload { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
