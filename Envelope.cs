using Renci.SshNet;
using System.Net.Sockets;

namespace Device;
public class Envelope1
{
    private const string EmptyString = "";
    public Destination destination;
    public string Login = string.Empty;
    public string Password = string.Empty;
    public string IP = string.Empty;
    public int Port = 0;
#pragma warning disable CS8618
    // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

#pragma warning restore CS8618
    public Envelope1(string ip, int port, string arglogin, string argpassword, SshClient socket,
    (
        destination = new Destination
        (
        Login arglogin;
        Password argpassword;
        Port port;
        ip ip;
        );
    );
}//class Evenlope
