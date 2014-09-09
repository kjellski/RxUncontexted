using System.Collections.Generic;
using OxyPlot;

namespace ReactiveWpfFrontend
{
    class PlotViewModel
    {
        private string _title;
        private List<DataPoint> _points;

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


        public string Title
        {
            get { return _title; }
            private set { _title = value; }
        }

        public List<DataPoint> Points
        {
            get { return _points; }
            private set { _points = value; }
        }
    }
}
