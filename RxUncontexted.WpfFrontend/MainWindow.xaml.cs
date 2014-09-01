using System;
using System.Reactive;
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