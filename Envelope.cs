
namespace automation
{
    public class Envelope
    {
        public string messageToSend;
        public string expectedResponseCOntent;
        public VerificationType VerT;
        public bool responceAwaiting;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>- string message to send
        /// <param name="expected"></param>string Expected content in Responce mrssage
        /// <param name="vt"></param> Veirfication Type. is responce Contains Expected content or Equal to
        /// <param name="isWait"></param>SHould we wait for responce or not
        public Envelope(string request, string expected, VerificationType vt, bool isWait)
        { 
            messageToSend = request;
            expectedResponseCOntent = expected;
            VerT = vt;
            responceAwaiting = isWait;
        }

        public void SetRequestMEssage(string message)
        {
            messageToSend = message;
        }
        public void SetExpetedContent(string content)
        {
            expectedResponseCOntent = content;
        }
        public void SetVerificationType(VerificationType vt)
        {
            VerT = vt;
        }
        public void SetAwaitingStatus(bool awaiting)
        {
            responceAwaiting  = awaiting;
        }

    }
}
