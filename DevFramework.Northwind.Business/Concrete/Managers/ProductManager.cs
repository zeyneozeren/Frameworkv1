using DevFramework.Core.CrossCuttingConcerns.Validation.FluentValidation;
using DevFramework.Core.Aspects.PostSharp;
using DevFramework.Northwind.Business.Abstract;
using DevFramework.Northwind.Business.ValidationRules.FluentValidation;
using DevFramework.Northwind.DataAccess.Abstract;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFramework.Core.DataAccess;
using DevFramework.Core.CrossCuttingConcerns.Caching.Microsoft;
using DevFramework.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;

namespace DevFramework.Northwind.Business.Concrete.Managers
{
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;
       

        public ProductManager(IProductDal productDal)
        {
           
            _productDal = productDal;
        }

        [FluentValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public Product Add(Product product)
        {
            //ValidatorTool.FluentValidate(new ProductValidator(), product); yerıne [FluentValidate(typeof(ProductValidator))] yazdık
            return _productDal.Add(product);
        }

        [FluentValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public Product Update(Product product)
        {
            //ValidatorTool.FluentValidate(new ProductValidator(), product);
            return _productDal.Update(product);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        [LogAspect(typeof(FileLogger))]
        public List<Product> GetAll()
        {
            
            return _productDal.GetList();
        }

        public Product GetById(int id)
        {
            return _productDal.Get(p => p.ProductID == id);
        }
    }
}
