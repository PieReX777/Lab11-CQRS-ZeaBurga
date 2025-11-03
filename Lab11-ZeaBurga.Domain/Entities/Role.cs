using System.Collections.Generic;
using System;

namespace Lab11_ZeaBurga.Domain.Entities;

/// <summary>
/// Entidad que representa la tabla 'roles'
/// </summary>
public class Role
{
    public Guid RoleId { get; set; }
    public string RoleName { get; set; } = string.Empty;

    // Propiedad de navegación para la relación muchos-a-muchos
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}