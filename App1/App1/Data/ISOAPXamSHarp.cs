using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App1.Data
{
    public interface ISOAPXamSHarp
    {
        Task<string> ValidateLogin(string Username, string PasswordUser);
    }
}
