using MessagePack;

namespace CSharpExample_MD.Types
{
    [MessagePackObject(keyAsPropertyName: true)]
    public  class Trade
    {
        public string Symbol { get; set; }

        public string TradeId { get; set; }

        public DateTime TradeTimeUtc { get; set; }

        public string Buyer { get; set; }

        public string Seller { get; set; }

        public double Quantity { get; set; }

        public double Price { get; set; }

        public string TradeCondition { get; set; }

        public TradeAction Action { get; set; }
    }

    public enum TradeAction { Undefined, New, Cancel, Cancelled }
}
