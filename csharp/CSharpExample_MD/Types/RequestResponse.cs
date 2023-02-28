namespace CSharpExample_MD.Types
{
    public class RequestResponse
    {
        public List<string> AllowedSymbols { get; set; } = new();
        public List<string> NonExistentSymbols { get; set; } = new();
        public List<string> UnauthorizedSymbols { get; set; } = new();
        public List<string> SegmentNotAllowedSymbols { get; set; } = new();
    }
}
