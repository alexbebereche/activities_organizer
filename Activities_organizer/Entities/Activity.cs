using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Activities_organizer.Entities
{   [Serializable]
    public class Activity:ICloneable, IComparable<Activity>
    {
        private DateTime date;

        public string ActivityName { get; set; }
        //public DateTime ActivityEndTime { get; set; }
        public DateTime ActivityEndTime {
            get {
                return date;
            }
            set {
                if(value > DateTime.Today.AddDays(365))
                {
                    throw new InvalidDate(value);
                }

                date = value;

            }
            }


        public Category Category { get; set; }
        public Project Project { get; set; }

        
        //!!!
        public Activity(string activityName, DateTime activityEndTime)
        {
            ActivityName = activityName;
            ActivityEndTime = activityEndTime;
        }



        //public Activity(string activityName, DateTime activityEndTime, string domain, string category) : this(activityName, activityEndTime)
        //{
        //    Domain = domain;
        //    Category = category;
        //}

        public Activity(string activityName, DateTime activityEndTime, Category category) : this(activityName, activityEndTime)
        {
            Category = category;
        }

        public Activity(string activityName, DateTime activityEndTime, Category category, Project project) : this(activityName, activityEndTime)
        {
            Category = category;
            Project = project;
        }

        public object Clone()
        {
            var clone = (Activity)MemberwiseClone();

            clone.ActivityName = (String)ActivityName.Clone();
            clone.Category = (Category)Category;
            clone.ActivityEndTime = (DateTime)ActivityEndTime;

            return clone;
        }


        public int CompareTo(Activity other)
        {
            if(Category.Domain == other.Category.Domain)
            {
                return Category.Domain.CompareTo(other.Category.Domain);
            }
            else
            {
                return Category.CategoryName.CompareTo(other.Category.CategoryName);
            }
        }
    }
}
