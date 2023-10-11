using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WPF_Application.Model;

namespace WPF_Application.Repositories
{
    public interface IUserRepository
    {
        bool AuthenticateUser(NetworkCredential credential);
        void Add(UserModel model);
        void Update(UserModel model);
        void Remove(int id);
        UserModel Get(int id);
        IEnumerable<UserModel> GetAll();
    }
}
