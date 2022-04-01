using System.ComponentModel.DataAnnotations;
using TransferApi.Models;

namespace TransferApi.Mapper
{
    public class TransferDto
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
        public decimal Amount { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z\s]*$")]
        public string Description { get; set; } = string.Empty;

        public Cart Cart { get; set; }
        public Boolean IsSigned { get; set; }
        public  DateTime? SignedDate { get; set; }
          public ICollection<TransferTransaction>? Transaction { get; set; }
    }
}