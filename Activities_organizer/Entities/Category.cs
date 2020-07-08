using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Activities_organizer.Entities
{   [Serializable]
    public class Category
    {
        public string CategoryName { get; set; }
        public string Domain { get; set; }
        public Activity Activity { get; set; }

        public Category(string categoryName, string domain)
        {
            CategoryName = categoryName;
            Domain = domain;
        }

        public Category(string categoryName, string domain, Activity activity) : this(categoryName, domain)
        {
            Activity = activity;
        }

        public Category()
        {

        }

    }
}
