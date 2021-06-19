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
using BagageSorting_Engine.ViewModels;


namespace WPFUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ProgramSession programSession = new ProgramSession();
        public MainWindow()
        {
            InitializeComponent();
            programSession.StartSession();

            DataContext = programSession;
        }

        private void OnClick_OpenCheckIn()
        {
            programSession.OpenCheckIn();
        }
        private void OnClick_CloseCheckIn()
        {
            programSession.CloseCheckIn();
        }
        private void OnClick_OpenGate()
        {
            programSession.OpenGate();
        }
        private void OnClick_CloseGate()
        {
            programSession.CloseGate();
        }

        private void GatePlusButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
