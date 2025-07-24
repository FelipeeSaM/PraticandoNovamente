using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blocos.CQRS
{
    public interface IQueryHandler<in TQuery, TResposta> : IRequestHandler<TQuery, TResposta>
        where TQuery : IQuery<TResposta>
        where TResposta : notnull
    {
    }
}
