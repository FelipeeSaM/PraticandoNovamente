using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blocos.CQRS
{
    public interface IQuery<out TResposta> : IRequest<TResposta> where TResposta : notnull
    {
    }
}
