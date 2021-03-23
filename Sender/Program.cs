using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Sender
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 3) return;

            String ip = args[0];
            int port = Int32.Parse(args[1]);
            String target = args[2];

            new Program(ip, port, target);
        }

        public Program(String ip, int port, String target)
        {
            // 원격 서버와 연결
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(ip), port);
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.Connect(ipep);

            // 원격 서버에서의 응답을 받음
            Byte[] _data = new Byte[1024];

            client.Receive(_data);
            Console.WriteLine(Encoding.Default.GetString(_data).TrimEnd('\0'));

            // 원격 서버로 메세지 보냄
            client.Send(Encoding.Default.GetBytes(target));

            _data = new Byte[1024];
            client.Receive(_data);
            Console.WriteLine(Encoding.Default.GetString(_data).TrimEnd('\0'));

            //연결 종료
            client.Close();
        }
    }
}
