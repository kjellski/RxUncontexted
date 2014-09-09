using System.Diagnostics;
using System.Reactive;
using System.Reactive.Subjects;
using System.Windows;
using RxUncontexted;
using RxUncontexted.Parsing;

namespace ReactiveWpfFrontend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UncontextedDataDetailsViewModel _uncontextedDataDetailsViewModel;

        public MainWindow()
        {
            InitializeComponent();
            var changeSubject = InitializeWebsocket();
            InitializeDataContext(changeSubject);
        }

        private void InitializeDataContext(Subject<UncontextedData> changeSubject)
        {
            DataContext = this;
            UncontextedDataDetails.DataContext = new UncontextedDataDetailsViewModel(changeSubject);
        }

        private Subject<UncontextedData> InitializeWebsocket()
        {
            var websocketSubscriber = new UncontextedWebsocketSubscriber("ws://literature.uncontext.com:80");
            websocketSubscriber.ConnectAndStartReceivingToWebSocket();
            return websocketSubscriber.UncontextedData;
        }
    }
}