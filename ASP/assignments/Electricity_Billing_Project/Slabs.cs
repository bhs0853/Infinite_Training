using System.Collections.Generic;

namespace Electricity_Billing_Project
{
    public class Slabs
    {
        public int? maxLimit { get; set; }
        public double rate { get; set; }

        public Slabs(int? maxLimit, double rate)
        {
            this.maxLimit = maxLimit;
            this.rate = rate;
        }

        public static List<Slabs> electricitySlabs()
        {
            List<Slabs> slabs = new List<Slabs>();
            slabs.Add(new Slabs(100, 0));
            slabs.Add(new Slabs(300, 1.5));
            slabs.Add(new Slabs(600, 3.5));
            slabs.Add(new Slabs(1000, 5.5));
            slabs.Add(new Slabs(null, 7.5));
            return slabs;
        }
    }
}