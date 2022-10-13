using System.ComponentModel.DataAnnotations;

namespace SinaMerchant.Web.Entities
{
    // site's user or customer's table
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? FName { get; set; }
        public string? LName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? Phone { get; set; }
        public string EmailActiveCode { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime RegisterDate { get; set; }

        public ICollection<ShopOrder>? ShopOrders { get; set; }
    }
}
