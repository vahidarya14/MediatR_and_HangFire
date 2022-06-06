using MediatR;
using MediatR_and_HangFire;
using Microsoft.AspNetCore.Mvc;

namespace WebDotnet6.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public IActionResult Index() => View();

        [HttpGet("Home/order/{id?}")]
        public async Task<IActionResult> order(int id, CancellationToken ct)
        {
            await _mediator.Send(new PalceOrder
            {
                OrderId = id,
                source = "mediatR"
            }, ct);

            return View();
        }

        [HttpGet("Home/order2/{id?}")]
        public IActionResult order2(int id, CancellationToken ct)
        {
            _mediator.EnqueueWithOneRetry(
                new PalceOrder
                {
                    OrderId = id,
                    source = "hangfire"
                }, ct);

            return View();
        }

    }

}