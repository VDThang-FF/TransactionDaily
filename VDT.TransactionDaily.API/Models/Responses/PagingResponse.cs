namespace VDT.TransactionDaily.API.Models.Responses
{
    public class PagingResponse
    {
        public object PageData { get; set; }
        public object SummaryData { get; set; }
        public int TotalRecord { get; set; }
    }
}
