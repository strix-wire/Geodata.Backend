using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geodata.Application.Geodata.Commands.DeleteAllGeodata;

public class DeleteAllGeodataCommand : IRequest
{
    public Guid UserId { get; set; }
}
