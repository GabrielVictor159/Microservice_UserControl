using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace API.Controllers.User.DTO
{
    public class GetPaginationDTOValidation : AbstractValidator<GetPaginationDTO>
    {
        public GetPaginationDTOValidation()
        {
            RuleFor(e => e.pageIndex)
            .GreaterThan(0)
            .WithMessage("pageIndex must be equal to or greater than 1");
            RuleFor(e => e.pageSize)
            .GreaterThan(0)
            .WithMessage("pageSize must be equal to or greater than 1");
        }
    }
}