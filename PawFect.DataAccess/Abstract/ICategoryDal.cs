using PawFect.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawFect.DataAccess.Abstract
{
    public interface ICategoryDal:IRepository<Category>
    {
        void DeleteFromCategory(int categoryId, int productId);
        Category GetByIdWithProducts(int id); //productidye göre kategorileri döndürecek

    }
}
