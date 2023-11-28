using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Transfer
    {
        [Key]
        public int Id { get; set; }

        public DateTime StartingDate { get; set; }

        public int StartLocationId { get; set; }

        public Location StartingLocation { get; set; }

        public DateTime EndingDate { get; set; }

        public int EndingLocationId { get; set; }

        public Location EndingLocation { get; set; }

        public ShipmentCondition ShipmentCondition { get; set; }

        public TransferStatus Status { get; set; }

        public User User { get; set; }

        public List<Analytic> Analytics { get; set; }
    }
}
