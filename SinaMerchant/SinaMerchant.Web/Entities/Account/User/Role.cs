namespace SinaMerchant.Web.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleTitle { get; set; }

        #region Relations

        public ICollection<UserRole> UserRoles { get; set; }

        #endregion
    }
}
