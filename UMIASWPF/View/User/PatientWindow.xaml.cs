using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UMIASWPF.Model;
using UMIASWPF.View.User.Pages;
using UMIASWPF.ViewModel.PatientViewModels;

namespace UMIASWPF.View.User
{
    /// <summary>
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>
    public partial class PatientWindow : Window
    {

        public PatientWindow()
        {
            InitializeComponent();
            DataContext = new PatientViewModel();
            Frame.Content = new AppointmentPage();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UnwrapButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void RollUpButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void TreeHandler(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue is TreeViewItem selectedItem && selectedItem.Header is string header)
            {
                Debug.WriteLine($"Selected item header: {header}");

                switch (header)
                {
                    case "Приёмы":
                        Frame.Content = new MedicalAppointmentsCardPage();
                        break;
                    case "Анализы":
                        Frame.Content = new MedCardAnalysesPage();
                        break;
                    case "Исследования":
                        Frame.Content = new MedicalResearchPage();
                        break;
                    case "Записи и направления":
                        Frame.Content = new MainPage();
                        break;
                    default:
                        // Добавьте обработку других элементов TreeView, если необходимо
                        break;
                }
            }
            else
            {
                // Добавьте здесь обработку случая, когда SelectedItem равен null или неожиданного типа
                Debug.WriteLine("Selected item is null or unexpected type.");
            }
        }




        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new ProfilePage();
        }
    }
}
