using TransferApi.Models;

namespace TransferApi.Mapper
{
    public class TransferTransactionDto
    {
        public DateTime ResultDate { get; set; }
         public int ResultCode { get; set; }
        public string ResultDescription { get; set; } = string.Empty;
           public Transfer Transfer { get; set; }
        //  public Status? Status { get; set; }
    }
    public enum Status
    {
        Pending, Success, Fail
    }
}