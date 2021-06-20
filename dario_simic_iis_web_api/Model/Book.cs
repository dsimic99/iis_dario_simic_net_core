using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace dario_simic_iis_web_api.Model
{
    [DataContract]
    public class Book
    {
        [DataMember(Order = 0)]
        public int IDBook { get; set; }
        [DataMember(Order = 1)]
        public string Name { get; set; }
        [DataMember(Order = 2)]
        public int NumberOfPages { get; set; }
        [DataMember(Order = 3)]
        public double Price { get; set; }

        public Book(int iDBook, string name, int numberOfPages, double price)
        {
            IDBook = iDBook;
            Name = name;
            NumberOfPages = numberOfPages;
            Price = price;
        }

        public Book()
        {
        }
    }
}
