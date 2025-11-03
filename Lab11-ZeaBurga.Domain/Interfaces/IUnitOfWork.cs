namespace Lab11_ZeaBurga.Domain.Interfaces;

/// <summary>
/// Interfaz de Unidad de Trabajo (Contrato)
/// Administra una transacción y agrupa repositorios.
/// También hereda de IDisposable para gestionar el ciclo de vida del DbContext.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Obtiene una instancia de un repositorio genérico para una entidad específica.
    /// Esto asegura que todos los repositorios compartan el mismo DbContext y transacción.
    /// (Basado en el ejemplo del Lab 11: _unitOfWork.Repository<Empresa>())
    /// </summary>
    /// <typeparam name="T">La entidad del dominio</typeparam>
    IRepository<T> Repository<T>() where T : class;

    /// <summary>
    /// Guarda todos los cambios pendientes (inserciones, actualizaciones, eliminaciones)
    /// en la base de datos como una única transacción.
    /// </summary>
    /// <returns>El número de filas afectadas.</returns>
    Task<int> CompleteAsync(CancellationToken cancellationToken = default);
}