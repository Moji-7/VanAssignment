namespace TransferApi.Models
{
    public class TransferTransaction
    {
        public int ID { get; set; }
        public DateTime SendDate { get; set; }

        #region all result form Core Banking switch
        public Status? Status { get; set; }
        public DateTime ResponseDate { get; set; }
        public int ResultCode { get; set; }
        public string ResultDescription { get; set; } = string.Empty;
        public int TraceNumber { get; set; }
        #endregion
        public Guid TransferID { get; set; }
        public Transfer Transfer { get; set; }
    }
    public enum Status
    {
        Pending, Success, Fail
    }

}