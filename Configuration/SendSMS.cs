using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace ttpMiddleware.Configuration
{    
    public class SendSMS
    {
        public string send()
        {
            String message = HttpUtility.UrlEncode("This is your message");
            using (var wb = new WebClient())
            {
                byte[] response = wb.UploadValues("https://api.txtlocal.com/send/", new NameValueCollection()
                {
                {"apikey" , "NzU1MTZmNDc0NTZhNTU1MTM2NDg2YzU3NmQ2NzMwMzE="},
                {"numbers" , "919920024852"},
                {"message" , message},
                {"sender" , "Mung sender"}
                });
                string result = System.Text.Encoding.UTF8.GetString(response);
                return result;
            }
        }
    }

}
