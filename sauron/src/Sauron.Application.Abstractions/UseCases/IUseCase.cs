using System.Threading.Tasks;

namespace Sauron.Application.Abstractions.UseCases
{
    public interface IUseCase<in TUseCaseInput>
    {
        Task Execute(TUseCaseInput input);
    }
}
