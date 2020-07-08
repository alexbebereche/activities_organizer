using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Activities_organizer.Entities
{   [Serializable]
    public class Project
    {

        #region Attributes

        public string ProjectName { get; set; }
        public int ProjectNoOfParticipants { get; set; }
        public List<Activity> Activities { get; set; }

        public Project(string projectName, int projectNoOfParticipants, List<Activity> activities)
        {
            ProjectName = projectName;
            ProjectNoOfParticipants = projectNoOfParticipants;
            Activities = activities;
        }

        //""
        public Project(string projectName , int projectNoOfParticipants)
        {
            ProjectName = projectName;
            ProjectNoOfParticipants = projectNoOfParticipants;
        }

        public Project()
        {

        }





        #endregion
    }
}
