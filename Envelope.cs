
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework.Internal;
using Renci.SshNet;

namespace simicon.automation;

    public class Envelope
    {
        public string messageToSend;
        public string expectedResponseContent;
        public VerificationType VerT;
        public string testName;
        public SshClient targetSocket;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>- string message to send
    /// <param name="expected"></param>string Expected content in Responce mrssage
    /// <param name="vt"></param> Veirfication Type. is responce Contains Expected content or Equal to
    /// <param name="isWait"></param>SHould we wait for responce or not
    public Envelope(string request, VerificationType vt)
    {
        messageToSend = request;
        VerT = vt;
    }
    public Envelope(string testname, string request, string expectedContent, VerificationType vt, SshClient socket)
    {
        testName = testname;
        messageToSend = request;
        expectedResponseContent = expectedContent;
        VerT = vt;
        targetSocket=socket;
    }
    public Envelope(string testname, string request, string expectedContent, VerificationType vt)
        { 
            messageToSend = request;
            expectedResponseContent = expectedContent;
            VerT = vt;
            testName = testname;
    }

        public void SetRequestMEssage(string message)
        {
            messageToSend = message;
        }
        public void SetExpetedContent(string content)
        {
            expectedResponseContent = content;
        }
        public void SetVerificationType(VerificationType vt)
        {
            VerT = vt;
        }
    }

