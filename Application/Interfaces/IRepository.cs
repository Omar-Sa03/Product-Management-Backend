namespace Application.Interfaces
{
    /// <summary>
    /// Generic repository interface
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    public interface IRepository<T> : IReadOnlyRepository<T>
    {
        /// <summary>
        /// Add an entity in the context.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        [Obsolete("Use Add or Update version instead AddOrUpdate")]
        Task AddOrUpdateAsync(T entity, CancellationToken cancellationToken);

        /// <summary>
        /// Add a collection of entities in the context.
        /// </summary>
        /// <param name="entities">The entities to add or update.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        [Obsolete("Use Add or Update version instead AddOrUpdate")]
        Task AddOrUpdateAsync(IEnumerable<T> entities, CancellationToken cancellationToken);

        /// <summary>
        /// Add an entity in the context.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task AddAsync(T entity, CancellationToken cancellationToken);

        /// <summary>
        /// Add a collection of entities in the context.
        /// </summary>
        /// <param name="entities">The entities to add or update.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task AddAsync(IEnumerable<T> entities, CancellationToken cancellationToken);

        /// <summary>
        /// Update an entity in the context.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task UpdateAsync(T entity, CancellationToken cancellationToken);

        /// <summary>
        /// Update a collection of entities in the context.
        /// </summary>
        /// <param name="entities">The entities to add or update.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        Task UpdateAsync(IEnumerable<T> entities, CancellationToken cancellationToken);

        /// <summary>
        /// Removes the entity from the context.
        /// </summary>
        /// <param name="keyValues">The key values.</param>
        void RemoveByKey(params object[] keyValues);

        /// <summary>
        /// Removes the specified entity from the context.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        void Remove(T entity);

        /// <summary>
        /// Removes the specified entities from the context.
        /// </summary>
        /// <param name="entities">The entities to remove.</param>
        void Remove(IEnumerable<T> entities);
    }
}