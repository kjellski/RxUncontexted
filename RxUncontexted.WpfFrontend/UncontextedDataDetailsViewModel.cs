using System;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Subjects;
using ReactiveUI;
using RxUncontexted.Parsing;

namespace ReactiveWpfFrontend
{
    internal class UncontextedDataDetailsViewModel : ReactiveObject
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private long _millisecondsSinceLastUpdate;
        private UncontextedData _uncontextedDataPoint;
        private string _millisecondsSinceLastUpdateFormatted;

        public UncontextedDataDetailsViewModel(Subject<UncontextedData> changeSubject)
        {
            InitializeData();
            var updateObserver = Observer.Create<UncontextedData>(data =>
            {
                MillisecondsSinceLastUpdate = _stopwatch.ElapsedMilliseconds;
                MillisecondsSinceLastUpdateFormatted = String.Format("Update took {0}ms", MillisecondsSinceLastUpdate);
                UncontextedDataPoint = data;
                _stopwatch.Restart();
            });

            changeSubject.Subscribe(updateObserver);
        }

        public String MillisecondsSinceLastUpdateFormatted
        {
            get { return _millisecondsSinceLastUpdateFormatted; }
            set { this.RaiseAndSetIfChanged(ref _millisecondsSinceLastUpdateFormatted, value); }
        }

        public long MillisecondsSinceLastUpdate
        {
            get { return _millisecondsSinceLastUpdate; }
            set { this.RaiseAndSetIfChanged(ref _millisecondsSinceLastUpdate, value); }
        }

        public UncontextedData UncontextedDataPoint
        {
            get { return _uncontextedDataPoint; }
            private set { this.RaiseAndSetIfChanged(ref _uncontextedDataPoint, value); }
        }

        private void InitializeData()
        {
            _stopwatch.Start();
            _millisecondsSinceLastUpdate = 0;
            _uncontextedDataPoint = new UncontextedData
            {
                A = 0,
                B = 13.37f,
                C = 1,
                D = 2,
                E = new E
                {
                    F = 42,
                    G = 21
                }
            };
        }
    }
}