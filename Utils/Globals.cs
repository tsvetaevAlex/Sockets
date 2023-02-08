public static class Globals
{

    public static string Host = "";
    
    public static string Login = "";
    
    public static string Password = "";

    public static int TryCount = 5;
    
    public static bool isEnvironmentPrepared = false;
    
    public static SensorType CameraType = SensorType.Undefined;
    
    public static string remoteDirectory = "/tftpboot/boot/";

    public static SftpClient? DeviceSFTP = null;
    public static ShellStream Picocom;
}