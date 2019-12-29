using System.Collections.Generic;
using System.Linq;

namespace WebApplicationReact.Models
{
    public interface IContractRepository
    {
        IQueryable<Contract> Contracts { get; }

        void AddContract(int i, Contract p);
    }
}