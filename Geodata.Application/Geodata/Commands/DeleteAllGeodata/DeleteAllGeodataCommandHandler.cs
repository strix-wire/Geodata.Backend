using Geodata.Application.Geodata.Commands.DeleteGeodata;
using Geodata.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geodata.Application.Geodata.Commands.DeleteAllGeodata;

public class DeleteGeodataCommandHandler : IRequestHandler<DeleteAllGeodataCommand>
{
    private readonly IGeodataDbContext _dbContext;

    public DeleteGeodataCommandHandler(IGeodataDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task<Unit> Handle(DeleteAllGeodataCommand request,
        CancellationToken cancellationToken)
    {
        var listEntity = await _dbContext.GeodataEntities.ToListAsync();

        if (listEntity == null)
            //to (never) do create class exception
            //throw new NotFoundException(nameof(GeoEventConsidered), request.Id);
            throw new ArgumentNullException();

        foreach (var i in listEntity)
            _dbContext.GeodataEntities.Remove(i);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
