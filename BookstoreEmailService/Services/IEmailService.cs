using BookstoreEmailService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreEmailService.Services
{
    public interface IEmailService
    {
        void SendEmail(MailMessage message);
    }
}
