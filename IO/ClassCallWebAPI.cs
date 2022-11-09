using Newtonsoft.Json;
using Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IO
{
    public class ClassCallWebAPI
    {
        public async Task<string> GetURLContentsAsync(string strUrl)
        {
            MemoryStream content = new MemoryStream();
            WebRequest webReq = WebRequest.Create(strUrl);

            using (WebResponse response = await webReq.GetResponseAsync())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    await responseStream.CopyToAsync(content);
                }
            }

            return Encoding.UTF8.GetString(content.ToArray());
        }
    }
}
