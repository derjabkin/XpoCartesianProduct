using DevExpress.Xpo;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;

namespace XpoCartesianProduct
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (var dataLayer = new SimpleDataLayer(XpoDefault.GetConnectionProvider(connectionString, DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema)))
            {
                CreateTestObjects(dataLayer);

                using (var unitOfWork = new UnitOfWork(dataLayer))
                {
                    var query = from e1 in unitOfWork.Query<Entity1>()
                                from e2 in unitOfWork.Query<Entity2>()
                                select new { e1, e2 };

                    var toArrayLength = query.ToArray().Length;
                    var queryCount = query.Count();
                    Console.WriteLine("Array length: {0}", toArrayLength); //10000 (Correct)
                    Console.WriteLine("Returned by query.Count(): {0}", queryCount); //100 (Incorrect)
                    Debug.Assert(queryCount == 10000);
                }
            }
        }

        private static void CreateTestObjects(SimpleDataLayer dataLayer)
        {
            using (var unitOfWork = new UnitOfWork(dataLayer))
            {
                if (unitOfWork.Query<Entity1>().Any()) return;
                for (int i = 0; i < 100; i++)
                {
                    var entity1 = new Entity1(unitOfWork) { Name = $"Entity1 {i}" };
                    var entity2 = new Entity2(unitOfWork) { Name = $"Entity2 {i}" };
                }

                unitOfWork.CommitChanges();
            }
        }
    }

}
