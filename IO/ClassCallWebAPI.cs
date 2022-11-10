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
        /// <summary>
        /// I: Henter data fra given api URL.
        /// Denne synkrone metode kan kommunikere alle former for web API'er.
        /// Den skal blot modtage en komplet URL.
        /// </summary>
        /// <param name="inUrl">string</param>
        /// <returns>string</returns>
        public async Task<string> GetURLContentsAsync(string strUrl)
        {
            // A: Streams bliver brugt til at samle flere packets i et enkelt object
            MemoryStream content = new MemoryStream();
            WebRequest webReq = WebRequest.Create(strUrl);

            using (WebResponse response = await webReq.GetResponseAsync())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    await responseStream.CopyToAsync(content);
                }
            }
            // I: Omdanner resultatet fra api'en til tekst
            return Encoding.UTF8.GetString(content.ToArray());
        }
    }
}
