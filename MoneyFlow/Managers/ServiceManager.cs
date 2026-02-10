using Microsoft.EntityFrameworkCore;
using MoneyFlow.Context;
using MoneyFlow.Entities;
using MoneyFlow.Models;

namespace MoneyFlow.Managers
{
    public class ServiceManager(AppDbContext _dbContext)
    {
        public List<ServiceVM> GetAll(int userId)
        {
            var list = _dbContext.Services
                .Where(item => item.UserId == userId)
                .Select(item => new ServiceVM
                {
                    ServiceId = item.ServiceId,
                    UserId = item.UserId,
                    Name = item.Name,
                    Type = item.Type
                })
                .ToList();
            return list;
        }

        public int New(ServiceVM viewModel)
        {
            var entity = new Service
            {
                Name = viewModel.Name,
                Type = viewModel.Type,
                UserId = viewModel.UserId,
                User = _dbContext.Users.First(user => user.UserId == viewModel.UserId)
            };

            _dbContext.Services.Add(entity);
            var rowsAfected = _dbContext.SaveChanges();
            return rowsAfected;
        }

        public ServiceVM GetById(int id)
        {
            var entity = _dbContext.Services.Find(id);

            var model = new ServiceVM
            {
                ServiceId = entity.ServiceId,
                Name = entity.Name,
                Type = entity.Type,
            };

            return model;
        }

        public int Edit(ServiceVM viewModel)
        {
            var entity = _dbContext.Services.Find(viewModel.ServiceId);
            entity.Name = viewModel.Name;
            entity.Type = viewModel.Type;

            _dbContext.Services.Update(entity);
            var rowsAfected = _dbContext.SaveChanges();
            return rowsAfected;
        }

        public int Delete(int id)
        {
            var entity = _dbContext.Services.Find(id);
            _dbContext.Services.Remove(entity);
            var rowsAfected = _dbContext.SaveChanges();
            return rowsAfected;
        }

        public List<ServiceVM> GetByType(int userId, string type)
        {
            var list = _dbContext.Services
                .Where(item => item.UserId == userId && item.Type == type)
                .Select(item => new ServiceVM
                {
                    ServiceId = item.ServiceId,
                    Name = item.Name,
                    Type = item.Type,
                }).ToList();
            return list;
        }
    }
}
