using simicon.automation.Tests;

namespace simicon.automation;

public class Connections : TestRun

{
    private string LogPrefix = string.Empty;
    private SshClient? _DeviceSSH;
    private SftpClient? _DeviceSFTP;// secured ftp to device. to be able download files(snaphots) from device
    private ShellStream? _DeviceShell;
    private ShellStream? _cameraShell;
    private int SshCounter = 1;
    /// <summary>
    /// initializa all reuqired connections: SSH  and SFTP to device, console to Camera
    /// </summary>
    /// <param name="host"></param>
    /// <param name="loginname"></param>
    /// <param name="pswd"></param>
    /// 

    public Connections()
    {
        GetCreds();
        LogPrefix = " simicon.automation.ConnectionPointers;Code Line: ";
        Console.WriteLine($"{LogPrefix}26; Connections : TestRun. Default Constructor");
    }

    private void GetCreds()
    {
        log.Route($"{LogPrefix}34 GetCreds()");
        log.Route($"{LogPrefix}37 GetCreds.GetHost; {GetHost()}");
        log.Route($"{LogPrefix}39 GetCreds.GetLogin; {GetLogin()}");
        log.Route($"{LogPrefix}41 GetCreds.GetPassword; {GetPassword()}");
    }

    public SshClient GetDeviceSSH()
    {
        if (_DeviceSSH == null)
        {
            IniSSHConnection();
        }
        return _DeviceSSH;
    }
    public SftpClient GEtDeviceSFTP()
    {
        if (_DeviceSFTP == null)
        {
            InitSFTPConnection();
        }
        return _DeviceSFTP;
    }


    public ShellStream GetPicococm()
    {
        if (_cameraShell == null)
        {
            AuthorizePicocom();
        }
        return _cameraShell;
    }
    //TODO:
    //public ShellStream GetDeviceShell()
    //{
    //    if (_DeviceShell == null)
    //    {

    //    }
    //    else
    //    {
    //        return _DeviceShell;
    //    }
    //}       
    public void InitCameraConsole()
    {
        if (_cameraShell == null)
            AuthorizePicocom();
    }

    //SSH
    /// <summary>
    /// create SSH connectionObject to Device
    /// </summary>
    public void IniSSHConnection()
    {
        GetCreds();
        log.Route(NewLine + "ConnectionPointers.IniSSHConnection()" + NewLine);

        int i = 1;
        int m = 1000;
        while (true)
        {
            try
            {
                _DeviceSSH = new SshClient(Host, Login, Password);
                log.Networtk($"{LogPrefix}106 SSH Device Connection created. Pointer  is: [{_DeviceSSH}].");
                _DeviceSSH.Connect();
                SshClient = _DeviceSSH;
                break;
            }
            catch (SocketException e)
            {
                log.Warning($"SSH Attemp is Failed due-to exceptio has been thrown: {e.ToString()}");

                if (i < m)
                {
                    i++;
                    continue;
                }
                else
                    break;


            }
        }// end of while
        #region wait for connection acknowledgment
        log.Info($"{LogPrefix}122; open shell console with device");
        if (_DeviceSSH != null)
        {
            _DeviceShell = _DeviceSSH.CreateShellStream("", 0, 0, 0, 0, 0);
            log.Info($"{LogPrefix}126 INFO: wait for connection acknowledgment");
            if (_DeviceShell.DataAvailable)
            {
                while (_DeviceShell.CanRead)
                {
                    string responseString = _DeviceShell.ReadLine();

                    {
                        log.Info("initConnectionPointers, response received: 'Processing /etc/profile... Done'");
                        log.Info($"resp: '{responseString}'");
                        log.Info($"!!!SSH CONNECTION APPROVED!!!");
                    }
                }
            }
        }
        else
        {
            log.Failure($"{LogPrefix}149 ERROR, due-to unknown internal errort ssh connection has dropped into null");
            log.Info($"{SshCounter} from {MAXTryCount}, Attempts has been used");

            if (SshCounter <  MAXTryCount)
            {
                log.Info($"available {MAXTryCount - SshCounter} more attemptsto use.");
            }

        }
        if (_DeviceShell.DataAvailable)
        {
            while (_DeviceShell.CanRead)
            {
                string ResponseString = _DeviceShell.ReadLine();

                if (ResponseString.Contains(authorizationApproval))
                {
                    log.Info("initConnectionPointers, response received: 'Processing /etc/profile... Done'");
                    log.Info($"resp: '{ResponseString}'");
                    log.Info($"!!!SSH CONNECTION APPROVED!!!");
                }
            }
        }
    }// End of IniSSHConnection
    #endregion

    /// <summary>
    ///  crete SFTP connectionObject to Device
    /// </summary>
    public void InitSFTPConnection()
    {
        #region create SFTP connection with Device
        log.Route("Device InitSFTPConnection");
        int i = 1;
        int m = 1000;
        while (true)
        {
            try
            {
                _DeviceSFTP = new SftpClient(Host, Login, Password);
                _DeviceSFTP.Connect();
                if (_DeviceSFTP.IsConnected)
                {
                    log.Info($"SFTP connection createn poiner is: {_DeviceSFTP}");
                    SftpClient = _DeviceSFTP;
                    log.Route(NewLine + $"____________________End IniSftpConnection_pointer: {_DeviceSFTP}______________" + NewLine);
                    //return _DeviceSFTP;
                    log.Networtk($"InitSFTPConnection connection created successfully: '{_DeviceSFTP}'");
                    break;
                }
            }
            catch (SocketException e)
            {
                log.Warning($"SFTP Attemp is Failed due-to exceptio has been thrown: {e.ToString()}");

                if (i < m)
                {
                    i++;
                    continue;
                }
                else
                    break;
            }

            #endregion
        }
    }// end of InitSFTPConnection

        //killall picocom
        // no process killed
        // no response in case any process killed

        //AuthorizePicocom
        /// <summary>
        /// create connectionObject to camera console using picocom command
        /// </summary>
        public ShellStream AuthorizePicocom()
        {
            log.Route("has entered into AuthorizePicocom");
            ShellStream returnvalue = null;

            if (_DeviceSSH is not null)
            {
                ShellStream _DeviceShell = _DeviceSSH.CreateShellStream("picocom Terminal", 80, 180, 800, 600, 1024);
            }
            {
                IniSSHConnection();
            }
            /*

             [root@MDXXXX ~]# killall picocom
            killall: picocom: no process killed

             */
            string picocomRequest = "picocom -b 115200 /dev/tts/camera --imap lfcrlf";
            string expectedContent = "Terminal ready";

            StringBuilder sb = new StringBuilder();
            string buffer = "";
            log.Info($"send Camera connectionObject string to create camera data channel: '{picocomRequest}'.");
        //send auth request
        //_DeviceShell.Flush();
        _DeviceShell.WriteLine(picocomRequest);
        log.Networtk(picocomRequest);
        Thread.Sleep(1000); // Prevents the shell from losing the output if it becomes too slow


            //receive response. expected Content is: "Terminal ready";
            if (_DeviceShell.DataAvailable)
            {
                while (_DeviceShell.DataAvailable)
                {
                    _DeviceShell.WriteLine("killall picocom");
                    buffer = _DeviceShell.ReadLine();
                if (buffer != "")
                {
                    log.Info($"responsestring: {buffer}");
                }
                if (buffer == "Terminal ready")
                {
                    log.Success($"Expected Response String received: {buffer}");
                    log.Networtk($"picocom response: {buffer}");
                }  
                    sb.Append(buffer);
                    if (buffer.Contains(expectedContent))
                    {
                        log.Success("!!!Camera Console Connected: 'Terminal Ready' message received.");
                        returnvalue = _DeviceShell;
                    }
                    if (buffer.Contains("'FATAL"))
                    {
                        string ERRmsg = "ASSERT TestRun Failure. Ccnnot connect to camera service. Stream port busy.";
                        log.Failure(ERRmsg);
                        Assert.Fail(ERRmsg);
                    }
                }// end of while (stream.DataAvailable)
            }// end of if (stream.DataAvailable)
        return returnvalue;

    }//end of AuthorizePicocom

}// end o class ConnectionPointers