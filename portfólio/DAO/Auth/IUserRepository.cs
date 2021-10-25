using portfólio.Models;
using portfólio.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace portfólio.DAO.RepositoryUser
{
  public interface IUserRepository
    {
        RegisterViewModel Register(RegisterViewModel entity);
        User Login(string email, string password);
    }
}
