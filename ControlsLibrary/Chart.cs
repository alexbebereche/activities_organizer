using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlsLibrary
{
    public class Chart
    {
        public string Label { get; set; }
        public float Value { get; set; }


        public Chart(string label, float value)
        {
            Label = label;
            Value = value;
        }

      
    }
}
