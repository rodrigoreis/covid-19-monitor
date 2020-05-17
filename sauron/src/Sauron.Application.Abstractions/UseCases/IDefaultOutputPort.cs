namespace Sauron.Application.Abstractions.UseCases
{
    public interface IDefaultOutputPort<in TOutputPortModel>
    {
        void Respond(TOutputPortModel output);
    }
}
