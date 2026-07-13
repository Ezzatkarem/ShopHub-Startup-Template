using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DAL.Repo.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepo Products { get; }
        ICategoryRepo Categories { get; }
        
        // Save
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        // Transactions
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        Task RollbackTransactionAsync(CancellationToken cancellationToken = default);

        // Check
        bool HasChanges();
    }
}
