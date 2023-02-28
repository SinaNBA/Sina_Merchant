namespace SinaMerchant.Web.Entities
{
    public class ShopOrder : EntityBase
    {
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Price { get; set; }// total price

        public User User { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }

    }
}
