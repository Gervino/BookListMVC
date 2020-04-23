using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BookListMVC.AppDbContext;
using BookListMVC.DTO;
using BookListMVC.Models;
using BookListMVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookListMVC.Controllers
{

    public class CitiesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CitiesController(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public City City { get; set;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(_db.Cities);
        }

        [HttpPost]
        public string Index(IEnumerable<City> cities)
        {
            if(cities.Count(x => x.IsSelected) == 0)
            {
                return "you didn'nt select any city";
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("You selected - ");
                foreach (City city in cities)
                {
                    if (city.IsSelected)
                    {
                        sb.Append(city.Name + ", ");
                    }
                }
                sb.Remove(sb.ToString().LastIndexOf(","), 1);
                return sb.ToString();
            }
        }
    }
}