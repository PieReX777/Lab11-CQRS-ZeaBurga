using AutoMapper;
using Lab11_ZeaBurga.Domain.Entities;
using Lab11_ZeaBurga.Domain.Interfaces;
using MediatR;

namespace Lab11_ZeaBurga.Application.Features.Tickets.Commands;

/// <summary>
/// El Comando (Command) - Es el DTO o "Request" que se envía a MediatR.
/// Define los datos necesarios para crear un ticket.
/// Implementa IRequest<Guid> porque este comando devolverá el Guid del nuevo ticket.
/// </summary>
public record CreateTicketCommand : IRequest<Guid>
{
    public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

/// <summary>
/// El Handler (Manejador) - Contiene la lógica de negocio.
/// Es la única clase que sabe cómo "manejar" un CreateTicketCommand.
/// </summary>
internal sealed class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateTicketCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
    {
        // 1. Mapear el Comando (Request) a la Entidad del Dominio
        var ticket = _mapper.Map<Ticket>(request);

        // 2. Establecer valores por defecto (lógica de negocio)
        ticket.TicketId = Guid.NewGuid();
        ticket.Status = "abierto"; // Estado inicial por defecto
        ticket.CreatedAt = DateTime.UtcNow;

        // 3. Usar el Repositorio (vía UnitOfWork) para añadir la entidad
        await _unitOfWork.Repository<Ticket>().AddAsync(ticket);
        
        // 4. Guardar los cambios en la base de datos
        await _unitOfWork.CompleteAsync(cancellationToken);

        // 5. Devolver el ID del nuevo ticket
        return ticket.TicketId;
    }
}