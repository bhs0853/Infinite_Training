using System;

namespace assignment3
{
    /*
     * 3. Create a class called Saledetails which has data members like Salesno,  Productno,  Price, dateofsale, Qty, TotalAmount
            - Create a method called Sales() that takes qty, Price details of the object and updates the TotalAmount as  Qty *Price
            - Pass the other information like SalesNo, Productno, Price,Qty and Dateof sale through constructor
            - call the show data method to display the values without an object.
     */
    class SaleDetails
    {
        static int salesNo { get; set; }
        static int productNo;
        static int price { get; set; }
        static DateTime dateOfSale { get; set; }
        static int quantity { get; set; }
        static int totalAmount { get; set; }

        public SaleDetails(int salesNo1, int productNo1, int price1, DateTime dateOfSale1, int quantity1)
        {
            salesNo = salesNo1;
            productNo = productNo1;
            price = price1;
            dateOfSale = dateOfSale1;
            quantity = quantity1;
            totalAmount = quantity1 * price1;
        }

        public void Sales(int quantity1, int price1)
        {
            quantity = quantity1;
            price = price1;
            totalAmount = quantity1 * price1;
        }

        public static void ShowData()
        {
            Console.WriteLine("********** Sales Details **********");
            Console.WriteLine($"Sales no: {salesNo}");
            Console.WriteLine($"Product Number: {productNo}");
            Console.WriteLine($"Price: {price}");
            Console.WriteLine($"Date of Sale: {dateOfSale}");
            Console.WriteLine($"Quantity: {quantity}");
            Console.WriteLine($"Total Amount: {totalAmount}");
            Console.WriteLine("************************");
        }
    }
    class SaleDetailsQuestion
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sales Details:");
            Console.WriteLine("Enter the Sales no:");
            int salesNo = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the product no:");
            int productNo = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the price:");
            int price = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the Date of Sale:");
            DateTime dateOfSale = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Enter the quantity:");
            int quantity = Convert.ToInt32(Console.ReadLine());

            SaleDetails saleDetails = new SaleDetails(salesNo, productNo, price, dateOfSale, quantity);
            SaleDetails.ShowData();
            Console.WriteLine("Enter the new quantity:");
            quantity = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the new price:");
            price = Convert.ToInt32(Console.ReadLine());
            saleDetails.Sales(quantity, price);

            //calling static method directly using className
            SaleDetails.ShowData();
            Console.Read();
        }
    }
}
