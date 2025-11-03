using Lab11_ZeaBurga.Application.Features.Tickets.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lab11_ZeaBurga.Controllers;

[ApiController]
[Route("api/[controller]")] // La ruta será /api/tickets
public class TicketsController : ControllerBase
{
    // El controlador SOLO depende de MediatR
    private readonly IMediator _mediator;

    public TicketsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Endpoint para crear un nuevo ticket.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTicket([FromBody] CreateTicketCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // 1. Envía el comando a MediatR.
        // MediatR encontrará automáticamente el Handler (CreateTicketCommandHandler)
        // que creamos en la capa de Application.
        var ticketId = await _mediator.Send(command);

        // 2. Devuelve una respuesta HTTP 201 (Created) con el ID del nuevo ticket.
        // (Devolvemos el ID en un objeto anónimo para que sea un JSON válido)
        return CreatedAtAction(nameof(GetTicketById), new { id = ticketId }, new { ticketId });
    }
    
    // NOTA: Este endpoint (GetTicketById) aún no existe,
    // pero lo usamos en 'CreatedAtAction' como buena práctica.
    // Lo implementaríamos con una "Query" (Consulta) de CQRS.
    [HttpGet("{id:guid}")]
    public IActionResult GetTicketById(Guid id)
    {
        // Esta lógica la implementaríamos en el Paso 9 (Queries)
        // Por ahora, solo es un placeholder para que CreatedAtAction funcione.
        return Ok(new { Message = $"Endpoint de 'Get' para el ticket {id} (aún no implementado)." });
    }
}