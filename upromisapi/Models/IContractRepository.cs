using System.Linq;

namespace upromiscontractapi.Models
{
    public interface IContractRepository
    {
        IQueryable<Contract> Contracts { get; }

        void AddContract(int i, Contract p);
    }
}