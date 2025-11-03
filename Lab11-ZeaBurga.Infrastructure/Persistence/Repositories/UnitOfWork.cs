using System.Collections;
using Lab11_ZeaBurga.Domain.Interfaces;

namespace Lab11_ZeaBurga.Infrastructure.Persistence.Repositories;

/// <summary>
/// Implementación concreta de la Unidad de Trabajo.
/// Inyecta el DbContext y administra su ciclo de vida y
/// la creación de repositorios genéricos.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly TicketeraDbContext _context;
    
    // Usamos Hashtable para guardar "instancias" de repositorios genéricos.
    // (Tal como en el Lab 10 y el ejemplo del Lab 11)
    private Hashtable _repositories;

    public UnitOfWork(TicketeraDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Guarda todos los cambios en la base de datos.
    /// </summary>
    public async Task<int> CompleteAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Libera el DbContext cuando el UnitOfWork es desechado.
    /// </summary>
    public void Dispose()
    {
        _context.Dispose();
    }

    /// <summary>
    /// Método genérico que crea (o reutiliza) un repositorio para una entidad T.
    /// </summary>
    public IRepository<T> Repository<T>() where T : class
    {
        // Si _repositories es nulo, lo inicializa.
        _repositories ??= new Hashtable();

        var type = typeof(T).Name;

        // Si ya tenemos un repositorio para este tipo, lo devolvemos.
        if (_repositories.ContainsKey(type))
        {
            return (IRepository<T>)_repositories[type]!;
        }

        // Si no, creamos uno nuevo, lo guardamos y lo devolvemos.
        var repositoryType = typeof(Repository<>);
        var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);

        _repositories.Add(type, repositoryInstance!);
        return (IRepository<T>)_repositories[type]!;
    }
}