﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.DataModels.Core;
using Models.DataModels.RoleSystem;

namespace Models.DataModels
{
    public class User : BaseModel
    {
        [Required] public string Email { get; set; }

        [Required] public string Password { get; set; }

        public bool LoggedIn { get; set; } = false;
        public DateTime? LastLogOut { get; set; }

        [NotMapped] public string Token { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual Customer Customer { get; set; }
    }
}