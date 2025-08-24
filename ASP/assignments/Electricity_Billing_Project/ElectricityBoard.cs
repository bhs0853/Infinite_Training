using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Electricity_Billing_Project
{
    public class ElectricityBoard
    {
        static SqlConnection con = null;
        public void AddBill(ElectricityBill ebill)
        {
            try
            {
                con = DBHandler.GetConnection();
                SqlCommand cmd = new SqlCommand("insert into ElectricityBill (consumer_number, consumer_name, units_consumed, bill_amount) values (@consumer_number, @consumer_name, @units_consumed, @bill_amount)", con);
                cmd.Parameters.AddWithValue("@consumer_number", ebill.ConsumerNumber);
                cmd.Parameters.AddWithValue("@consumer_name", ebill.ConsumerName);
                cmd.Parameters.AddWithValue("@units_consumed", ebill.UnitsConsumed);
                cmd.Parameters.AddWithValue("@bill_amount", ebill.BillAmount);

                cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }
        }
        public void CalculateBill(ElectricityBill ebill)
        {
            int units = ebill.UnitsConsumed;
            double bill = 0;
            int previousLimit = 0;
            List<Slabs> slabs = Slabs.electricitySlabs();
            foreach (var slab in slabs)
            {
                if (slab.maxLimit == null || units < slab.maxLimit)
                {
                    bill += (units - previousLimit) * slab.rate;
                    break;
                }
                else
                {
                    bill += (double)(slab.maxLimit - previousLimit) * slab.rate;
                    previousLimit = (int)slab.maxLimit;
                }
            }
            ebill.BillAmount = bill;
        }
        public List<ElectricityBill> Generate_N_BillDetails(int num)
        {
            List<ElectricityBill> bills = new List<ElectricityBill>();
            try
            {
                con = DBHandler.GetConnection();
                SqlCommand cmd = new SqlCommand($"select top {num} consumer_number, consumer_name, units_consumed, bill_amount from ElectricityBill order by created_at desc", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    bills.Add(new ElectricityBill
                    {
                        ConsumerNumber = dr["consumer_number"].ToString(),
                        ConsumerName = dr["consumer_name"].ToString(),
                        UnitsConsumed = Convert.ToInt32(dr["units_consumed"]),
                        BillAmount = Convert.ToDouble(dr["bill_amount"])
                    });
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                con.Close();
            }
            return bills;
        }
    }
}