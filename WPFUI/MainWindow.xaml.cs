using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using BagageSorting_Engine.Events;
using BagageSorting_Engine.ViewModels;
using BagageSorting_Engine.TransportersAndSorters;
using System.Windows.Threading;

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ProgramSession programSession = new ProgramSession();
        private PassengersToCheckIn passengersToCheckIn = new PassengersToCheckIn();
        public MainWindow()
        {
            InitializeComponent();

            DataContext = programSession;

            programSession.BagageCreated += OnBagageCreated;
            passengersToCheckIn.BagageMoved += OnBagageMoved;

        }

        private void OnBagageMoved(object sender, EventArgs e)
        {
            if (e is ConveyorEventArgs)
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    PassengerList.ItemsSource = ((ConveyorEventArgs)e).PassengerList;
                    ProgramSession.PassengerList.RemoveAt(1); // (((ConveyorEventArgs)e).BagageItem);
                }));
            }
        }

        private void OnBagageCreated(object sender, EventArgs e)
        {
            if (e is PassengerEventArgs)
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    PassengerList.ItemsSource = ((PassengerEventArgs)e).PassengerList;
                    ProgramSession.PassengerList.Add(((PassengerEventArgs)e).BagageItem);
                }));
            }
        }

        private void OnClick_OpenCheckIn(object sender, RoutedEventArgs e)
        {
            programSession.OpenCheckIn();
        }
        private void OnClick_CloseCheckIn(object sender, RoutedEventArgs e)
        {
            programSession.CloseCheckIn();
        }
        private void OnClick_OpenGate(object sender, RoutedEventArgs e)
        {
            programSession.OpenGate();
        }
        private void OnClick_CloseGate(object sender, RoutedEventArgs e)
        {
            programSession.CloseGate();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            programSession.StartSession();
        }
    }
}
