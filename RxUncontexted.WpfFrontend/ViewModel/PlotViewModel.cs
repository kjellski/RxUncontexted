﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;

namespace ReactiveWpfFrontend.ViewModel
{
    class PlotViewModel
    {
        public PlotViewModel()
        {
            Points = new List<DataPoint>
            {
                new DataPoint(0, 4),
                new DataPoint(10, 13),
                new DataPoint(20, 15),
                new DataPoint(30, 16),
                new DataPoint(40, 12),
                new DataPoint(50, 12)
            };
            Title = "test!";
        }

        public string Title { get; private set; }
        public IList<DataPoint> Points { get; private set; }

    }
}
