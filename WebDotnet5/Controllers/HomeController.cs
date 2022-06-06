using MediatR;
using MediatR_and_HangFire;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace WebDotnet5.Controllers
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

    public class PalceOrder : IRequest
    {
        public int OrderId { get; set; }
        public string source { get; set; }
    }

    public class PalceOrderHandler : IRequestHandler<PalceOrder>
    {
        AppDbContext _db;

        public PalceOrderHandler(AppDbContext appDbContext)
        {
            _db = appDbContext;
        }


        public async Task<Unit> Handle(PalceOrder request, CancellationToken ct)
        {
            for (int i = 1; i < 6; i++)
            {
                await Task.Delay(3000, ct);
                await _db.Orders.AddAsync(new Order
                {
                    CustomerName = $"{request.source}_{i}",
                });
                await _db.SaveChangesAsync();
            }
            //return Unit.Value;
            throw new System.NotImplementedException();
        }
    }
}
