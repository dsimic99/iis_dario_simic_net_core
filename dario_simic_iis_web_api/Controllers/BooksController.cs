using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dario_simic_iis_web_api.Model;
using System.Xml;

namespace dario_simic_iis_web_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        //implementirati httpsresponse 201 za insert npr:
        [HttpPost]
        public bool Post(Book b)
        {
            if (b.Name != null)
                return true;

            else
                return false;
        }
        [HttpGet]
        public Book Get()
        {
            
            Book b = new Book();
            b.IDBook = 1;
            b.Name = "Help";
            b.NumberOfPages = 543;
            b.Price = 4.99;

            
            return b;
        }


    }
}
