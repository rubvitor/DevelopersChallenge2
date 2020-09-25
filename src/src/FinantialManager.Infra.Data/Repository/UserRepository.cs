using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.Data.ODataLinq.Helpers;
using FinantialManager.Domain.Interfaces;
using FinantialManager.Domain.Models;
using FinantialManager.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;

namespace FinantialManager.Infra.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        protected readonly FinantialManagerContext Db;
        protected readonly DbSet<User> DbSet;

        public UserRepository(FinantialManagerContext context)
        {
            Db = context;
            DbSet = Db.Set<User>();
        }

        public IUnitOfWork UnitOfWork => Db;

        public async Task<User> GetById(string id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<User> GetByUsername(string username)
        {
            return await DbSet.AsNoTracking().Where(u => u.UserName.Trim().ToLower().Equals(username.Trim().ToLower())).FirstOrDefaultAsync();
        }

        public async Task<User> GetByEmail(string email)
        {
            return await DbSet.AsNoTracking().Where(u => u.Email.Trim().ToLower().Equals(email.Trim().ToLower())).FirstOrDefaultAsync();
        }

        public async Task<bool> ValidatePassword(string emailOrusername, string password)
        {
            return await DbSet.AsNoTracking().AnyAsync(u => (u.Email.Trim().ToLower().Equals(emailOrusername.Trim().ToLower())
                                                          || u.UserName.Trim().ToLower().Equals(emailOrusername.Trim().ToLower()))
                                                           && u.PasswordHash.Equals(password));
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public void Add(User User)
        {
            DbSet.Add(User);
        }

        public void Update(User User)
        {
            DbSet.Update(User);
        }

        public void Remove(User User)
        {
            DbSet.Remove(User);
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
