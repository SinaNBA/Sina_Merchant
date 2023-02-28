﻿namespace SinaMerchant.Web.Entities
{
    public class UserRole : EntityBase
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        #region Relations

        public User User { get; set; }
        public Role Role { get; set; }

        #endregion
    }
}
