namespace SinaMerchant.Web.Entities
{
    public class RolePermission
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }

        #region Relations

        public Role Role { get; set; }
        public Permission Permission { get; set; }

        #endregion

    }
}
