namespace Domain.Interfaces
{
    /// <summary>
    /// Unit of work interface.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets the context identifier.
        /// </summary>
        /// <value>
        /// The context identifier.
        /// </value>
        Guid? ContextId { get; }

        /// <summary>
        /// Saves the context asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing the asynchronous save operation, returning the number of affected entities.</returns>
        Task<int> SaveAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Bulks the insert asynchronous.
        /// </summary>
        /// <typeparam name="T">The type of entities to bulk insert.</typeparam>
        /// <param name="entities">The collection of entities to be inserted in bulk.</param>
        /// <returns>A task representing the asynchronous bulk insertion operation.</returns>
        Task BulkInsertAsync<T>(IEnumerable<T> entities)
            where T : class;
    }
}
