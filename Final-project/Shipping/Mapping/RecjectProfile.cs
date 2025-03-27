using AutoMapper;
using Shipping.core.Models;
using Shipping.DTO.Rejection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Shipping.Mapping
{
    public class RecjectProfile : Profile
    {
        public RecjectProfile()
        {
            CreateMap<Rejection, RejectionDto>().ReverseMap();


        }
    }
}
