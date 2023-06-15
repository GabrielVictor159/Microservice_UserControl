using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Infraestructure.Database.Entities;

namespace API.Controllers.User.DTO
{
    public class GetPaginationDTO
    {
        public int pageIndex { get; set; } = 1;
        public int pageSize { get; set; } = 10;
        public Users? searchUser { get; set; } = null;
    }
}