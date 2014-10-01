using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductsManagementApp
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Units { get; set; }
        public decimal Cost { get; set; }
        public override string ToString()
        {
            return string.Format("{0}\t{1}\t{2}\t{3}", this.Id, this.Name, this.Units, this.Cost);
        }
    }

   

    public class ProductComparerById : IProductComparer
    {
        public int Compare(Product left, Product right)
        {
            if (left.Id < right.Id) return -1;
            if (left.Id > right.Id) return 1;
            return 0;
        }
    }

    public class ProductComparerByUnits: IProductComparer
    {
        public int Compare(Product left, Product right)
        {
            if (left.Units < right.Units) return -1;
            if (left.Units > right.Units) return 1;
            return 0;
        }
    }

    public class ProductsCollection {
        ArrayList list = new ArrayList();
        public void Add(Product product){
            list.Add(product);
        }
        
        public int Count{
        get{
            return list.Count;
        }
        }

        public Product this[int index]{
            get {
                return list[index] as Product;
            }
            set {
                list[index]  = value;
            }
        }
       
        public void Sort(IProductComparer comparer){
            for(var i=0;i<list.Count -1;i++)
                for(var j=i+1;j<list.Count;j++){
                    var leftProduct = (Product)list[i];
                    var rightProduct= (Product)list[j];
                    if (comparer.Compare(leftProduct,rightProduct) > 0 ){
                        var temp = leftProduct;
                        list[i] = list[j];
                        list[j] = temp;
                    }
                }
        }

        public ProductsCollection Filter(IProductCriteria criteriaObj)
        {
            var result = new ProductsCollection();
            for (var i = 0; i < list.Count; i++)
            {
                var product = (Product)list[i];
                if (criteriaObj.IsSatisfiedBy(product))
                    result.Add(product);
            }
            return result;
        }

         //public ProductsCollection Filter(Func<Product,bool> criteria)
        public ProductsCollection Filter(Predicate<Product> criteria)
        {
            var result = new ProductsCollection();
            for (var i = 0; i < list.Count; i++)
            {
                var product = (Product)list[i];
                if (criteria(product))
                    result.Add(product);
            }
            return result;
        }
    }

    public interface IProductCriteria
    {
        bool IsSatisfiedBy(Product product);
    }

     //public delegate bool ProductCriteriaDelegate(Product product);
    //public delegage bool ItemCriteriaDelegate<T>(T item);

     public interface IProductComparer
    {
        int Compare(Product left, Product right);
    }

    public class CostlyProductCriteria : IProductCriteria
    {
        public bool IsSatisfiedBy(Product product)
        {
            return product.Cost > 50;
        }
    }

    public class InverseProductCriteria : IProductCriteria{
        private IProductCriteria _criteria;
        public InverseProductCriteria (IProductCriteria criteria)
	    {
            _criteria = criteria;
	    }
        public bool IsSatisfiedBy(Product product)
        {
 	        return !_criteria.IsSatisfiedBy(product);
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var products = new ProductsCollection();
            products.Add(new Product{ Id = 4, Name = "Pen", Units = 70, Cost = 60});
            products.Add(new Product{ Id = 8, Name = "Hen", Units = 40, Cost = 30});
            products.Add(new Product{ Id = 2, Name = "Ten", Units = 20, Cost = 90});
            products.Add(new Product{ Id = 6, Name = "Den", Units = 80, Cost = 40});
            products.Add(new Product{ Id = 9, Name = "Len", Units = 60, Cost = 20});
            products.Add(new Product{ Id = 5, Name = "Zen", Units = 50, Cost = 50});

            Console.WriteLine("Initial List");
            for(var i=0;i<products.Count;i++)
                Console.WriteLine(products[i]);

            Console.WriteLine();
            Console.WriteLine("After sorting by Id..");
            products.Sort(new ProductComparerById());
            for(var i=0;i<products.Count;i++)
                Console.WriteLine(products[i]);

            Console.WriteLine();
            Console.WriteLine("After sorting by Units..");
            products.Sort(new ProductComparerByUnits());
            for (var i = 0; i < products.Count; i++)
                Console.WriteLine(products[i]);


            var costlyProducts = products.Filter(new CostlyProductCriteria());

            //var affordableProdcts = products.Filter(new ProductCriteriaDelegate(AffordableProductCriteria));

            /*
            var affordableProdcts = products.Filter(delegate(Product product)
            {
                return product.Cost <= 50;
            });
             */
            /*
            var affordableProdcts = products.Filter((product) =>
            {
                return product.Cost <= 50;
            });
             */
            var affordableProdcts = products.Filter(product => product.Cost <= 50);

            Console.WriteLine();
            Console.WriteLine("Affordable products");
            for (var i = 0; i < affordableProdcts.Count; i++)
                Console.WriteLine(affordableProdcts[i]);

            Console.ReadLine();
        }
        /*
        public static bool AffordableProductCriteria(Product product){
            return product.Cost <= 50;
        }
         * */
    }
}
