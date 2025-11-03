using System.Linq.Expressions;
using Lab11_ZeaBurga.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lab11_ZeaBurga.Infrastructure.Persistence.Repositories;

/// <summary>
/// Implementación concreta del Repositorio Genérico.
/// Esta clase traduce las operaciones de la interfaz (Domain)
/// a comandos de Entity Framework Core (Infrastructure).
/// </summary>
public class Repository<T> : IRepository<T> where T : class
{
    // El DbContext es protegido para que las clases que hereden
    // de este repositorio (si las hubiera) puedan acceder a él.
    protected readonly TicketeraDbContext _context;

    public Repository(TicketeraDbContext context)
    {
        _context = context;
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        // AsNoTracking() es una optimización para consultas de solo lectura.
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }
    
    public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().Where(predicate).AsNoTracking().ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public void Update(T entity)
    {
        // Marca la entidad como modificada
        _context.Set<T>().Update(entity);
        // O una forma más explícita:
        // _context.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }
}