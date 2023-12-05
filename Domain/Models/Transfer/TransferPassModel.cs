using Core.Entities;

namespace Core.Models.Transfer
{
    public class TransferPassModel : IModel
    {
        public int Id { get; set; }

        public Location StartingLocation { get; set; }

        public Location EndingLocation { get; set; }

        public TransferCondition Condition { get; set; }
    }
}
