using System;

namespace Electricity_Billing_Project
{
    public class ElectricityBill
    {
        String consumerNumber;
        String consumerName;
        int unitsConsumed;
        double billAmount;

        public String ConsumerNumber
        {
            get { return consumerNumber; }
            set { consumerNumber = value; }
        }
        public String ConsumerName
        {
            get { return consumerName; }
            set { consumerName = value; }
        }
        public int UnitsConsumed
        {
            get { return unitsConsumed; }
            set { unitsConsumed = value; }
        }
        public double BillAmount
        {
            get { return billAmount; }
            set { billAmount = value; }
        }
    }
}