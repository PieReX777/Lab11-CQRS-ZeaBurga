using System;
using System.Collections.Generic;

namespace Lab11_ZeaBurga.Domain.Entities;

/// <summary>
/// Entidad que representa la tabla 'tickets'
/// </summary>
public class Ticket
{
    public Guid TicketId { get; set; }
    public Guid UserId { get; set; } // Foreign key
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    
    // El CHECK (status IN ('abierto', 'en_proceso', 'cerrado'))
    // se manejará con un Enum o validación.
    public string Status { get; set; } = string.Empty; 
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ClosedAt { get; set; }

    // Propiedades de navegación
    public User User { get; set; } = null!; 
    public ICollection<Response> Responses { get; set; } = new List<Response>();
}