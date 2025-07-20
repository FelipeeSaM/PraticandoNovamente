using MediatR;

namespace Blocos.CQRS
{

    public interface ICommand : ICommand<Unit> { }

    public interface ICommand<out TResposta> : IRequest<TResposta>
    {
    }
}
