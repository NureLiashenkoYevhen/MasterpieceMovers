using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Location
    {
        [Key]
        public int Id { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public List<Transfer> StartedTransfers { get; set; }

        public List<Transfer> FinishedTransfers { get; set; }
    }
}
