using Lab11_ZeaBurga.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lab11_ZeaBurga.Infrastructure.Persistence;

/// <summary>
/// DbContext que representa la sesión con la base de datos "TicketeraBD".
/// Actúa como el "mapa" principal entre las entidades de dominio y la base de datos.
/// </summary>
public class TicketeraDbContext : DbContext
{
    /// <summary>
    /// Constructor requerido por la Inyección de Dependencias (DI) para pasar las opciones de configuración.
    /// </summary>
    public TicketeraDbContext(DbContextOptions<TicketeraDbContext> options) : base(options)
    {
    }

    // --- Tablas (DbSet) ---
    // Define las colecciones de entidades que EF Core debe rastrear.

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Response> Responses { get; set; }

    /// <summary>
    /// Método principal de configuración del modelo (el "mapeo").
    /// Aquí definimos las llaves primarias, nombres de tablas, columnas, relaciones, etc.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // --- Configuración de la entidad User ---
        modelBuilder.Entity<User>(p =>
        {
            p.ToTable("users"); // Nombre de la tabla
            p.HasKey(u => u.UserId); // Llave primaria

            // Mapeo de propiedades a columnas
            p.Property(u => u.UserId).HasColumnName("user_id");
            p.Property(u => u.Username).HasColumnName("username").IsRequired().HasMaxLength(100);
            p.HasIndex(u => u.Username).IsUnique(); // Restricción UNIQUE
            
            p.Property(u => u.PasswordHash).HasColumnName("password_hash").IsRequired().HasMaxLength(255);
            p.Property(u => u.Email).HasColumnName("email").HasMaxLength(150);
            p.HasIndex(u => u.Email).IsUnique(); // Restricción UNIQUE
            
            p.Property(u => u.CreatedAt).HasColumnName("created_at");
        });

        // --- Configuración de la entidad Role ---
        modelBuilder.Entity<Role>(p =>
        {
            p.ToTable("roles");
            p.HasKey(r => r.RoleId);
            p.Property(r => r.RoleId).HasColumnName("role_id");
            p.Property(r => r.RoleName).HasColumnName("role_name").IsRequired().HasMaxLength(50);
            p.HasIndex(r => r.RoleName).IsUnique();
        });

        // --- Configuración de la entidad UserRole (Tabla Pivote Muchos-a-Muchos) ---
        modelBuilder.Entity<UserRole>(p =>
        {
            p.ToTable("user_roles");
            p.HasKey(ur => new { ur.UserId, ur.RoleId }); // Llave primaria compuesta
            
            p.Property(ur => ur.UserId).HasColumnName("user_id");
            p.Property(ur => ur.RoleId).HasColumnName("role_id");
            p.Property(ur => ur.AssignedAt).HasColumnName("assigned_at");

            // Relación: UserRole -> User
            p.HasOne(ur => ur.User)
             .WithMany(u => u.UserRoles)
             .HasForeignKey(ur => ur.UserId)
             .OnDelete(DeleteBehavior.Cascade); // Coincide con el ON DELETE CASCADE del SQL

            // Relación: UserRole -> Role
            p.HasOne(ur => ur.Role)
             .WithMany(r => r.UserRoles)
             .HasForeignKey(ur => ur.RoleId)
             .OnDelete(DeleteBehavior.Cascade); // Coincide con el ON DELETE CASCADE del SQL
        });

        // --- Configuración de la entidad Ticket ---
        modelBuilder.Entity<Ticket>(p =>
        {
            p.ToTable("tickets");
            p.HasKey(t => t.TicketId);
            p.Property(t => t.TicketId).HasColumnName("ticket_id");
            p.Property(t => t.UserId).HasColumnName("user_id");
            p.Property(t => t.Title).HasColumnName("title").IsRequired().HasMaxLength(255);
            p.Property(t => t.Description).HasColumnName("description");
            
            // Nota: La restricción CHECK (status IN (...)) se maneja a nivel de aplicación o con un Enum.
            p.Property(t => t.Status).HasColumnName("status").IsRequired().HasMaxLength(20);
            
            p.Property(t => t.CreatedAt).HasColumnName("created_at");
            p.Property(t => t.ClosedAt).HasColumnName("closed_at");

            // Relación: Ticket -> User (El User que creó el ticket)
            p.HasOne(t => t.User)
             .WithMany(u => u.Tickets) // Un usuario puede tener muchos tickets
             .HasForeignKey(t => t.UserId);
        });

        // --- Configuración de la entidad Response ---
        modelBuilder.Entity<Response>(p =>
        {
            p.ToTable("responses");
            p.HasKey(r => r.ResponseId);
            p.Property(r => r.ResponseId).HasColumnName("response_id");
            p.Property(r => r.TicketId).HasColumnName("ticket_id");
            p.Property(r => r.ResponderId).HasColumnName("responder_id"); // El User que responde
            p.Property(r => r.Message).HasColumnName("message").IsRequired();
            p.Property(r => r.CreatedAt).HasColumnName("created_at");

            // Relación: Response -> Ticket
            p.HasOne(r => r.Ticket)
             .WithMany(t => t.Responses) // Un ticket puede tener muchas respuestas
             .HasForeignKey(r => r.TicketId)
             .OnDelete(DeleteBehavior.Cascade); // Coincide con el ON DELETE CASCADE del SQL

            // Relación: Response -> User (El User que respondió)
            p.HasOne(r => r.Responder)
             .WithMany(u => u.Responses) // Un usuario puede escribir muchas respuestas
             .HasForeignKey(r => r.ResponderId);
        });
    }
}

