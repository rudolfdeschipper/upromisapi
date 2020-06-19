using APIUtils.APIMessaging;
using System.Linq;
using System.Threading.Tasks;

namespace upromiscontractapi.Models
{
    public interface IRepository<T>
    {
        IQueryable<T> List { get; }

        Task<T> Get(int id);

        Task<T> Post(SaveMessage<T> rec);
        Task<T> Put(SaveMessage<T> rec);
        Task<bool> Delete(SaveMessage<T> rec);

    }


    public interface IContractRepository : IRepository<ContractDTO>
    {
    }
}