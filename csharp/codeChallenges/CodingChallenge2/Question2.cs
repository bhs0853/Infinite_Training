using System;

namespace CodingChallenge2
{
    /*
     * 2. Create a Class called Products with Productid, Product Name, Price. Accept 10 Products,
     * sort them based on the price, and display the sorted Products
     * 
     */
    class Products
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int price { get; set; }
        public Products(int ProductId, string ProductName, int price)
        {
            this.ProductId = ProductId;
            this.ProductName = ProductName;
            this.price = price;
        }
        public void Display()
        {
            Console.WriteLine($"product id: {ProductId} name: {ProductName} price: {price}");
        }
    }
    class Question2
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the no of products: ");
            int NoOfProducts = Convert.ToInt32(Console.ReadLine());
            Products[] productList = new Products[NoOfProducts];
            for (int i = 0; i < NoOfProducts; i++)
            {
                Console.WriteLine($"Enter product {i + 1} details: ");
                Console.Write("Enter the product id: ");
                int productId = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter the product name: ");
                string productName = Console.ReadLine();
                Console.Write("Enter the product price: ");
                int price = Convert.ToInt32(Console.ReadLine());
                productList[i] = new Products(productId, productName, price);
                Console.WriteLine();
            }
            Console.WriteLine("******************************");

            Console.WriteLine("Product list before sorting: ");
            Console.WriteLine();
            for (int i = 0; i < NoOfProducts; i++)
                productList[i].Display();
            Console.WriteLine("******************************");

            Array.Sort(productList, (p1, p2) => p1.price - p2.price);

            Console.WriteLine("Product list after sorting based on price: ");
            Console.WriteLine();
            for (int i = 0; i < NoOfProducts; i++)
                productList[i].Display();

            Console.ReadLine();
        }
    }
}
