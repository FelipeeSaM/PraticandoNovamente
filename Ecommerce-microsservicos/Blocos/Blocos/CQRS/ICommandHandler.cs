using MediatR;

namespace Blocos.CQRS
{
    public interface ICommandHandler<in TCommand, TResposta> : IRequestHandler<TCommand, TResposta>
        where TCommand : ICommand<TResposta>
        where TResposta : notnull
    {
    }

    public interface ICommandHandler<in TCommand> : ICommandHandler<TCommand, Unit>
        where TCommand : ICommand<Unit>
    {
    }
}
