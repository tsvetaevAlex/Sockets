
using automation;
using Renci.SshNet;


// test method to debug path of code. will be refind later
public class CamTest001
{
    public CamTest001() { }

    public void Run()
    {
        Envelope testData = new Envelope(
            testname: "CamTest001",
            request: "sqlite3 /tftpboot/boot/conf/kris.sql3 \"insert or replace into tblSettings (tName,tValue) values ('SENSORAPP_MSENSORATPORT','')\"",
            expectedContent: "",
            vt: VerificationType.None
            );
        Helper.Execute(testData);
    }

}
