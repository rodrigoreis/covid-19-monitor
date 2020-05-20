using System;

namespace Sauron.Application.Abstractions.UseCases
{
    public interface IInternalServerErrorOutputPort
    {
        void InternalServerError(Exception exception);
    }
}
