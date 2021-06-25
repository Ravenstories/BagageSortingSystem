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
using System.Diagnostics;

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
            programSession.ItemRemovedFromList += BagageLeftPassengerList;
            programSession.BagageMovedToCheckOutList += BagageMovedToCheckOut;

            programSession.MovedToConveyor += BagageMovedToConveyor;
            programSession.MovedFromConveyor += BagageMovedFromConveyor;
            
            programSession.CheckInOpenClosedEvent += CheckInOpenClose;
            programSession.GateOpenClosedEvent += GateOpenClose;

        }


        //Add's a random bagage to a list that random sorts to check in. 
        private void OnBagageCreated(object sender, EventArgs e)
        {
            if (e is BagageEventArgs)
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    PassengerList.ItemsSource = programSession.PassengerList;
                    programSession.PassengerList.Add(((BagageEventArgs)e).BagageItem);
                }));
            }
        }

        //Removes an elemnt from Passenger List
        private void BagageLeftPassengerList(object sender, EventArgs e)
        {
            if (e is BagageEventArgs)
            {

                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(() =>
                {
                    Debug.WriteLine(((BagageEventArgs)e).BagageItem.Name);
                    PassengerList.ItemsSource = programSession.PassengerList;
                    programSession.PassengerList.Remove(((BagageEventArgs)e).BagageItem);

                }));
                
            }
        }




        //Should add an element to the conveyor - Doesn't work
        private void BagageMovedToConveyor(object sender, EventArgs e)
        {
            if (e is ConveyorEventArgs)
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    ConveyorList.ItemsSource = programSession.Conveyor;

                    programSession.Conveyor.Add(((ConveyorEventArgs)e).BagageItem);

                }));
            }
        }


        private void BagageMovedFromConveyor(object sender, EventArgs e)
        {
            if (e is ConveyorEventArgs)
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    ConveyorList.ItemsSource = programSession.Conveyor; //Might be redundant

                    //ConveyorBelt.Conveyor[ConveyorBelt.ConveyorCounter] = ((ConveyorEventArgs)e).BagageItem;

                    programSession.Conveyor.Remove(((ConveyorEventArgs)e).BagageItem);

                }));
            }
        }

        
        //Move Item To A Global CheckOut List - Haven't checked if working yet
        private void BagageMovedToCheckOut(object sender, EventArgs e)
        {
            if (e is BagageEventArgs)
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    PlanePassengerList.ItemsSource = ProgramSession.CheckedOutList;
                    ProgramSession.CheckedOutList.Add(((BagageEventArgs)e).BagageItem);

                }));
            }
        }
        


        //Opens and closes CheckIns and Gates
        private void CheckInOpenClose(object sender, EventArgs e)
        {
            
            switch (((OpenClosedEvent)e).Number)
            {
                case 0:
                    if (((OpenClosedEvent)e).OpenClosed == true)
                    {
                        CheckInOne.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        CheckInOne.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 1:
                    if (((OpenClosedEvent)e).OpenClosed == true)
                    {
                        CheckInTwo.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        CheckInTwo.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 2:
                    if (((OpenClosedEvent)e).OpenClosed == true)
                    {
                        CheckInThree.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        CheckInThree.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 3:
                    if (((OpenClosedEvent)e).OpenClosed == true)
                    {
                        CheckInFour.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        CheckInFour.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 4:
                    if (((OpenClosedEvent)e).OpenClosed == true)
                    {
                        CheckInFive.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        CheckInFive.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 5:
                    if (((OpenClosedEvent)e).OpenClosed == true)
                    {
                        CheckInSix.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        CheckInSix.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 6:
                    if (((OpenClosedEvent)e).OpenClosed == true)
                    {
                        CheckInSeven.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        CheckInSeven.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 7:
                    if (((OpenClosedEvent)e).OpenClosed == true)
                    {
                        CheckInEight.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        CheckInEight.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 8:
                    if (((OpenClosedEvent)e).OpenClosed == true)
                    {
                        CheckInNine.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        CheckInNine.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 9:
                    if (((OpenClosedEvent)e).OpenClosed == true)
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
        private void GateOpenClose(object sender, EventArgs e)
        {

            switch (((OpenClosedEvent)e).Number)
            {
                case 0:
                    if (((OpenClosedEvent)e).OpenClosed == true)
                    {
                        GateOne.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        GateOne.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 1:
                    if (((OpenClosedEvent)e).OpenClosed == true)
                    {
                        GateTwo.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        GateTwo.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 2:
                    if (((OpenClosedEvent)e).OpenClosed == true)
                    {
                        GateThree.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        GateThree.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 3:
                    if (((OpenClosedEvent)e).OpenClosed == true)
                    {
                        GateFour.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        GateFour.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 4:
                    if (((OpenClosedEvent)e).OpenClosed == true)
                    {
                        GateFive.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        GateFive.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 5:
                    if (((OpenClosedEvent)e).OpenClosed == true)
                    {
                        GateSix.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        GateSix.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 6:
                    if (((OpenClosedEvent)e).OpenClosed == true)
                    {
                        GateSeven.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        GateSeven.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 7:
                    if (((OpenClosedEvent)e).OpenClosed == true)
                    {
                        GateEight.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        GateEight.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 8:
                    if (((OpenClosedEvent)e).OpenClosed == true)
                    {
                        GateNine.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        GateNine.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 9:
                    if (((OpenClosedEvent)e).OpenClosed == true)
                    {
                        GateTen.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        GateTen.Visibility = Visibility.Collapsed;
                    }
                    break;
                default:
                    break;
            }
        }

        //Buttons to close CheckIns and Gates
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
