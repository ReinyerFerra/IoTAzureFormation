using System;

namespace ConnectedServiceExample
{
    class Program
    {
         static void Main(string[] args)
        {
            while (true)
            {
                var messageRx = AzureIoTHub.ReceiveCloudToDeviceMessageAsync().Result;
                Console.WriteLine(messageRx);
            }
        }
    }
}
