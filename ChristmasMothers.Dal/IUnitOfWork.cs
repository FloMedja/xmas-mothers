using System;
using System.Threading;
using System.Threading.Tasks;

namespace ChristmasMothers.Dal
{
    public interface IUnitOfWork<TContext> : IDisposable
    {
        TContext Context { get; }

        TRepository Repository<TRepository>();

        /// <summary>
        ///     Commits the changes made inside this unit of work.
        /// </summary>
        void Commit();

        /// <summary>
        ///     Asynchronously commits the changes made inside this unit of work.
        /// </summary>
        Task CommitAsync();

        /// <summary>
        ///     Asynchronously commits the changes made inside this unit of work.
        /// </summary>
        Task CommitAsync(CancellationToken cancellationToken);

        /// <summary>
        ///     Occurs when this unit of work is disposed.
        /// </summary>
        event EventHandler Disposing;
    }
}