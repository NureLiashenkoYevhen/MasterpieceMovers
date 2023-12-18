using Core.Enums;

namespace Core.Models.Transfer
{
    public class UpdateTransferModel : IModel
    {
        public int Id { get; set; }

        public DateTime StartingDate { get; set; }

        public float StartingLocationLatitude { get; set; }

        public float StartingLocationLongitude { get; set; }

        public DateTime EndingDate { get; set; }

        public float EndingLocationLatitude { get; set; }

        public float EndingLocationLongitude { get; set; }

        public float MinTemperature { get; set; }

        public float MaxTemperature { get; set; }

        public float MinHumidity { get; set; }

        public float MaxHumidity { get; set; }

        public TransferStatus TransferStatus { get; set; }
    }
}
