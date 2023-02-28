namespace SinaMerchant.Web.Entities
{
    public class Permission : EntityBase
    {
        public string Name { get; set; }

        #region Relations

        public ICollection<RolePermission> RolePermissions { get; set; }

        #endregion
    }
}
