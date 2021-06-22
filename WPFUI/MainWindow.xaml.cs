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
using System.Windows.Threading;
using BagageSorting_Engine.Models;
using BagageSorting_Engine.Events;
using BagageSorting_Engine.ViewModels;
using BagageSorting_Engine.TransportersAndSorters;
using System.Collections.ObjectModel;

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ProgramSession programSession = new ProgramSession();
        private ConveyorBelt conveyorBelt = new ConveyorBelt();
        public MainWindow()
        {
            InitializeComponent();

            DataContext = programSession;

            programSession.BagageCreated += OnBagageCreated;
            programSession.BagageMovedFromPassengerList += BagageLeftPassengerList;
            programSession.BagageMovedToCheckOutList += BagageMovedToCheckOut;
            programSession.MovedToConveyor += BagageMovedToConveyor;
            
            programSession.IsOpenEvent += OnCheckIn;

        }

        //Add's a random bagage to a list that random sorts to check in. 
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

        //Should remove an elemnt from Passenger List - Doesn't Work
        private void BagageLeftPassengerList(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
            {
                PassengerList.ItemsSource = ((PassengerEventArgs)e).PassengerList;
                ProgramSession.PassengerList.Remove(((PassengerEventArgs)e).BagageItem);

            }));
        }

        //Should add an element to the conveyor - Doesn't work
        private void BagageMovedToConveyor(object sender, EventArgs e)
        {

            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
            {
                Conveyor.ItemsSource = ((ConveyorEventArgs)e).Conveyor;

                conveyorBelt.Conveyor[ConveyorBelt.ConveyorCounter] = ((ConveyorEventArgs)e).BagageItem;
            
                ProgramSession.Conveyor.Add(((ConveyorEventArgs)e).BagageItem);

            }));
        }

        
        //Move Item To A Global CheckOut List - Haven't checked if working yet
        private void BagageMovedToCheckOut(object sender, EventArgs e)
        {
            if (e is PassengerEventArgs)
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    PlanePassengerList.ItemsSource = ((PassengerEventArgs)e).PassengerList;
                    ProgramSession.CheckedOutList.Add(((PassengerEventArgs)e).BagageItem);

                }));
            }
        }
        

        //Opens and closes CheckIns
        private void OnCheckIn(object sender, EventArgs e)
        {
            
            switch (((CheckInOpenEvent)e).CheckInNumber)
            {
                case 0:
                    if (((CheckInOpenEvent)e).IsOpen == true)
                    {
                        CheckInOne.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        CheckInOne.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 1:
                    if (((CheckInOpenEvent)e).IsOpen == true)
                    {
                        CheckInTwo.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        CheckInTwo.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 2:
                    if (((CheckInOpenEvent)e).IsOpen == true)
                    {
                        CheckInThree.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        CheckInThree.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 3:
                    if (((CheckInOpenEvent)e).IsOpen == true)
                    {
                        CheckInFour.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        CheckInFour.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 4:
                    if (((CheckInOpenEvent)e).IsOpen == true)
                    {
                        CheckInFive.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        CheckInFive.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 5:
                    if (((CheckInOpenEvent)e).IsOpen == true)
                    {
                        CheckInSix.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        CheckInSix.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 6:
                    if (((CheckInOpenEvent)e).IsOpen == true)
                    {
                        CheckInSeven.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        CheckInSeven.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 7:
                    if (((CheckInOpenEvent)e).IsOpen == true)
                    {
                        CheckInEight.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        CheckInEight.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 8:
                    if (((CheckInOpenEvent)e).IsOpen == true)
                    {
                        CheckInNine.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        CheckInNine.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 9:
                    if (((CheckInOpenEvent)e).IsOpen == true)
                    {
                        CheckInTen.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        CheckInTen.Visibility = Visibility.Collapsed;
                    }
                    break;
                default:
                    break;
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

        //Start Button
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            programSession.StartSession();
        }
    }
}
