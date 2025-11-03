using System.Linq.Expressions;

namespace Lab11_ZeaBurga.Domain.Interfaces;

/// <summary>
/// Interfaz de Repositorio Genérico (Contrato)
/// Define las operaciones de datos estándar para cualquier entidad.
/// </summary>
/// <typeparam name="T">La entidad del dominio (ej. Ticket, User)</typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Obtiene una entidad por su Id (UUID).
    /// </summary>
    Task<T?> GetByIdAsync(Guid id);

    /// <summary>
    /// Obtiene una lista de solo lectura de todas las entidades.
    /// </summary>
    Task<IReadOnlyList<T>> GetAllAsync();

    /// <summary>
    /// Obtiene la primera entidad que cumple con una condición (predicado).
    /// </summary>
    Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
    
    /// <summary>
    /// Obtiene todas las entidades que cumplen con una condición (predicado).
    /// </summary>
    Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// Añade una nueva entidad (marcada para inserción).
    /// </summary>
    Task AddAsync(T entity);

    /// <summary>
    /// Actualiza una entidad existente (marcada para modificación).
    /// </summary>
    void Update(T entity);

    /// <summary>
    /// Elimina una entidad existente (marcada para borrado).
    /// </summary>
    void Delete(T entity);
}