using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_Application.Repositories;

namespace WPF_Application.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private string _username = "Username";
        private SecureString _password;
        private string _errorMessage;
        private bool _isVisibleView = true;

        private IUserRepository _userRepository;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public SecureString Password 
        { 
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string ErrorMessage 
        { 
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public bool IsVisibleView 
        { 
            get => _isVisibleView;
            set
            {
                _isVisibleView = value;
                OnPropertyChanged(nameof(IsVisibleView));
            }
        }

        public ICommand LoginCommand { get; }
        public ICommand RecoverPassowrdCommand { get; }
        public ICommand ShowPassowrdCommand { get; }
        public ICommand RememberPassowrdCommand { get; }

        public LoginViewModel()
        {
            _userRepository = new UserRepository();

            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            RecoverPassowrdCommand = new ViewModelCommand(e => ExecuteRecoverPasswordCommand("",""));
        }

        private bool CanExecuteLoginCommand(object obj)
        {
            bool invalidData = string.IsNullOrEmpty(Username) || Password is null || Password.Length < 3;
            return !invalidData;

        }

        private void ExecuteLoginCommand(object obj)
        {
            var isValidUser = _userRepository.AuthenticateUser(new NetworkCredential(Username, Password));
            if (isValidUser)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(Username), null);
                IsVisibleView = true;
            }
            else
            {
                ErrorMessage = "* Invalid username or password!";
            }
        }

        private void ExecuteRecoverPasswordCommand(string username, string email)
        {
            throw new NotImplementedException();
        }
    }
}
