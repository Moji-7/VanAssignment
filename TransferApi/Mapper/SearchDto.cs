namespace TransferApi.Mapper
{
    public class SearchDto
    {
        public class TransferSearchDto
        {
            [System.ComponentModel.DefaultValue("4001 5900 0000 0001")]
            public string? CardNumber { get; set; }
        }
        public class TransactionSearchDto
        {
            public string? CardNumber { get; set; }= string.Empty;
            public DateTime? fromDate { get; set; }
             
        }
    }
}