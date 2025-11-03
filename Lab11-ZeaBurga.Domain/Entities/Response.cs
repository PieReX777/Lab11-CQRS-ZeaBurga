using System;

namespace Lab11_ZeaBurga.Domain.Entities;

/// <summary>
/// Entidad que representa la tabla 'responses'
/// </summary>
public class Response
{
    public Guid ResponseId { get; set; }
    public Guid TicketId { get; set; } // Foreign key
    public Guid ResponderId { get; set; } // Foreign key (quien responde, un User)
    public string Message { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Propiedades de navegación
    public Ticket Ticket { get; set; } = null!; 
    public User Responder { get; set; } = null!;
}