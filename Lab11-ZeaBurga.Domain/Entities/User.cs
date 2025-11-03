using System;
using System.Collections.Generic;

namespace Lab11_ZeaBurga.Domain.Entities;

/// <summary>
/// Entidad que representa la tabla 'users'
/// </summary>
public class User
{
    public Guid UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string? Email { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Propiedades de navegación
    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    public ICollection<Response> Responses { get; set; } = new List<Response>();
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}