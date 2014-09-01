using System.Collections.Generic;
using System.Windows;
using OxyPlot;
using RxUncontexted;

namespace ReactiveWpfFrontend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly UncontextedWebsocketSubscriber _uncontextedDataProvider;

        public MainWindow()
        {
            InitializeComponent();  

            _uncontextedDataProvider = new UncontextedWebsocketSubscriber("ws://literature.uncontext.com:80");
            InitializeWebsocket(_uncontextedDataProvider);
        }


        private void InitializeWebsocket(UncontextedWebsocketSubscriber websocketSubscriber)
        {
            websocketSubscriber.ConnectAndStartReceivingToWebSocket();
        }
    }
}