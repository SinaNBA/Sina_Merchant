namespace SinaMerchant.Web.Entities
{
    public class RolePermission: EntityBase
    {
        public int RoleId { get; set; }
        public int PermissionId { get; set; }

        #region Relations

        public Role Role { get; set; }
        public Permission Permission { get; set; }

        #endregion

    }
}
