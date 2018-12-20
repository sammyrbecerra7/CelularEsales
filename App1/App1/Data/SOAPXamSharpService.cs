using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.Threading.Tasks;

namespace App1.Data
{
    public class SOAPXamSharpService : ISOAPXamSHarp
    {
        Soap.TemporalSoap oService;
        public SOAPXamSharpService()
        {
            oService = new Soap.TemporalSoapClient(
                new BasicHttpBinding(),
                new EndpointAddress("")
                );
        }

        public async Task<string> ValidateLogin(string Username, string PasswordUser)
        {
            string Result = null;
            try
            {
                //Result = await Task.Factory.FromAsync.FromAsync(oService.InsertaDatosTmpAsync, oService.InsertaDatosTmpAsync, Username, PasswordUser, TaskCreationOptions.None);
            }
            catch (Exception ex)
            {
                //throw ex;
                Result = ex.ToString();
            }
            return Result;
        }
    }
}
