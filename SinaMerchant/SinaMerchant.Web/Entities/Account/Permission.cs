namespace SinaMerchant.Web.Entities
{
    public class Permission
    {
        public int Id { get; set; }
        public string Name { get; set; }

        #region Relations

        public ICollection<RolePermission> RolePermissions { get; set; }

        #endregion
    }
}
