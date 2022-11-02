
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework.Internal;
using System.Reflection;


namespace simicon.automation;

    public class Envelope
    {
        public string messageToSend;
        public string expectedResponseContent;
        public VerificationType VerT;
        public string testName;         
        public ConnectionPointers _ConnectionPointers;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>- string message to send
        /// <param name="expected"></param>string Expected content in Responce mrssage
        /// <param name="vt"></param> Veirfication Type. is responce Contains Expected content or Equal to
        /// <param name="isWait"></param>SHould we wait for responce or not
        public Envelope(string testname, string request, string expectedContent, VerificationType vt)
        {
        Console.WriteLine("!!!!!!!!!!!!!!Checkpoint Envelope param Constructor");

            messageToSend = request;
            expectedResponseContent = expectedContent;
            VerT = vt;
            testName = testname;
            ConnectionPointers _pointers = new ConnectionPointers();
            _ConnectionPointers = _pointers.GetConnectionPointers();
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

