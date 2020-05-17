using System.Threading.Tasks;

namespace Sauron.Domain.Jobs
{
    public interface IJob
    {
        Task Execute();
    }
}
