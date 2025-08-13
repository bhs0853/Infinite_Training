using System;

namespace Railway_Reservation
{
    class View
    {
        public void adminView(int user_id)
        {
            Console.WriteLine("Welcome Admin !!!");
            Console.WriteLine();
            Console.WriteLine("********** Admin View **********");
            AdminView admin = new AdminView(user_id);
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Enter 1 to create train type \nEnter 2 to delete train type \nEnter 3 to create train class \nEnter 4 to delete train class \nEnter 5 to create train \nEnter 6 to delete train \nEnter 7 to view Reservations \nEnter 8 to view Cancellations \nEnter 9 to create new admin \nEnter 10 to Sign Out");
                int choice = -1;
                int.TryParse(Console.ReadLine(), out choice);
                switch (choice)
                {
                    case 1:
                        admin.createTrainType();
                        break;
                    case 2:
                        admin.deleteTrainType();
                        break;
                    case 3:
                        admin.createTrainClass();
                        break;
                    case 4:
                        admin.deleteTrainClass();
                        break;
                    case 5:
                        admin.createTrain();
                        break;
                    case 6:
                        admin.deleteTrain();
                        break;
                    case 7:
                        admin.viewReservations();
                        break;
                    case 8:
                        admin.viewCancellations();
                        break;
                    case 9:
                        admin.createAdmin();
                        break;
                    case 10:
                        Console.WriteLine("Admin Logout Successful");
                        return;
                    default:
                        Console.WriteLine("Invalid choice");
                        continue;
                }
            }
        }
        public void userView(int user_id)
        {
            Console.WriteLine("Welcome User !!!");
            Console.WriteLine();
            Console.WriteLine("********** User View **********");
            UserView user = new UserView(user_id);
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Enter 1 to view profile \nEnter 2 to view available trains \nEnter 3 to book a ticket \nEnter 4 to Cancel a ticket \nEnter 5 to view your reservations \nEnter 6 to view your cancellations \nEnter 7 to Log Out");
                int choice = -1;
                int.TryParse(Console.ReadLine(), out choice);
                switch (choice)
                {
                    case 1:
                        user.viewProfile();
                        break;
                    case 2:
                        user.viewAvailableTrains();
                        break;
                    case 3:
                        user.BookTicket();
                        break;
                    case 4:
                        user.CancelTicket();
                        break;
                    case 5:
                        user.viewReservations();
                        break;
                    case 6:
                        user.viewCancellations();
                        break;
                    case 7:
                        Console.WriteLine("User Logout Successful");
                        return;
                    default:
                        Console.WriteLine("Invalid choice");
                        continue;
                }
            }
        }
    }
}