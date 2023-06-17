using Geodata.Application.Interfaces;
using MediatR;

namespace Geodata.Application.Geodata.Commands.DeleteGeodata;

public class DeleteGeodataCommandHandler : IRequestHandler<DeleteGeodataCommand>
{
    private readonly IGeodataDbContext _dbContext;

    public DeleteGeodataCommandHandler(IGeodataDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task<Unit> Handle(DeleteGeodataCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await _dbContext.GeodataEntities
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
            throw new ArgumentNullException();

        _dbContext.GeodataEntities.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
