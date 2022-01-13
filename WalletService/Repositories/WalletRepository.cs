using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WalletService.Abstraction;
using WalletService.Abstraction.Entities;
using WalletService.Common;

namespace WalletService.Repositories
{
    public class WalletRepository : RepositoryBase, IWalletRepository
    {
        public WalletRepository(IOptions<DbOptions> options) : base(options)
        {
            
        }
        public Task<IEnumerable<Wallet>> GetWalletsAsync(CancellationToken token)
        {
            return SelectAsync<Wallet>(token);
        }
    }
}
