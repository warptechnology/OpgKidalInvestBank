using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WalletService.Abstraction.Entities;

namespace WalletService.Abstraction
{
    public interface IWalletRepository
    {
        Task<IEnumerable<Wallet>> GetWalletsAsync(CancellationToken token);
    }

}
