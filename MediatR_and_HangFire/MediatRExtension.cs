using Hangfire;
using MediatR;
using System.Threading;

namespace MediatR_and_HangFire
{
    public static class MediatRExtension
    {
        public static void Enqueue(this IMediator mediator, IRequest request, CancellationToken ct = default)
        {
            ////برای اینکه بشه از DisplayName و AutomaticRetry استفاده کرد به این wraper نیاز بود
            BackgroundJob.Enqueue<MediatorWrapper>((x) => x.Send(request.GetType().Name, request, ct));
        }

        public static void EnqueueWithOneRetry(this IMediator mediator, IRequest request, CancellationToken ct = default)
        {
            BackgroundJob.Enqueue<MediatorWrapper>((x) => x.SendWithOneRetry(request.GetType().Name, request, ct));
        }

        public static void EnqueueWithNoRetry(this IMediator mediator, IRequest request, CancellationToken ct = default)
        {
            BackgroundJob.Enqueue<MediatorWrapper>((x) => x.SendWithNoRetry(request.GetType().Name, request, ct));
        }


        //////اگر نمیخای از DisplayName و AutomaticRetry استفاده کنی میتونی مثل زیر بنویسی 
        //public static void Enqueue2(this IMediator mediator, IRequest request, CancellationToken ct = default)
        //{
        //    BackgroundJob.Enqueue(() => mediator.Send(request, ct));
        //}

    }

}
