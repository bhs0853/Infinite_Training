using System;

namespace assignment5
{
    /*
     * 3. Create a class called Books with BookName and AuthorName as members. Instantiate the class through constructor and also write a method Display() to display the details. 

     * Create an Indexer of Books Object to store 5 books in a class called BookShelf. Using the indexer method assign values to the books and display the same.
     */
    class Books
    {
        string BookName;
        string AuthorName;
        public Books(string BookName, string AuthorName)
        {
            this.BookName = BookName;
            this.AuthorName = AuthorName;
        }
        public void Display()
        {
            Console.WriteLine($"Book: {BookName} Author: {AuthorName}");
        }
    }
    class BookShelf
    {
        Books[] books;
        public BookShelf(int n)
        {
            this.books = new Books[n];
        }
        public Books this[int i]
        {
            get { return books[i]; }
            set { books[i] = value; }
        }
    }
    class Question3
    {
        static void Main(string[] args)
        {
            Console.Write("Enter number of books you want to store: ");
            int n = Convert.ToInt32(Console.ReadLine());
            BookShelf bookShelf = new BookShelf(n);
            int i = 0;
            for (; i < n; i++)
            {
                Console.Write("Enter the book name: ");
                string bookName = Console.ReadLine();
                Console.Write("Enter the author name: ");
                string authorName = Console.ReadLine();
                Books newBook = new Books(bookName, authorName);
                bookShelf[i] = newBook;
            }
            i = 0;
            while (true)
            {
                try
                {
                    Console.WriteLine($"Enter the index of the book between 0 and {n - 1} or else any character key to exit");
                    if (!int.TryParse(Console.ReadLine(), out i))
                        break;
                    bookShelf[i].Display();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            Console.WriteLine("****** EXIT ******");
            Console.ReadLine();
        }
    }
}
