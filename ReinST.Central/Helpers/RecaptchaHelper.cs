using Newtonsoft.Json;
using System.Collections.Generic;

namespace ReinST.Central.Helpers
{
    public class ReCaptchaHelper
    {
        /// <summary>
        /// Validates ReCapcha response.
        /// </summary>
        /// <param name="EncodedResponse">
        /// Encoded response from ReCaptcha.
        /// </param>
        /// <param name="PrivateKey">
        /// Private API key for ReCaptcha.
        /// </param>
        /// <returns>
        /// Returns true if ReCaptcha validation succeeds.
        /// </returns>
        public static string Validate(string EncodedResponse, string PrivateKey)
        {
            var client = new System.Net.WebClient();
            var GoogleReply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", PrivateKey, EncodedResponse));
            var captchaResponse = JsonConvert.DeserializeObject<ReCaptchaHelper>(GoogleReply);
            return captchaResponse.Success;
        }

        [JsonProperty("success")]
        public string Success
        {
            get { return m_Success; }
            set { m_Success = value; }
        }

        private string m_Success;

        [JsonProperty("error-codes")]
        public List<string> ErrorCodes
        {
            get { return m_ErrorCodes; }
            set { m_ErrorCodes = value; }
        }

        private List<string> m_ErrorCodes;
    }
}
