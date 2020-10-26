using System;
namespace Services.DTOs
{
    public class RoleDTO : ResponseBaseDTO
    {
        public string Name { get; set; }
    }
    public class PermissionDTO : ResponseBaseDTO
    {
        public string Name { get; set; }
    }
}
