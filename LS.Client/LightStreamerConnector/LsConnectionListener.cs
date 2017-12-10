using Lightstreamer.DotNet.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LS.Client
{
  
    class LsConnectionListener : IConnectionListener
    {
        private long bytes = 0;
       

        public LsConnectionListener() { }

      

        public void OnConnectionEstablished()
        {
            Console.WriteLine("Connected to LS Successfully!");
           // lblConnecionStatus.Text = "Connected Successfully to LS";
        }

        public void OnSessionStarted(bool isPolling)
        {
            if (isPolling)
            {
                Console.WriteLine("smart polling session started");
            }
            else
            {
                Console.WriteLine("streaming session started");
            }
        }

        public void OnNewBytes(long newBytes)
        {
            this.bytes += newBytes;
        }

        public void OnDataError(PushServerException e)
        {
            Console.WriteLine("data error");
            Console.WriteLine(e);
        }

        public void OnActivityWarning(bool warningOn)
        {
            if (warningOn)
            {
                Console.WriteLine("connection stalled");
            }
            else
            {
                Console.WriteLine("connection no longer stalled");
            }
        }

        public void OnClose()
        {
            Console.WriteLine("total bytes: " + bytes);
        }

        public void OnEnd(int cause)
        {
            Console.WriteLine("connection forcibly closed");
        }

        public void OnFailure(PushServerException e)
        {
            Console.WriteLine("server failure");
            Console.WriteLine(e);
        }

        public void OnFailure(PushConnException e)
        {
           
            Console.WriteLine("connection failure");
            Console.WriteLine(e);
        }
    }
}
