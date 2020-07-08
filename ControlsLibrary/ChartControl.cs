using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlsLibrary
{
    public partial class ChartControl : Control
    {
        public Chart[] Data { get; set; }


        public ChartControl()
        {

            Data = new Chart[]
            {
            new Chart("2020", 1),
            new Chart("2021", 2),
            new Chart("2022", 3),
            };


            InitializeComponent();
        }

       


    

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);


            Graphics graphics = pe.Graphics;
            Rectangle clipRectangle = pe.ClipRectangle;

            var barHeight = clipRectangle.Height / Data.Length;  //-
            var maxValue = Data.Max(x => x.Value);
            var scalingFactor = clipRectangle.Width / maxValue;

            for(var i = 0; i < Data.Length; i++)
            {
                var barWidth = Data[i].Value * scalingFactor;
                var barX = clipRectangle.Width - barWidth;
                var barY = i * barHeight;

                graphics.FillRectangle(
                    Brushes.Green,
                    barX,
                    barY,
                    barWidth,
                    barHeight * 0.8f);
                    
            }
        }
    }
}
