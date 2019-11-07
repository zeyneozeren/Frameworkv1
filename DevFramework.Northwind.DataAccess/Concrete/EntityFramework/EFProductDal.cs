using DevFramework.Core.DataAccess.EntityFramework;
using DevFramework.Northwind.DataAccess.Abstract;
using DevFramework.Northwind.Entities.Concrete;
using DevFramework.Northwind.DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DevFramework.Northwind.Entities.ComplexType;

namespace DevFramework.Northwind.DataAccess.Concrete.EntityFramework
{
    public class EFProductDal : EFEntityRepositoryBase<Product, NorthwindContext>, IProductDal
    {
        public List<ProductDetail> GetProductDetails()
        {
            using (NorthwindContext context=new NorthwindContext())
            {
                var result = from p in context.Products
                             //join c in context.Categories on c.CategoryID equals p.CategoryID
                             select new ProductDetail()
                             {
                                 ProductID = p.ProductID,
                                 ProductName=p.ProductName,
                                 CategoryName=""
                             };
                return result.ToList();
            }
        }
    }
}
