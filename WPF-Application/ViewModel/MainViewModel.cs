using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_Application.Model;
using WPF_Application.Repositories;

namespace WPF_Application.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private UserAccountModel _currentUserAccount;

        private ViewModelBase _currentChildView;
        private string _caption;
        private IconChar _icon;

        private UserRepository _userRepository;

        public UserAccountModel CurrentUserAccount
        {
            get => _currentUserAccount;
            set
            {
                _currentUserAccount = value;
                OnPropertyChanged(nameof(CurrentUserAccount));
            }
        }

        public ViewModelBase CurrentChildView
        {
            get => _currentChildView;
            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }

        public string Caption
        {
            get => _caption;
            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }

        public IconChar Icon
        {
            get => _icon;
            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }

        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowCustomersViewCommand { get; }

        public MainViewModel()
        {
            _userRepository = new UserRepository();
            LoadCurrentUserData();

            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
            ShowCustomersViewCommand = new ViewModelCommand(ExecuteShowCustomersViewCommand);

            ExecuteShowHomeViewCommand(null);
        }

        private void ExecuteShowCustomersViewCommand(object obj)
        {
            CurrentChildView = new CustomersViewModel();

            Caption = "Customers";
            Icon = IconChar.Cat;
        }

        private void ExecuteShowHomeViewCommand(object obj)
        {
            CurrentChildView = new HomeViewModel();

            Caption = "Dashboard";
            Icon = IconChar.Home;
        }

        private void LoadCurrentUserData()
        {
            try
            {
                var user = _userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
                if (user is not null)
                {
                    CurrentUserAccount = new UserAccountModel()
                    {
                        Username = user.Username,
                        DisplayName = $"{user.Name} {user.LastName}",
                        ProfilePicture = null
                    };
                }
                else
                {
                    CurrentUserAccount = GetDefault();
                }
            }
            catch
            {
                CurrentUserAccount = GetDefault();
            }

            UserAccountModel GetDefault()
            {
                return new UserAccountModel() { Username = "Invalid", DisplayName = "Invalid" };
            }
        }
    }
}
