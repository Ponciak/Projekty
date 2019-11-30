using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ShallowLibApp.Models;
using ShallowLibApp.Services;

namespace ShallowLibApp
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {

            CreateMap <LibraryItem, LibraryRepositorys>().ReverseMap();
            CreateMap<AuthorsItem, Autors>().ReverseMap();
        }
    }
}
