using MessagePack;

namespace CSharpExample_MD.Types
{
    [MessagePackObject(keyAsPropertyName: true)]
    public class BestOffers
    {
        public string Symbol { get; set; }
        public int TradingStatusCode { get; set; }
        public DateTime LastTradeTimestamp { get; set; }
        public double LastTradePrice { get; set; }
        public long LastTradeQty { get; set; }
        public double MaxPrice { get; set; }
        public double MinPrice { get; set; }
        public double Variation { get; set; }
        public double OpenPrice { get; set; }
        public double ClosePrice { get; set; }
        public double VolumeDay { get; set; }
        public double QtyTradedDay { get; set; }
        public double AvgPrice { get; set; }
        public double BestBidQty { get; set; }
        public double BestBidPrice { get; set; }
        public double BestAskQty { get; set; }
        public double BestAskPrice { get; set; }
        public double? AuctionPrice { get; set; }
        public char AuctionImbalanceSide { get; set; }
        public double? AuctionImbalanceVolume { get; set; }
        public double? AuctionVolume { get; set; }
        public DateTime? EstimatedTradingSessionOpenTime { get; set; }
        public TradingSession TradingSession { get; set; }
        public double ClosePriceDay { get; set; }
        public double AdjustPrice { get; set; }
        public double AdjustPriceDay { get; set; }
        public double? HardLowLimitPrice { get; set; }
        public double? HardHighLimitPrice { get; set; }
        public double? AuctionLowLimitPercent { get; set; }
        public double? AuctionHighLimitPercent { get; set; }
        public bool? IsPercentageAuctionBand { get; set; }
        public double? RejectionLowLimitPercent { get; set; }
        public double? RejectionHighLimitPercent { get; set; }
        public bool? IsPercentageRejectionBand { get; set; }
        public double? StaticLowLimitPrice { get; set; }
        public double? StaticHighLimitPrice { get; set; }
        public long? MaxTradeVol { get; set; }
        public double? OpenInterest { get; set; }
        public long? AvgDailyTradedQty { get; set; }
    }

    public enum TradingSession { Undefined = 0, RegularDaySession = 1, AfterHours = 6 };
}
