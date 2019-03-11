using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorHosted.Shared
{
    public class Order
    {

        public int OrderID
        {
            get;set;
        }

        public DateTime OrderDate
        {
            get; set;
        }


        public string CompanyName
        {
            get; set;
        }

        public string EmployeeName
        {
            get; set;
        }

        public string CustomerID
        {
            get; set;
        }
    }
}
