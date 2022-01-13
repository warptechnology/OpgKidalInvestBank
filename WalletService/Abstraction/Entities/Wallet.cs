using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WalletService.Abstraction.Entities
{
    [Table("wallets")]
    public record Wallet (
        [property: Column("id")] long Id,
        [property: Column("Ballance")] decimal Ballance
        );
    
    
}
