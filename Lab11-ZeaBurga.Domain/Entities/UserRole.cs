using System;

namespace Lab11_ZeaBurga.Domain.Entities;

/// <summary>
/// Entidad que representa la tabla pivote 'user_roles'
/// </summary>
public class UserRole
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

    // Propiedades de navegación
    public User User { get; set; } = null!;
    public Role Role { get; set; } = null!;
}