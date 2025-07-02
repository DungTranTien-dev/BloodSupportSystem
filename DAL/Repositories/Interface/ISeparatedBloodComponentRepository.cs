using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interface
{
    public interface ISeparatedBloodComponentRepository : IGenericRepository<SeparatedBloodComponent>
    {
        Task<List<SeparatedBloodComponent>> GetAllWithBloodAsync();
        Task<IEnumerable<SeparatedBloodComponent>> GetAllAsync(Expression<Func<SeparatedBloodComponent, bool>> predicate);

    }
}
