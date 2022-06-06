using MediatR;

namespace WebDotnet6.Controllers
{
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
            throw new NotImplementedException();
        }
    }

}