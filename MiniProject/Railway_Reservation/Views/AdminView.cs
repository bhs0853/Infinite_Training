namespace Railway_Reservation
{
    class AdminView
    {
        int user = -1;
        public AdminView(int user)
        {
            this.user = user;
        }
        public void createAdmin()
        {
            UserAuth auth = new UserAuth();
            auth.adminSignUp(user);
        }
        public void createTrainType()
        {
            TrainType type = new TrainType(user);
            type.createTrainType();
        }
        public void deleteTrainType()
        {
            TrainType type = new TrainType(user);
            type.deleteTrainType();
        }
        public void createTrainClass()
        {
            TrainClass tClass = new TrainClass(user);
            tClass.createTrainClass();
        }
        public void deleteTrainClass()
        {
            TrainClass tClass = new TrainClass(user);
            tClass.deleteTrainClass();
        }
        public void createTrain()
        {
            Train train = new Train(user);
            train.createTrain();
        }
        public void deleteTrain()
        {
            Train train = new Train(user);
            train.deleteTrain();
        }
        public void viewReservations()
        {
            Reservation reservation = new Reservation(user);
            reservation.viewReservations();
        }
        public void viewCancellations()
        {
            Cancellation cancellation = new Cancellation(user);
            cancellation.viewCancellations();
        }
    }
}