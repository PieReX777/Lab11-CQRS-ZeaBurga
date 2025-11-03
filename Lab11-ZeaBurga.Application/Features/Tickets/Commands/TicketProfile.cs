using AutoMapper;
using Lab11_ZeaBurga.Application.Features.Tickets.Commands;
using Lab11_ZeaBurga.Domain.Entities;

namespace Lab11_ZeaBurga.Application.Features.Tickets.Mapping;

public class TicketProfile : Profile
{
    public TicketProfile()
    {
        // Mapea del Comando -> a la Entidad
        CreateMap<CreateTicketCommand, Ticket>();
        
        // Aquí agregaríamos otros mapeos, por ejemplo:
        // CreateMap<Ticket, TicketDto>(); // Para las Queries
    }
}