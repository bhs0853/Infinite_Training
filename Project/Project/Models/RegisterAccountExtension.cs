using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project.Dto;

namespace Project.Models
{
    public partial class RegisterAccount
    {
        public static RegisterAccount FromDto(RegisterAccountDto dto)
        {
            return new RegisterAccount
            {
                Title = dto.Title,
                First_Name = dto.FirstName,
                Middle_Name = dto.MiddleName,
                Last_Name = dto.LastName,
                Father_Name = dto.FatherName,
                Mobile_Number = long.TryParse(dto.Mobile, out var mobile) ? mobile : 0,
                Email_Id = dto.Email,
                Aadhar = dto.Aadhar,
                Gender = dto.Gender,
                Date_Of_Birth = dto.DOB,
                Residential_Address = $"{dto.ResAddress1}, {dto.ResAddress2}, {dto.ResLandmark}, {dto.ResCity}, {dto.ResState}, {dto.ResPincode}",
                Permanent_Address = $"{dto.PerAddress1}, {dto.PerAddress2}, {dto.PerLandmark}, {dto.PerCity}, {dto.PerState}, {dto.PerPincode}",
                Occupation_Type = dto.OccupationType,
                Source_Of_Income = dto.SourceIncome,
                Gross_Annual_Income = (double?)dto.AnnualIncome,
                Opt_Debit_Card = dto.DebitCard,
                Opt_Net_Banking = dto.NetBanking
            };
        }
    }
}
