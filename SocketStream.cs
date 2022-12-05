using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simicon.automation;

    public static class SocketStream
    {

        public static void Send(ShellStream stream, string message, string expectedKeyWord, string TAG="Send.Stream")
        {
            Logger.Write($"message to send: {message}", "SshSocket");
            stream.WriteLine(message);
            StringBuilder sb = new StringBuilder();
            string output = "";
            string buffer = "";

            while (stream.CanRead)
            {
                buffer = stream.ReadLine();
                Logger.Write($"Response String received: {buffer}.", "ShellStream");
                sb.Append(buffer);
            }
            OutputVerification.Verify(
                command: message,
                output: sb.ToString(),
                expectedContent: expectedKeyWord);

    }// End of Send
    /*

            StringBuilder sb = new StringBuilder();
        string output = "";
        string buffer = "";
        while (CameraSocketStream.CanRead)
        {
            buffer = CameraSocketStream.ReadLine();
            Console.WriteLine($"Response String received: {buffer}.");
            sb.Append(buffer);
            if (buffer.Contains("Terminal ready"))
            {
                break;
            }
        }
        output = sb.ToString();
        Console.WriteLine(output);
     
    */
}

