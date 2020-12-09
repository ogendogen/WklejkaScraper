using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    internal static class OCR
    {
        private static readonly HttpClient client = new HttpClient();
        internal static string ApiKey { get; set; }
        internal static async Task<string> ProcessImage(byte[] image)
        {
            client.DefaultRequestHeaders.Add("apikey", ApiKey);
            var values = new Dictionary<string, string>
            {
                { "base64Image", Convert.ToBase64String(image) },
                { "language", "pol" },
                { "filetype", "gif" }
            };

            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync("https://api.ocr.space/parse/image", content);

            return await response.Content.ReadAsStringAsync();
        }
    }
}
