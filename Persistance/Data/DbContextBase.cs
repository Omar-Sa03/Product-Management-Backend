using Domain.Common;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel.DataAnnotations;

namespace Persistance.Data
{
    /// <summary>
    /// DbContext base class
    /// </summary>
    /// <seealso cref="DbContext" />
    /// <seealso cref="IContext" />
    public class DbContextBase : DbContext
    {

        /// <summary>
        /// Gets the context identifier.
        /// </summary>
        /// <value>
        /// The context identifier.
        /// </value>
        public Guid? Id
        {
            get
            {
                return _id ?? (_id = Guid.NewGuid());
            }
        }
        private Guid? _id;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbContextBase"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="userService">The user service.</param>
        public DbContextBase(DbContextOptions options)
            : base(options)
        {

            // Default settings
            ChangeTracker.LazyLoadingEnabled = false;
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            int result;
            OnValidate();
            OnBeforeSaveChanges();
            result = await base.SaveChangesAsync(cancellationToken);
            OnAfterSaveChanges();

            return result;
        }

        /// <summary>
        /// Befores the SaveChanges.
        /// </summary>
        protected virtual void OnBeforeSaveChanges()
        {
        }

        /// <summary>
        /// After the SaveChanges.
        /// </summary>
        protected virtual void OnAfterSaveChanges()
        {
        }

        /// <summary>
        /// Validation before the SaveChanges
        /// </summary>
        protected virtual void OnValidate()
        {
            var entities = from e in ChangeTracker.Entries()
                           where e.State == EntityState.Added || e.State == EntityState.Modified
                           select e.Entity;

            foreach (var entity in entities)
            {
                var validationContext = new ValidationContext(entity);
                Validator.ValidateObject(entity, validationContext, validateAllProperties: true);
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging(true); // To show sql queries parameters values 
        }

        /// <summary>
        /// On model creating
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        /// <summary>
        /// Uses the auditable behaviour.
        /// </summary>
        protected virtual void UseAuditable()
        {

            // Change Created date & Modified date
            foreach (var entry in ChangeTracker.Entries<IAuditable>())
            {
                if (entry.Entity is IAuditable entity)
                {
                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedDate = DateTime.Now; // _dateTimeService.Now;
                    }

                    if (entry.State == EntityState.Modified)
                    {
                        entity.ModifiedDate = DateTime.Now; //_dateTimeService.Now;
                    }
                }
            }
        }

        /// <summary>
        /// Uses the soft delete behaviour.
        /// </summary>
        protected virtual void UseSoftDelete()
        {
            foreach (var entry in ChangeTracker.Entries<ISoftDelete>())
            {
                if (entry.Entity is ISoftDelete softDelete && entry.State == EntityState.Deleted)
                {
                    softDelete.IsDeleted = true;
                    softDelete.DeletedDate = DateTimeOffset.Now; //_dateTimeService.Now;
                    entry.State = EntityState.Modified;
                }
            }
        }
    }
}
