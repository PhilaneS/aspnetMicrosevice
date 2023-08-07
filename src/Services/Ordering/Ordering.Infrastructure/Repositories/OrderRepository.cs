using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
      

        public OrderRepository(OrderContext dbContext): base(dbContext)
        {}

        public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
        {
            return await _dbContext.Orders.Where(o => o.UserName == userName).ToListAsync();
        }
        public Task<IReadOnlyList<Order>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetAsync(Expression<Func<Order, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetAsync(Expression<Func<Order, bool>> predicate = null, Func<IQueryable<Order>, IOrderedQueryable<Order>> orderBy = null, string includeString = null, bool disableTracking = true)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetAsync(Expression<Func<Order, bool>> predicate = null, Func<IQueryable<Order>, IOrderedQueryable<Order>> orderBy = null, List<Expression<Func<Order, object>>> includes = null, bool disableTracking = true)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }



        public Task UpdateAsync(Order entity)
        {
            throw new NotImplementedException();
        }
        public async Task<Order> AddAsync(Order entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Order entity)
        {
            throw new NotImplementedException();
        }

      
    }
}
