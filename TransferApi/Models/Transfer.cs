using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransferApi.Models
{
    public class Transfer
    {

        public Guid ID { get; set; }

        public DateTime CreationDate { get; set; }

        [Required]
        [StringLength(110, MinimumLength = 3)]
        public string CounterpartyName { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        //TODO: Just for Iran 
        [RegularExpression(@"^\d{2}\-\d{7}\-(\d{3}|\d{2})$")]
        public string CounterpartyIBAN { get; set; }


        [Range(1, 100000)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z\s]*$")]
        public string Description { get; set; } = string.Empty;

        [Range(typeof(DateTime), "1/1/2022", "1/1/2030")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ScheduleDate { get; set; }
        public Boolean IsSigned { get; set; }
        public DateTime? SignedDate { get; set; }
        //public int CartID { get; set; }
        public Cart Cart { get; set; }

        public int TransactionID { get; set; }
        public ICollection<TransferTransaction>? Transaction { get; set; }
    }
}