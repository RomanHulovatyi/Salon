using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salon.Web.Profiles
{
    public class CustomerProfiler : Profile
    {
        public CustomerProfiler()
        {
            CreateMap<Entities.Models.Customer, DTO.Customer>();
        }
    }
}
