using PawFect.DataAccess.Abstract;
using PawFect.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PawFect.DataAccess.Concrete.EfCore
{
    public class EfCoreCommentDal : EfCoreGenericRepository<Comment,DataContext>, ICommentDal
    {
       
    }
}
