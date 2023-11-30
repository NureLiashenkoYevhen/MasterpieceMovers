using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class TransferCondition
    {
        [Key]
        public int Id { get; set; }

        public float MinTemperature { get; set; }

        public float MaxTemperature { get; set; }

        public float MinHumidity { get; set; }

        public float MaxHumidity { get; set; }

        public int TransferId { get; set; }

        public Transfer Transfer { get; set; }
    }
}
