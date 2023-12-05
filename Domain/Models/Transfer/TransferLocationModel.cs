using Core.Models;

namespace Core.DTO.Transfer
{
    public class TransferLocationModel : IModel
    {
        public float Latitude { get; set; }

        public float Longitude { get; set; }
    }
}
