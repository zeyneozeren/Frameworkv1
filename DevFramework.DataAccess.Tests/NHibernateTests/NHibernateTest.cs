using DevFramework.Northwind.DataAccess.Concrete.NHibernate;
using DevFramework.Northwind.DataAccess.Concrete.NHibernate.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.DataAccess.Tests.NHıbernateTests
{
    [TestClass]
    public class NHibernateTest
    {
        [TestMethod]
        public void Get_All_Returns_All_Products()
        {
            NHProductDal productDal = new NHProductDal(new SqlServerHelper());
            var result = productDal.GetList();
            Assert.AreEqual(77, result.Count);
        }

        [TestMethod]
        public void Get_All_withparameter_Returns_filtered_Products()
        {
            NHProductDal productDal = new NHProductDal(new SqlServerHelper());
            var result = productDal.GetList(p => p.ProductName.Contains("ab"));
            Assert.AreEqual(4, result.Count);
        }
    }
}
