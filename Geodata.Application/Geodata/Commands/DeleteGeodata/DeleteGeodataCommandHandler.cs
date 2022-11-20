using Geodata.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            //to (never) do create class exception
            //throw new NotFoundException(nameof(GeoEventConsidered), request.Id);
            throw new ArgumentNullException();

        _dbContext.GeodataEntities.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
