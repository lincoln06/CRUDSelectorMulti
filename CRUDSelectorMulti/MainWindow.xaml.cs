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

namespace CRUDSelectorMulti
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void LoginBtn_Click()
        {
            var selectedCrud = CheckSelection();
            OutputTbx.Clear();
            OutputTbx.Text = LoginProcess(selectedCrud, EmailTbx.Text.ToString(), Coder.Encrypt(PasswordPbx.Password, EmailTbx.Text));
        }
        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            OutputTbx.IsEnabled = false;
            FirstNameTbx.Clear();
            LastNameTbx.Clear();
            EmailTbx.Clear();
            PasswordPbx.Clear();
            OutputTbx.Clear();
            ChooseCrudCbx.SelectedItem = null;
            ChooseActionCbx.SelectedItem = null;
        }
        private void RegisterBtn_Click()
        {
            var selectedCrud = CheckSelection();
            User user = new(FirstNameTbx.Text, LastNameTbx.Text, EmailTbx.Text, PasswordPbx.Password);
            OutputTbx.Clear();
            OutputTbx.Text = RegistrationProcess(selectedCrud, user);
        }
        private IElement CheckSelection()
        {
            switch (ChooseCrudCbx.SelectedIndex)
            {
                case 0:
                    return new MongoCRUD();
                    break;
                case 1:
                    return new SqliteCRUD();
                    break;
            }
            return null;
        }
        private string RegistrationProcess<T>(T selectedCrud, User user) where T : IElement
        {
            selectedCrud.Register(user);
            return $"Użytkownik został dodany do {ChooseCrudCbx.Text}";
        }
        private string LoginProcess<T>(T selectedCrud, string email, string password) where T : IElement
        {
            User user = selectedCrud.Login(email, password);
            if (user == null) return "Brak danych";
            string outputMessage =
                "Dane użytkownika:\n" +
                new string('-', 40) +
                $"\nImię\t\t{user.FirstName}\n" +
                $"Nazwisko\t{user.LastName}\n" +
                $"E-mail:\t\t{user.Email}\n" +
                new string('-', 40) +
                $"\nZalogowano przy użyciu {ChooseCrudCbx.Text}";
            return outputMessage;
        }
        private void ProcessBtn_Click(object sender, RoutedEventArgs e)
        {
            switch (ChooseActionCbx.SelectedIndex)
            {
                case 0:
                    LoginBtn_Click();
                    break;
                case 1:
                    RegisterBtn_Click();
                    break;
            }
        }
        private void ChooseCrudCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cbxControl = sender as ComboBox;
            switch (cbxControl.SelectedIndex)
            {
                case 0:
                case 1:
                    ProcessBtn.Visibility = Visibility.Visible;
                    break;
                default:
                    ProcessBtn.Visibility = Visibility.Hidden;
                    break;
            }
        }
        private void ChooseActionCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cbxControl = sender as ComboBox;
            switch (cbxControl.SelectedIndex)
            {
                case 0:
                    FirstNameTbx.IsEnabled = false;
                    LastNameTbx.IsEnabled = false;
                    EmailTbx.IsEnabled = true;
                    PasswordPbx.IsEnabled = true;
                    ChooseCrudCbx.IsEnabled = true;
                    break;
                case 1:
                    FirstNameTbx.IsEnabled = true;
                    LastNameTbx.IsEnabled = true;
                    EmailTbx.IsEnabled = true;
                    PasswordPbx.IsEnabled = true;
                    ChooseCrudCbx.IsEnabled = true;
                    break;
                default:
                    FirstNameTbx.IsEnabled = false;
                    LastNameTbx.IsEnabled = false;
                    EmailTbx.IsEnabled = false; ;
                    PasswordPbx.IsEnabled = false;
                    ChooseCrudCbx.IsEnabled = false;
                    break;
            }
        }
    }
}