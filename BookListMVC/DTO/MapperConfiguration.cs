using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookListMVC.Models;

namespace BookListMVC.DTO
{
    public class MapperConfiguration:Profile
    {
        public MapperConfiguration()
        {
            CreateMap<BookDTO, Book>();
        }
    }
}
