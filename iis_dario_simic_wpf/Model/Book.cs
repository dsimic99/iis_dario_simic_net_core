using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace iis_dario_simic_wpf.Model
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/dario_simic_iis_web_api.Model")]
    class Book
    {
        [DataMember(Order = 0)]
        public int IDBook { get; set; }

        [DataMember(Order = 1)]
        public string Name { get; set; }

        [DataMember(Order = 2)]
        public int NumberOfPages { get; set; }

        [DataMember(Order = 3)]
        public double Price { get; set; }
    }
}
