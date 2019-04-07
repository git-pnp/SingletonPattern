using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Threading;

namespace SingletonPattern
{    
    public sealed class ProductDetails
    {
        private static ProductDetails _productDetail = null;
        private static readonly object lockObject = new object();

        private ProductDetails()
        {
            objCounter = objCounter + 1;
        }
        public static ProductDetails ProdDetail
        {
            get
            {
                if (_productDetail == null)
                {
                    lock (lockObject)
                    {
                        if (_productDetail == null)
                        {
                            _productDetail = new ProductDetails();
                        }
                    }
                }
                return _productDetail;
            }
        } 

        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        private static int objCounter;      
        
        public void ShowProduct(string _productName,int _productPrice)
        {
            ProductName = _productName;
            ProductPrice = _productPrice;
            Console.WriteLine(string.Format("Object Index:{0}", objCounter)  + "\n" + string.Format("Product Name:{0}", ProductName) + " \n" + string.Format("Product Price:{0}", ProductPrice.ToString()));
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            ProductDetails productDetails = ProductDetails.ProdDetail;
            Thread threadProductBag = new Thread(() => productDetails.ShowProduct("Bag", 1000));
            threadProductBag.Start();
            Thread threadProductCar = new Thread(() => productDetails.ShowProduct("Car", 11000));            
            threadProductCar.Start();
            Thread threadProductWashing = new Thread(() => productDetails.ShowProduct("Washing Machine", 8000));
            threadProductWashing.Start();
            Thread threadProductIron = new Thread(() => productDetails.ShowProduct("Iron", 500));
            threadProductIron.Start();
            
            Console.Read();
        }
    }
}
