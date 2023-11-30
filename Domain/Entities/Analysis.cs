using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Analysis
    {
        [Key]
        public int Id { get; set; }

        public string Metric { get; set; }

        public string Value { get; set; }

        public DateTime Timestamp { get; set; }

        public Transfer Transfer{ get; set; }
    }
}
