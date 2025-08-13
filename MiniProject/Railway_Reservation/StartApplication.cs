using System;

namespace Railway_Reservation
{
    class StartApplication
    {
        public static void startApplication()
        {
            Console.WriteLine("********** Welcome to Railway Reservation System **********");
            UserAuth userAuth = new UserAuth();
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Enter 1 to Sign In \nEnter 2 to Sign Up \nEnter 3 to exit");
                try
                {
                    int choice = -1;
                    int.TryParse(Console.ReadLine(), out choice);
                    switch (choice)
                    {
                        case 1:
                            var credentials = userAuth.signIn();
                            if (credentials.user <= 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Check Credentials!!! The email or password is wrong");
                                Console.ResetColor();
                                continue;
                            }
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("SignIn successful");
                            Console.ResetColor();
                            Console.WriteLine();

                            View view = new View();
                            switch (credentials.role)
                            {
                                case "admin":
                                    view.adminView(credentials.user);
                                    break;
                                case "user":
                                    view.userView(credentials.user);
                                    break;
                                default:
                                    Console.WriteLine("Error Occurred!! Please sign in again");
                                    continue;
                            }
                            break;
                        case 2:
                            userAuth.signUp();
                            break;
                        case 3:
                            Console.WriteLine("********** Exit **********");
                            return;
                        default:
                            Console.WriteLine("Invalid Choice");
                            continue;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
