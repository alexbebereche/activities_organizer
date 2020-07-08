using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Activities_organizer.Entities
{
    public class InvalidDate: Exception
    {
        public DateTime Date { get; set; }

        public InvalidDate(DateTime date)
        {
            Date = date ;
        }

       




    }
}
