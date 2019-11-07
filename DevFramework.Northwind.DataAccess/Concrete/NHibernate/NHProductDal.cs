using DevFramework.Northwind.DataAccess.Abstract;
using DevFramework.Northwind.Entities.Concrete;
using DevFramework.Northwind.DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFramework.Core.DataAccess.NHibernate;
using DevFramework.Northwind.Entities.ComplexType;
using NHibernate.Linq;

namespace DevFramework.Northwind.DataAccess.Concrete.NHibernate
{
    public class NHProductDal : NHEntityRepositoryBase<Product>, IProductDal
    {
        private NHibernateHelper _nHibernateHelper;
        public NHProductDal(NHibernateHelper nHibernateHelper) : base(nHibernateHelper)
        {
            _nHibernateHelper = nHibernateHelper;
        }

        public List<ProductDetail> GetProductDetails()
        {
            using (var session=_nHibernateHelper.OpenSession())
            {
                var result = from p in session.Query<Product>()
                             //join c in session.Query<Categories>() on c.CategoryID equals p.CategoryID
                             select new ProductDetail()
                             {
                                 ProductID = p.ProductID,
                                 ProductName = p.ProductName,
                                 CategoryName = ""
                             };
                return result.ToList();
            }
        }
    }
}
