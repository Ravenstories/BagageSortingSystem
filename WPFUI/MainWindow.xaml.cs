using System;
using System.Windows;
using System.Windows.Threading;
using BagageSorting_Engine.Events;
using BagageSorting_Engine.ViewModels;


namespace WPFUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Using a simple Mvvm idea with events instead of bindings.
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel viewModel = new ViewModel();
        public MainWindow()
        {
            InitializeComponent();

            DataContext = viewModel;

            viewModel.BagageCreatedAndMovedToList += OnBagageCreated;
            viewModel.BagageRemovedFromPassengerList += BagageLeftPassengerList;
            viewModel.BagageMovedToCheckOutList += BagageMovedToCheckOut;

            viewModel.MovedToConveyor += BagageMovedToConveyor;
            viewModel.MovedFromConveyor += BagageMovedFromConveyor;
            
            viewModel.CheckInOpenClosedEvent += CheckInOpenClose;
            viewModel.GateOpenClosedEvent += GateOpenClose;

            viewModel.PlaneEvent += PlaneEvent;
        }

        private void PlaneEvent(object sender, EventArgs e)
        {
            if (e is PlaneEventArgs)
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    if (((PlaneEventArgs)e).PlaneItem.IsPlaneAtGate == false)
                    {
                        Planes.Items.Remove(((PlaneEventArgs)e).PlaneItem);
                    }
                    else
                    {
                        Planes.Items.Add(((PlaneEventArgs)e).PlaneItem);
                    }
                }));
            }
        }


        //Add's a random bagage to a list that random sorts to check in. 
        private void OnBagageCreated(object sender, EventArgs e)
        {
            if (e is BagageEventArgs)
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    PassengerList.ItemsSource = viewModel.PassengerList;
                    viewModel.PassengerList.Add(((BagageEventArgs)e).BagageItem);
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
                    PassengerList.ItemsSource = viewModel.PassengerList;
                    viewModel.PassengerList.Remove(((BagageEventArgs)e).BagageItem);
                }));
                
            }
        }
        //Add an element to the conveyor
        private void BagageMovedToConveyor(object sender, EventArgs e)
        {
            if (e is BagageEventArgs)
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    ConveyorList.ItemsSource = ViewModel.Conveyor;
                    ViewModel.Conveyor.Add(((BagageEventArgs)e).BagageItem);

                }));
            }
        }
        private void BagageMovedFromConveyor(object sender, EventArgs e)
        {
            if (e is BagageEventArgs)
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    ViewModel.Conveyor.Remove(((BagageEventArgs)e).BagageItem);
                }));
            }
        }
        //Move Item To A Global CheckOut List 
        private void BagageMovedToCheckOut(object sender, EventArgs e)
        {
            if (e is BagageEventArgs)
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                {
                    PlanePassengerList.ItemsSource = ViewModel.CheckedOutList;
                    ViewModel.CheckedOutList.Add(((BagageEventArgs)e).BagageItem);

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
                        CheckInZero.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        CheckInZero.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 1:
                    if (((OpenClosedEvent)e).OpenClosed == true)
                    {
                        CheckInOne.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        CheckInOne.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 2:
                    if (((OpenClosedEvent)e).OpenClosed == true)
                    {
                        CheckInTwo.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        CheckInTwo.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 3:
                    if (((OpenClosedEvent)e).OpenClosed == true)
                    {
                        CheckInThree.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        CheckInThree.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 4:
                    if (((OpenClosedEvent)e).OpenClosed == true)
                    {
                        CheckInFour.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        CheckInFour.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 5:
                    if (((OpenClosedEvent)e).OpenClosed == true)
                    {
                        CheckInFive.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        CheckInFive.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 6:
                    if (((OpenClosedEvent)e).OpenClosed == true)
                    {
                        CheckInSix.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        CheckInSix.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 7:
                    if (((OpenClosedEvent)e).OpenClosed == true)
                    {
                        CheckInSeven.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        CheckInSeven.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 8:
                    if (((OpenClosedEvent)e).OpenClosed == true)
                    {
                        CheckInEight.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        CheckInEight.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 9:
                    if (((OpenClosedEvent)e).OpenClosed == true)
                    {
                        CheckInNine.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        CheckInNine.Visibility = Visibility.Collapsed;
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
                        GateZero.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        GateZero.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 1:
                    if (((OpenClosedEvent)e).OpenClosed == true)
                    {
                        GateOne.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        GateOne.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 2:
                    if (((OpenClosedEvent)e).OpenClosed == true)
                    {
                        GateTwo.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        GateTwo.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 3:
                    if (((OpenClosedEvent)e).OpenClosed == true)
                    {
                        GateThree.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        GateThree.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 4:
                    if (((OpenClosedEvent)e).OpenClosed == true)
                    {
                        GateFour.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        GateFour.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 5:
                    if (((OpenClosedEvent)e).OpenClosed == true)
                    {
                        GateFive.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        GateFive.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 6:
                    if (((OpenClosedEvent)e).OpenClosed == true)
                    {
                        GateSix.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        GateSix.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 7:
                    if (((OpenClosedEvent)e).OpenClosed == true)
                    {
                        GateSeven.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        GateSeven.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 8:
                    if (((OpenClosedEvent)e).OpenClosed == true)
                    {
                        GateEight.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        GateEight.Visibility = Visibility.Collapsed;
                    }
                    break;
                case 9:
                    if (((OpenClosedEvent)e).OpenClosed == true)
                    {
                        GateNine.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        GateNine.Visibility = Visibility.Collapsed;
                    }
                    break;
                default:
                    break;
            }
        }


        //Buttons to close CheckIns and Gates
        private void OnClick_OpenCheckIn(object sender, RoutedEventArgs e)
        {
            viewModel.OpenCheckIn();
        }
        private void OnClick_CloseCheckIn(object sender, RoutedEventArgs e)
        {
            viewModel.CloseCheckIn();
        }
        private void OnClick_OpenGate(object sender, RoutedEventArgs e)
        {
            viewModel.OpenGate();
        }
        private void OnClick_CloseGate(object sender, RoutedEventArgs e)
        {
            viewModel.CloseGate();
        }

        //Start Button
        private void OnClick_Start(object sender, RoutedEventArgs e)
        {
            viewModel.StartViewModel();
            CheckInPlusButton.Visibility = Visibility.Visible;
            CheckInMinusButton.Visibility = Visibility.Visible;
            GatePlusButton.Visibility = Visibility.Visible;
            GateMinusButton.Visibility = Visibility.Visible;
            Start.Visibility = Visibility.Collapsed;
        }
        private void OnClick_Exit(object sender, RoutedEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }
    }
}
