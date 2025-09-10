using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    public interface IOtpRepository
    {
        int GenerateOtp(string email);

        bool VerifyOtp(string email, int otp);

        void SendEmail(string to, string subject, string body);
    }
}
