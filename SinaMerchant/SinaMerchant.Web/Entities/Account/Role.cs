namespace SinaMerchant.Web.Entities
{
    public class Role: EntityBase
    {
        public string Title { get; set; }

        #region Relations

        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; }

        #endregion
    }
}
