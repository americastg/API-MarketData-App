using MessagePack;

namespace CSharpExample_MD.Types
{
    [MessagePackObject(keyAsPropertyName: true)]
    public class Book
    {
        public string Symbol { get; set; }

        public List<BookOffer> Bids { get; set; }

        public List<BookOffer> Asks { get; set; }
    }
}
