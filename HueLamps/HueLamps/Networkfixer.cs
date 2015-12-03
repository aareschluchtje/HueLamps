using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Web.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Networking.Sockets;

namespace HueLamps
{
    class Networkfixer
    {
        private async Task<string> RetrieveLights()
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfter(5000);

            try
            {
                HttpClient client = new HttpClient();

                string ip = "127.0.0.1";
                int port = 8000;
                string username = "bleh";

                Uri uriAllLightInfo
                       = new Uri($"http://{ip}:{port}/api/{username}/lights/");
                var response = await client.GetAsync(uriAllLightInfo).AsTask(cts.Token);
                if (!response.IsSuccessStatusCode)
                {
                    return string.Empty;
                }

                string jsonResponse
        = await response.Content.ReadAsStringAsync();

                System.Diagnostics.Debug.WriteLine(jsonResponse);

                return jsonResponse;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
