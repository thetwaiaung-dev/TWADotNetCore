namespace TWADotNetCore.RestApi.LimitRequests
{
    public class ClientStatistics
    {
        public DateTime LastSuccessfulResponseTime { get; set; }
        public int NumberOfRequestsCompleteSuccessfully {  get; set; }  
    }
}