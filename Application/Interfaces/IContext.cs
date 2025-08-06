namespace Application.Interfaces
{
    /// <summary>
    /// Interface de base du context de persistence. Utile pour les tests unitaires
    /// </summary>
    public interface IContext : IDisposable
    {
        /// <summary>
        /// Sauvegarde les changements courants du context
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
