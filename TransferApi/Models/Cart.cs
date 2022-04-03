using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TransferApi.Models
{
    [Index(nameof(CardNumber), IsUnique = true)]
    public class Cart
    {
        [Key]
        public string CardNumber { get; set; }
      
        // [CardInfoMask]
        public CartInfo CartInfo { get; set; }

        [Range(0, 1000000000)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Balance { get; set; }
        // public ICollection<Transfer> Transfers { get; set; }
    }

    public class CartInfo
    {
        [Key]
        [CreditCard(ErrorMessage = "credit card is not valid")]
        public string CardNumber { get; set; }
        [RegularExpression(@"^(0?[1-9]|1[0-2])/(2[0-9])$")]
        public string ExpireDate { get; set; }

        [RegularExpression(@"^([0-9]{3}|[0-9]{4})$")]
        public int CVV2 { get; set; }

    }
}