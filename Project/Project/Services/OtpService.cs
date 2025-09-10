using System;
using System.Net;
using System.Net.Mail;
using Project.Repository;
namespace Project.Services
{
    public class OtpService
    {
        private readonly IOtpRepository _otpRepo;
        public OtpService(IOtpRepository _otpRepo)
        {
            this._otpRepo = _otpRepo;
        }

        public int GenerateOtp(string email)
        {
            if (string.IsNullOrEmpty(email)) 
            {
                return -1; 
            }
            return _otpRepo.GenerateOtp(email);
        }

        public bool VerifyOtp(string email, int otp)
        {
            return _otpRepo.VerifyOtp(email, otp);
        }
    }
}