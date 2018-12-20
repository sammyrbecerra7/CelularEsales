using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App1.Data
{
    public class XamSharpServiceManager
    {
        ISOAPXamSHarp soapService;

        public XamSharpServiceManager(ISOAPXamSHarp service)
        {
            soapService = service;
        }

        public Task<string> ValidateLogin(string username, string password)
        {
            try
            {
                return soapService.ValidateLogin(username, password);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
