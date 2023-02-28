namespace CSharpExample_MD.Types
{
    public enum RequestType { Book, BestOffers, Trades };

    public class RequestObject
    {
        public string Token { get; set; }
        public List<string> Symbols { get; set; }
        public int UpdateFreq { get; set; }

        public RequestObject(string token, List<string> symbols, int updateFreq)
        {
            Token = token;
            Symbols = symbols;
            UpdateFreq = updateFreq;
        }
    }
}
