namespace simicon.automation.Utils;
public class Connection
{
    public static SftpClient? _connectionObject = null;
    public static string _name = "";
    public static bool _isConnected = false;
    public static bool _isNull = false;


    public Connection(SftpClient? connectionObject, string Name, bool isConnected, bool isNull)
    {
        _connectionObject = connectionObject;
        _name = Name;
        _isConnected = isConnected;
        _isNull = isNull;
    }
}
