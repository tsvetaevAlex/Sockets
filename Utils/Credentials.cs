using simicon.automation.Tests;
using System.Reflection.PortableExecutable;

namespace simicon.automation.Utils;

public class Credentials : TestRun
{
    private string path = "Credentials.txt";
    public void Get()
    {
        log.Info("@ simicon.automation.Utils.Credentials.Get();");
        log.Route("@ simicon.automation.Utils.Credentials.Get();");

        using (StreamReader reader = new StreamReader(path))
        {
            string buffer = reader.ReadLine();
                    if (buffer is not null)
                    {

                        if (buffer != "")
                        {
                        Console.WriteLine(buffer);

                        if (buffer.Contains('='))
                            {
                                buffer.Split('=');
                                string Name = buffer.Split('=')[0];
                                string Value = buffer.Split('=')[1];
                                if (Name == "Host")
                                {
                                    Host = Value; // set value tp Host property in parent class(TestRun)
                        }
                                if (Name == "login")
                                {
                                    Login = Value; // set value tp Host property in parent class(TestRun)
                        }
                                if (Name == "Password")
                                {
                                    Password = Value; // set value tp Host property in parent class(TestRun)
                        }
                            }
                        }
                    }
        }
    }// end of Get(){}


} // end of classs