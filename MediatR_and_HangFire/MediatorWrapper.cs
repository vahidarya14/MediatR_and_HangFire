using Hangfire;
using MediatR;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR_and_HangFire
{
    //برای اینکه بشه از DisplayName و AutomaticRetry استفاده کرد به این wraper نیاز بود
    public class MediatorWrapper
    {
        private readonly IMediator _mediator;
        private int _retryCount;
        public MediatorWrapper(IMediator mediator)
        {
            _mediator = mediator;
        }


        [DisplayName("{0}")]
        public async Task Send(string jobName, IRequest request, CancellationToken ct = default)
        => await _mediator.Send(request, ct);


        [DisplayName("{0}")]
        [AutomaticRetry(Attempts = 0)]
        public async Task SendWithNoRetry(string jobName, IRequest request, CancellationToken ct = default)
        => await _mediator.Send(request, ct);


        [DisplayName("{0}")]
        [AutomaticRetry(Attempts = 1)]
        public async Task SendWithOneRetry(string jobName, IRequest request, CancellationToken ct = default)
        => await _mediator.Send(request, ct);

    }

}
