using Device;
using Renci.SshNet;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.ComponentModel.DataAnnotations;

namespace sshClient
{
    public class Device
    {
        #region Fields
        private const string EmptyString = "";
        static SftpClient applicationSshCLientclient;

        #endregion Fields
        //private Envelope testInfo;
        public Envelope recivedEnelope;
        public Device(Envelope envelope)
        {
            Console.WriteLine("class Device constructor.");
            recivedEnelope = envelope;
        }//constructor

        public SshClient GetConnection(Envelope envelope) 
        {
            Console.WriteLine("class Device getConnectiton.");
            if (envelope is null)
            {
                Console.WriteLine("Precondition instance of class [Envelope], not provided or indtance of enelope corrupted.");
                throw new ArgumentNullException(nameof(envelope));
            }

            //

            PasswordConnectionInfo CI =  new PasswordConnectionInfo(
                 host: recivedEnelope.destination.Host,
                 port: recivedEnelope.destination.port,
                 username: recivedEnelope.Login,
                 password: recivedEnelope.Password
             );

            ///
            //var auth = new PasswordAuthenticationMethod(DeviceDetails.Login, DeviceDetails.Password);
            //ConnectionInfo connectionInfo = new ConnectionInfo(
            //        DeviceDetails.destination.Host,
            //        DeviceDetails.destination.port,
            //        DeviceDetails.Login,
//                     new AuthenticationMethod( CI );
            using (SftpClient connection = new SftpClient(CI))
            {
                Console.WriteLine("class Device in client if.");
                try
                {
                    connection.Connect();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception Happened on eviseSshCLientclient.Connect(): {0}", e.Message);
                }
                Console.WriteLine("class Device in client: {0}", connection);
                SendMessage(recivedEnelope.requestMessage);
                applicationSshCLientclient = connection;
            }


        }//GetConnection


        private string SendMessage(string msg)
        {
            Console.WriteLine("Attempt to send message: {0}", recivedEnelope.requestMessage);
            Renci.SshNet.ShellStream stream = applicationSshCLientclient.CreateShellStream("dumb", 0, 0, 0, 0, 1000);

            
            stream.Write(msg);

            string response = stream.Read();
            Console.WriteLine("Response: {0}", response);



            return response;
        }
    }
}
