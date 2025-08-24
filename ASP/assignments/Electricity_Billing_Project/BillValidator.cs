using System;

namespace Electricity_Billing_Project
{
    public class BillValidator
    {
        public String ValidateUnitsConsumed(int UnitsConsumed)
        {
            return UnitsConsumed >= 0 ? "Valid units" : "Given Units is invalid";
        }
    }
}