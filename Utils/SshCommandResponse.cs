namespace simicon.automation.Utils
{
    public class SshCommandResponse
    {
        public int ExitCode;
        public string output;

        public SshCommandResponse()
        {
            ExitCode = 0;
            output = string.Empty;
        }

        public void Wipe()
        {
            ExitCode = 0;
            output = string.Empty;
        }


    }
}
