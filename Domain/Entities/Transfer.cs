using Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Transfer
    {
        [Key]
        public int Id { get; set; }

        public DateTime StartingDate { get; set; }

        public int StartingLocationId { get; set; }

        public Location StartingLocation { get; set; }

        public DateTime EndingDate { get; set; }

        public int EndingLocationId { get; set; }

        public Location EndingLocation { get; set; }

        public TransferCondition TransferCondition { get; set; }

        public TransferStatus Status { get; set; }

        public User User { get; set; }

        public List<Analysis> Analysises { get; set; }
    }
}
