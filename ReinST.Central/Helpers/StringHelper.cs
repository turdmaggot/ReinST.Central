using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace ReinST.Central.Helpers
{
    public static class StringHelper
    {
        #region Constants

        private const string HTMLRegex = @"<(.|\n)*?>";

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the first image from a string with HTML content, for sharing in Facebook.
        /// </summary>
        /// <param name="content">
        /// Content to scan for a default image.
        /// </param>
        /// <param name="defaultImageURL">
        /// URL to of the image to display when no image tags are found.
        /// </param>
        /// <returns>
        /// URL of the first image tag found from the content.
        /// </returns>
        public static string GetFirstImageForShare(string content, string defaultImageURL)
        {
            try
            {
                List<string> imageLinks = GetAllImages(content);

                if (imageLinks.Count != 0)
                {
                    if (imageLinks[0].Contains("../../fileman"))
                    {
                        return imageLinks[0].Replace("../../fileman", ConfigurationManager.AppSettings["admin"] + "fileman");
                    }
                    else
                    {
                        return imageLinks[0];
                    }
                }
                else
                {
                    return defaultImageURL;
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                return defaultImageURL;
            }
        }

        /// <summary>
        /// Gets the first image from a string with HTML content, for a website front page.
        /// </summary>
        /// <param name="content">
        /// Content to scan for a default image.
        /// </param>
        /// <param name="defaultImageURL">
        /// URL to of the image to display when no image tags are found.
        /// </param>
        /// <returns>
        /// URL of the first image tag found from the content.
        /// </returns>
        public static string GetFirstImageForFrontPage(string content, string defaultImageURL)
        {
            try
            {
                List<string> imageLinks = GetAllImages(content);

                if (imageLinks.Count != 0)
                {
                    if (imageLinks[0].Contains("../../fileman"))
                    {
                        return imageLinks[0].Replace("../../fileman", ConfigurationManager.AppSettings["admin"] + "fileman");
                    }
                    else
                    {
                        return imageLinks[0];
                    }
                }
                else
                {
                    return defaultImageURL;
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                return defaultImageURL;
            }
        }

        /// <summary>
        /// Gets the first image from a string with HTML content, for icons on a listview etc.
        /// </summary>
        /// <param name="content">
        /// Content to scan for a default image.
        /// </param>
        /// <param name="defaultIconURL">
        /// URL to of the image to display when no image tags are found.
        /// </param>
        /// <returns>
        /// URL of the first image tag found from the content.
        /// </returns>
        public static string GetFirstImageIcon(string content, string defaultIconURL)
        {
            try
            {
                List<string> imageLinks = GetAllImages(content);

                if (imageLinks.Count != 0)
                {
                    if (imageLinks[0].Contains("../../fileman"))
                    {
                        return imageLinks[0].Replace("../../fileman", ConfigurationManager.AppSettings["admin"] + "fileman") + "?width=57&height=57";
                    }
                    else
                    {
                        return imageLinks[0];
                    }
                }
                else
                {
                    return defaultIconURL;
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                return defaultIconURL;
            }
        }

        /// <summary>
        /// Gets all images from string with HTML content.
        /// </summary>
        /// <param name="inputHTML">
        /// Content to scan for images
        /// </param>
        /// <returns>
        /// Collection of images in a list.
        /// </returns>
        public static List<string> GetAllImages(string inputHTML)
        {
            try
            {
                HtmlDocument document = new HtmlDocument();
                List<string> image_links = new List<string>();

                document.LoadHtml(inputHTML.Trim());

                HtmlNodeCollection htmlNodes = document.DocumentNode.SelectNodes("//img");

                if (htmlNodes != null)
                    foreach (HtmlNode link in htmlNodes)
                        image_links.Add(link.GetAttributeValue("src", ""));

                return image_links;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets all images from string with HTML content, with absolute URL.
        /// Note: You have to set root URL via "admin" app setting from web.config.
        /// </summary>
        /// <param name="inputHTML">
        /// Content to scan for images
        /// </param>
        /// <returns>
        /// Collection of images in a list.
        /// </returns>
        public static List<string> GetAllImagesWithFullURL(string inputHTML)
        {
            try
            {
                HtmlDocument document = new HtmlDocument();
                List<string> image_links = new List<string>();

                document.LoadHtml(inputHTML.Trim());

                HtmlNodeCollection htmlNodes = document.DocumentNode.SelectNodes("//img");

                if (htmlNodes != null)
                    foreach (HtmlNode link in document.DocumentNode.SelectNodes("//img"))
                        image_links.Add(link.GetAttributeValue("src", "").Replace("../../fileman", ConfigurationManager.AppSettings["admin"] + "fileman"));

                return image_links;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Trims the given string to the desired size.
        /// </summary>
        /// <param name="input">
        /// String to trim
        /// </param>
        /// <param name="max">
        /// Max no. of characters for the string
        /// </param>
        /// <returns>
        /// Trimmed string
        /// </returns>
        public static string TrimToMaxSize(string input, int max)
        {
            return ((input != null) && (input.Length > max)) ?
                input.Substring(0, max) : input;
        }


        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <returns>
        /// A random string
        /// </returns>
        public static string RandomStr()
        {
            string rStr = Path.GetRandomFileName();
            rStr = rStr.Replace(".", "");
            return rStr;
        }

        /// <summary>
        /// Trims the given string to the desired size, then appends ellipsis.
        /// </summary>
        /// <param name="input">
        /// String to trim
        /// </param>
        /// <param name="max">
        /// Max no. of characters for the string
        /// </param>
        /// <returns>
        /// Trimmed string, with ellipsis.
        /// </returns>
        public static string CutIfLong(string input, int max)
        {
            if (input.Length > max)
            {
                string splittedstring = TrimToMaxSize(input, max) + "...";
                return splittedstring;
            }
            else
            {
                return input;
            }
        }

        /// <summary>
        /// Generates a datetime timstamp string.
        /// </summary>
        /// <returns>
        /// Datetime timestamp.
        /// </returns>
        public static string GenerateDateTimeStamp()
        {
            return DateTime.Now.Year.ToString() + DateTime.Now.Month + DateTime.Now.Day + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
        }

        /// <summary>
        /// Generates random text based on given length
        /// </summary>
        /// <param name="length">
        /// Max no. of characters for the string
        /// </param>
        /// <returns>
        /// A random string.
        /// </returns>
        public static string GenerateRandomText(int length)
        {
            try
            {
                Random random = new Random();
                string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                StringBuilder result = new StringBuilder(length);
                for (int i = 0; i < length; i++)
                {
                    result.Append(characters[random.Next(characters.Length)]);
                }
                return result.ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Extracts the Youtube video ID from a youtube URL.
        /// </summary>
        /// <param name="youtubeURL">
        /// The URL of the video from Youtube.
        /// </param>
        /// <returns>
        /// The Youtube ID of the Youtube video.
        /// </returns>
        public static string ExtractYoutubeID(string youtubeURL)
        {
            //Setup the RegEx Match and give it 
            Match regexMatch = Regex.Match(youtubeURL, "^[^v]+v=(.{11}).*", RegexOptions.IgnoreCase);
            if (regexMatch.Success)
            {
                //return "http://www.youtube.com/v/" + regexMatch.Groups[1].Value +
                //       "&hl=en&fs=1";
                return regexMatch.Groups[1].Value;
            }
            return youtubeURL;
        }

        /// <summary>
        /// Gets the entire HTML content from a URL.
        /// </summary>
        /// <param name="url">
        /// The URL of the site to be scraped.
        /// </param>
        /// <returns>
        /// Scraped HTML content of the site from the URL.
        /// </returns>
        public static string GetWebContent(string url)
        {
            Uri uri = new Uri(url);
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.Method = WebRequestMethods.Http.Get;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string output = reader.ReadToEnd();
            response.Close();

            return output;
        }

        /// <summary>
        /// Formats currency in Philippine format (e.g XXX,XXX,XXX.XX).
        /// </summary>
        /// <param name="input">
        /// Parseable string to convert.
        /// </param>
        /// <returns>
        /// Formatted currency string.
        /// </returns>
        public static string FormatMoney(string input)
        {
            try
            {
                double dInput = Convert.ToDouble(input);
                return dInput.ToString("C", CultureInfo.CreateSpecificCulture("en-PH"));
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Formats to currency.
        /// </summary>
        /// <param name="input">
        /// Parseable string to convert.
        /// </param>
        /// <returns>
        /// Formatted currency string.
        /// </returns>
        public static string FormatMoney(double input)
        {
            try
            {
                return string.Format("{0:f}", input);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Converts currency string with "PHP" to words.
        /// </summary>
        /// <param name="strInput">
        /// Parseable amount with currency to convert.
        /// </param>
        /// <returns>
        /// Currency converted to words.
        /// </returns>
        public static string CurrencyToText(string strInput)
        {
            if (strInput == null || strInput.Length <= 0)
            {
                throw new ArgumentNullException();
            }
            try
            {
                strInput = strInput.Replace(",", string.Empty).Replace("Php", string.Empty, StringComparison.OrdinalIgnoreCase).Trim();
                int decimalCount = 0;
                int Val = strInput.Length - 1;
                for (int x = 0; x <= Val; x++)
                {
                    char Val2 = strInput[x];
                    if (Val2.ToString() == ".")
                    {
                        decimalCount++;
                        if (decimalCount > 1)
                        {
                            throw new ArgumentException("Only monetary values are accepted");
                        }
                    }
                    Val2 = strInput[x];
                    char Valtemp = strInput[x];
                    if (!(char.IsDigit(strInput[x]) | (Val2.ToString() == ".")) & !((x == 0) & (Valtemp.ToString() == "-")))
                    {
                        throw new ArgumentException("Only monetary values are accepted");
                    }
                }
                string returnValue = "";
                string[] parts;
                if (strInput.Contains("."))
                    parts = strInput.Split(new char[] { '.' });
                else
                    parts = (strInput + ".00").Split(new char[] { '.' });


                parts[1] = new string((parts[1] + "00").Substring(0, 2).ToCharArray());
                bool IsNegative = parts[0].Contains("-");
                if (parts[0].Replace("-", "").Length > 0x12)
                {
                    throw new ArgumentException("Maximum value is Php999,999,999,999,999,999.99");
                }
                if (IsNegative)
                {
                    parts[0] = parts[0].Replace("-", "");
                    returnValue = returnValue + "Minus ";
                }
                if (parts[0].Length > 15)
                {
                    returnValue = ((((returnValue + HundredsText(parts[0].PadLeft(0x12, '0').Substring(0, 3)) + "Quadrillion ")
                        + HundredsText(parts[0].PadLeft(0x12, '0').Substring(3, 3)) + "Trillion ") +
                        HundredsText(parts[0].PadLeft(0x12, '0').Substring(6, 3)) + "Billion ") +
                        HundredsText(parts[0].PadLeft(0x12, '0').Substring(9, 3)) + "Million ") +
                        HundredsText(parts[0].PadLeft(0x12, '0').Substring(12, 3)) + "Thousand ";
                }
                else if (parts[0].Length > 12)
                {
                    returnValue = (((returnValue + HundredsText(parts[0].PadLeft(15, '0').Substring(0, 3)) +
                        "Trillion ") + HundredsText(parts[0].PadLeft(15, '0').Substring(3, 3)) + "Billion ") +
                        HundredsText(parts[0].PadLeft(15, '0').Substring(6, 3)) + "Million ") +
                        HundredsText(parts[0].PadLeft(15, '0').Substring(9, 3)) + "Thousand ";
                }
                else if (parts[0].Length > 9)
                {
                    returnValue = ((returnValue + HundredsText(parts[0].PadLeft(12, '0').Substring(0, 3)) +
                        "Billion ") + HundredsText(parts[0].PadLeft(12, '0').Substring(3, 3)) + "Million ") +
                        HundredsText(parts[0].PadLeft(12, '0').Substring(6, 3)) + "Thousand ";
                }
                else if (parts[0].Length > 6)
                {
                    returnValue = (returnValue + HundredsText(parts[0].PadLeft(9, '0').Substring(0, 3)) +
                        "Million ") + HundredsText(parts[0].PadLeft(9, '0').Substring(3, 3)) + "Thousand ";
                }
                else if (parts[0].Length > 3)
                {
                    returnValue = returnValue + HundredsText(parts[0].PadLeft(6, '0').Substring(0, 3)) +
                        "Thousand ";
                }
                string hundreds = parts[0].PadLeft(3, '0');
                int tempInt = 0;
                hundreds = hundreds.Substring(hundreds.Length - 3, 3);
                if (int.TryParse(hundreds, out tempInt) == true)
                {
                    if (int.Parse(hundreds) < 100)
                    {
                        returnValue = returnValue + "and ";
                    }
                    returnValue = returnValue + HundredsText(hundreds) + "Peso";
                    if (int.Parse(hundreds) != 1)
                    {
                        returnValue = returnValue + "s";
                    }
                    if (int.Parse(parts[1]) != 0)
                    {
                        returnValue = returnValue + " and ";
                    }
                }
                if ((parts.Length == 2) && (int.Parse(parts[1]) != 0))
                {
                    returnValue = returnValue + HundredsText(parts[1].PadLeft(3, '0')) + "Centavo";
                    if (int.Parse(parts[1]) != 1)
                    {
                        returnValue = returnValue + "s";
                    }
                }
                return returnValue + " only";
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Strips HTML tags off a given string. This uses the HTMLAgilityLibrary.
        /// </summary>
        /// <param name="htmlString">
        /// Input string.
        /// </param>
        /// <returns>
        /// Input string with HTML tags strpped off.
        /// </returns>
        public static string StripHTML(string htmlString)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlString);

            if (htmlDoc == null)
                return htmlString;

            return htmlDoc.DocumentNode.InnerText;
        }

        /// <summary>
        /// Strips HTML tags off a given string. This uses regex matching.
        /// </summary>
        /// <param name="htmlString">
        /// Input string.
        /// </param>
        /// <returns>
        /// Input string with HTML tags strpped off.
        /// </returns>
        public static string StripHTMLViaRegex(string htmlString)
        {
            return Regex.Replace(htmlString, HTMLRegex, string.Empty);
        }

        /// <summary>
        /// Detects if a given string has HTML tags. This uses regex matching.
        /// </summary>
        /// <param name="value">
        /// Input string to check.
        /// </param>
        /// <returns>
        /// Returns true if there's HTML tags in the string.
        /// </returns>
        public static bool ContainsHTML(string value)
        {
            if (value != null)
                return Regex.IsMatch(value, HTMLRegex);
            else
                return false;
        }

        /// <summary>
        /// Hashes given a given string using MD5.
        /// </summary>
        /// <param name="input">
        /// Input string to hash.
        /// </param>
        /// <returns>
        /// MD5 hash of the given string.
        /// </returns>
        public static string MD5(string input)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            byte[] originalBytes = ASCIIEncoding.Default.GetBytes(input);
            byte[] encodedBytes = md5.ComputeHash(originalBytes);

            return BitConverter.ToString(encodedBytes).Replace("-", "");
        }

        /// <summary>
        /// Hashes given a given string using BCrypt.
        /// </summary>
        /// <param name="input">
        /// Input string to hash.
        /// </param>
        /// <returns>
        /// BCrypt hash of the given string.
        /// </returns>
        public static string GenerateBCryptHash(string input)
        {
            //adjust salt level here below"
            string salt = BCrypt.GenerateSalt();

            return BCrypt.HashPassword(input.Trim(), salt);
        }

        /// <summary>
        /// Verifies the BCrypt hash against an input string.
        /// </summary>
        /// <param name="plainText">
        /// String in plain text to verify.
        /// </param>
        /// /// <param name="hash">
        /// Hash to verify the plain text against.
        /// </param>
        /// <returns>
        /// True, if the hash matches the input string.
        /// </returns>
        public static bool VerifyBCryptHash(string plainText, string hash)
        {
            return BCrypt.CheckPassword(plainText, hash);
        }

        /// <summary>
        /// Converts numbers to words.
        /// </summary>
        /// <param name="number">
        /// Number to be converted.
        /// </param>
        /// <returns>
        /// Integer converted to words.
        /// </returns>
        public static string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }

        #endregion

        #region Private Methods

        private static string HundredsText(string value)
        {
            try
            {
                char Val_1;
                char Val_2;

                string returnValue = "";
                bool IsSingleDigit = true;
                char Val = value[0];
                if (int.Parse(Val.ToString()) != 0)
                {
                    Val_1 = value[0];
                    returnValue = returnValue + Ones[int.Parse(Val_1.ToString()) - 1] + " Hundred ";
                    IsSingleDigit = false;
                }
                Val_1 = value[1];
                if (int.Parse(Val_1.ToString()) > 1)
                {
                    Val = value[1];
                    returnValue = returnValue + Tens[int.Parse(Val.ToString()) - 1] + " ";
                    Val_1 = value[2];
                    if (int.Parse(Val_1.ToString()) != 0)
                    {
                        Val = value[2];
                        returnValue = returnValue + Ones[int.Parse(Val.ToString()) - 1] + " ";
                    }
                    return returnValue;
                }
                Val_1 = value[1];
                if (int.Parse(Val_1.ToString()) == 1)
                {
                    Val = value[1];
                    Val_2 = value[2];
                    return (returnValue + Ones[int.Parse(Val.ToString() + Val_2.ToString()) - 1] + " ");
                }
                Val_2 = value[2];
                if (int.Parse(Val_2.ToString()) == 0)
                {
                    return returnValue;
                }
                if (!IsSingleDigit)
                {
                    returnValue = returnValue + "and ";
                }
                Val_2 = value[2];
                return (returnValue + Ones[int.Parse(Val_2.ToString()) - 1] + " ");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private static string[] Tens = new string[]
        {
            "Ten", "Twenty", "Thirty", "Forty", "Fifty",
            "Sixty", "Seventy", "Eighty", "Ninety"
        };

        private static string[] Ones = new string[]
        {
            "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten",
            "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"
        };

        #endregion
    }
}
