using Renci.SshNet;


namespace CameraTests;


public class Device : CameraTestSuite
{ 
    private int AttemptCount = 0;
    private int MAXattemptsQTY = 5;

    public Destination DeviceData { get;  set; }

    public Device(Destination TargetDeviceData) => DeviceData = TargetDeviceData;

    public Device()
        {
        }// end of default constructor

        public SshClient? GetConnection(Destination TargetDeviceData)
        {
            AttemptCount++;
            Console.WriteLine("{0} Attempt to send request.", AttemptCount);
            try
            {
                Renci.SshNet.SshClient device = new Renci.SshNet.SshClient(TargetDeviceData.IP, TargetDeviceData.login, TargetDeviceData.password);
                DeviceData.SetSocketPointer(device);
                return device;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: \n{0} happend. in GetConnection",e.Message);
                Console.WriteLine("coonection Attempt {0} of {1} to send request.FAILED", AttemptCount,MAXattemptsQTY);
                if (AttemptCount >= MAXattemptsQTY)
                {
                    Console.WriteLine("all allowed attempts are elapsed failed to connect to device.");
                    {
                        Console.WriteLine("FATAL ERROR: test Run FAILED.due - To connection failed");
                    }
                }
                return null;
            }
        }//end of GetConnection


        public string SendMessage(string request) {
            #region Send RequestMessage
            Console.WriteLine("Attempt to send request: {0}", request);
            //var stream = DeviceData.CreateShellStream("", 0, 0, 0, 0, 0);
            ShellStream stream = DeviceData.SocketPointer.CreateShellStream("", 0, 0, 0, 0, 0);

            stream.WriteLine(request + "\n");
            #endregion

            #region Wait for ResponseMessage
            int count  = 1;
            while (count <=100)
            {
                count++;
                Console.WriteLine("{0} attemmpt to read response: ", count);
                string response = stream.ReadLine();
                if (response.Length > 0)
                {
                    Console.WriteLine("{0} response received: {1}", count, response);
                    Thread.Sleep(200);
                    return response;
                }
            #endregion
            }
            return "ERROR: time to wait elapsed no response received. please check recipient servoce status.";
        }//rnd of SendMessage
}// end of clss Device