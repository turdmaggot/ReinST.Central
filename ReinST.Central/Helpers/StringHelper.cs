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


        public static string TrimToMaxSize(string input, int max)
        {
            return ((input != null) && (input.Length > max)) ?
                input.Substring(0, max) : input;
        }

        public static string RandomStr()
        {
            string rStr = Path.GetRandomFileName();
            rStr = rStr.Replace(".", "");
            return rStr;
        }

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

        public static string GenerateDateTimeStamp()
        {
            return DateTime.Now.Year.ToString() + DateTime.Now.Month + DateTime.Now.Day + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
        }

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

        private static string ExtractYoutubeID(string youtubeURL)
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

        public static string FormatMoney(string input)
        {
            double dInput = Convert.ToDouble(input);
            return dInput.ToString("C", CultureInfo.CreateSpecificCulture("en-PH"));
        }

        public static string FormatMoney(double input)
        {
            try
            {
                return string.Format("{0:f}", input);
            }
            catch
            {
                return "";
            }
        }

        public static string CurrencyToText(string strInput)
        {
            if (strInput == null || strInput.Length <= 0)
            {
                throw new ArgumentNullException();
            }
            try
            {
                strInput = strInput.Replace(",", "").Replace("Php", "").Trim();
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

        public static string StripHTML(string htmlString)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlString);

            if (htmlDoc == null)
                return htmlString;

            return htmlDoc.DocumentNode.InnerText;
        }

        public static string MD5(string input)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            byte[] originalBytes = ASCIIEncoding.Default.GetBytes(input);
            byte[] encodedBytes = md5.ComputeHash(originalBytes);

            return BitConverter.ToString(encodedBytes).Replace("-", "");
        }

        public static string GenerateBCryptHash(string input)
        {
            //adjust salt level here below"
            string salt = BCrypt.GenerateSalt();

            return BCrypt.HashPassword(input.Trim(), salt);
        }

        public static bool VerifyBCryptHash(string plainText, string hash)
        {
            return BCrypt.CheckPassword(plainText, hash);
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


    }
}
