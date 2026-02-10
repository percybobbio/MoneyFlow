using MoneyFlow.Context;
using MoneyFlow.Entities;
using MoneyFlow.Models;
using MoneyFlow.Utilities;

namespace MoneyFlow.Managers
{
    public class UserManager(AppDbContext _dbContext)
    {
        public UserVM Login(LoginVM viewModel)
        {
            var found = _dbContext.Users
                .Where(item => 
                    item.Email == viewModel.Email && 
                    item.Password == Sha256Hasher.ComputeHash(viewModel.Password))
                .FirstOrDefault();

            UserVM user;

            if (found != null)
            {
                user = new UserVM
                {
                    UserId = found.UserId,
                    FullName = found.FullName,
                    Email = found.Email,
                    Password = found.Password,
                    RepeatPassword = found.Password // Set to Password to satisfy required property
                };
            }
            else
            {
                user = new UserVM
                {
                    UserId = 0,
                    FullName = string.Empty,
                    Email = string.Empty,
                    Password = string.Empty,
                    RepeatPassword = string.Empty
                };
            }

            return user;
        }

        public int Register(UserVM viewModel)
        {
            if(viewModel.Password != viewModel.RepeatPassword)
            {
                throw new InvalidOperationException("Las contraseñas no coinciden");
            }

            var foundEmail = _dbContext.Users.Any(i => i.Email == viewModel.Email);
            if (foundEmail)
            {
                throw new InvalidOperationException("El correo electrónico ya existe");
            }
            else
            {
                var entity = new User
                {
                    FullName = viewModel.FullName,
                    Email = viewModel.Email,
                    Password = Sha256Hasher.ComputeHash(viewModel.Password)
                };
                
                _dbContext.Users.Add(entity);
                var rowsAffected = _dbContext.SaveChanges();
                return rowsAffected;
            }

        }
    }
}
