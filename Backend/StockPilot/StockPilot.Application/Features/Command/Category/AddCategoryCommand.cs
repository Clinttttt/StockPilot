using MediatR;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Command.Category
{
    public record AddCategoryCommand(string name, string description) : IRequest<Result>;
    
}
