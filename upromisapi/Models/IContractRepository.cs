using APIUtils.APIMessaging;
using System.Linq;
using System.Threading.Tasks;

namespace upromiscontractapi.Models
{
    public interface IRepository<T>
    {
        IQueryable<T> List { get; }

        Task<APIResult<T>> Get(int id);

        Task<APIResult<T>> Post(SaveMessage<T> rec);
        Task<APIResult<T>> Put(SaveMessage<T> rec);
        Task<APIResult<T>> Delete(SaveMessage<T> rec);

    }


    public interface IContractRepository : IRepository<ContractDTO>
    {
    }
}