namespace TransferApi.Models
{
    public class User
    {
        public int ID { get; set; }
        public int CartID { get; set; }
        public string Name { get; set; }
        public ICollection<Cart> carts { get; set; }
    }
}