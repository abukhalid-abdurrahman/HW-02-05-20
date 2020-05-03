using System;
using System.Text;
using System.Threading;
using System.Net;
using System.IO;

namespace Example
{
    public class Multithread
    {
        public static void ShowExample()
        {
            Console.WriteLine("Многопоточность...\nПример: Регистрация");
            Thread threadRegistration = new Thread(() =>
            {
                try
                {
                    WebRequest webRequest = WebRequest.Create("http://www.example.com/registration.php?");
                    webRequest.Method = "POST";

                    string postContent = "name=faridun&surname=berdiev&age=17&telNumber=992988889000";
                    byte[] byteArray = Encoding.UTF8.GetBytes(postContent);

                    webRequest.ContentLength = byteArray.Length;

                    Stream dataStream = webRequest.GetRequestStream();
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();

                    WebResponse response = webRequest.GetResponse();
                    Console.WriteLine(((HttpWebResponse)response).StatusDescription);

                    using (dataStream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(dataStream);
                        string responseFromServer = reader.ReadToEnd();
                        Console.WriteLine(responseFromServer);
                    }
                    response.Close();
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            });
            threadRegistration.Start();
            Thread threadOutputInfo = new Thread(() =>
            {
                Console.WriteLine("Вы в процессе регистрации...");
            });
            threadOutputInfo.Start();
        }
    }
}