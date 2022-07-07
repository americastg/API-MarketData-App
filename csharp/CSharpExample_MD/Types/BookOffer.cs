using MessagePack;

namespace CSharpExample_MD.Types
{
    [MessagePackObject(keyAsPropertyName: true)]
    public class BookOffer
    {
        public double Quantity { get; set; }

        public double Price { get; set; }

        public int NumberOfOrders { get; set; }
    }
}
