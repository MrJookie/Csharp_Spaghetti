using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections;

namespace ChatServer
{
    class Program
    {
        public static ConcurrentDictionary<TcpClient, string> connectedUsers = new ConcurrentDictionary<TcpClient, string>(10, 10);

        static void Main(string[] args)
        {
            TcpClient clientSocket;
            TcpListener serverSocket;
            int port;

            if (args.Length > 0)
                port = Convert.ToInt32(args[0]);
            else
                port = 9147;

            try
            {
                serverSocket = new TcpListener(IPAddress.Any, port);
                serverSocket.Start();

                Console.WriteLine("Server is listening on *:" + port);

                while (true)
                {
                    clientSocket = serverSocket.AcceptTcpClient();

                    int bufferSize = clientSocket.ReceiveBufferSize;
                    byte[] receivedPacket = new byte[bufferSize];
                    string userName;
                    string currDatetime = DateTime.Now.ToString("H:mm ");

                    NetworkStream networkStream = clientSocket.GetStream();
                    networkStream.Read(receivedPacket, 0, bufferSize);

                    byte[] nickNamePacket = new byte[receivedPacket.Length - 1];
                    Buffer.BlockCopy(receivedPacket, 1, nickNamePacket, 0, nickNamePacket.Length);

                    userName = System.Text.Encoding.ASCII.GetString(nickNamePacket);
                    userName = userName.Substring(0, userName.IndexOf('\0'));

                    connectedUsers.TryAdd(clientSocket, userName);

                    Console.WriteLine(currDatetime + userName + ": connected");
                    broadcast(userName, "connected", 1);

                    clientHandler client = new clientHandler();
                    client.initializeClient(clientSocket, userName, connectedUsers);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Binding failed. Port " + port + " is in use");
            }
        }

        private static byte[] Combine(byte[] a, byte[] b)
        {
            byte[] c = new byte[a.Length + b.Length];
            System.Buffer.BlockCopy(a, 0, c, 0, a.Length);
            System.Buffer.BlockCopy(b, 0, c, a.Length, b.Length);
            return c;
        }

        private static string getUserNames()
        {
            string formattedString = null;
            foreach (var client in connectedUsers)
            {
                formattedString += client.Value + "\r\n";
            }

            return formattedString;
        }

        public static void broadcast(string userName, string message, int eventType)
        {
            string currDatetime = DateTime.Now.ToString("H:mm ");

            foreach (var client in connectedUsers)
            {
                TcpClient broadcastSocket = client.Key;
                NetworkStream broadcastStream = broadcastSocket.GetStream();

                if (eventType == 1) //connected
                {
                    /*
                    byte[] packetType = { 0x01 };
                    byte[] connectedPacket = Encoding.ASCII.GetBytes(currDatetime + userName + ": " + message + '\0');
                    byte[] sendPacket = Combine(packetType, connectedPacket);

                    broadcastStream.Write(sendPacket, 0, sendPacket.Length);
                    broadcastStream.Flush();
                    */

                    byte[] packetType2 = { 0x02 };
                    byte[] nickNamePacket = System.Text.Encoding.ASCII.GetBytes(getUserNames() + '\0');
                    byte[] sendPacket2 = Combine(packetType2, nickNamePacket);

                    broadcastStream.Write(sendPacket2, 0, sendPacket2.Length);
                    broadcastStream.Flush();
                }
                else if (eventType == 2) //send message
                {
                    byte[] packetType = { 0x01 };
                    byte[] messagePacket = Encoding.ASCII.GetBytes(currDatetime + userName + ": " + message + '\0');
                    byte[] sendPacket = Combine(packetType, messagePacket);

                    broadcastStream.Write(sendPacket, 0, sendPacket.Length);
                    broadcastStream.Flush();
              
                }
                else if (eventType == 3) //disconnected
                {
                    /*
                    byte[] packetType = { 0x01 };
                    byte[] disconnectedPacket = Encoding.ASCII.GetBytes(currDatetime + userName + ": " + message + '\0');
                    byte[] sendPacket = Combine(packetType, disconnectedPacket);

                    broadcastStream.Write(sendPacket, 0, sendPacket.Length);
                    broadcastStream.Flush();
                    */

                    byte[] packetType2 = { 0x02 };
                    byte[] nickNamePacket = System.Text.Encoding.ASCII.GetBytes(getUserNames());
                    byte[] sendPacket2 = Combine(packetType2, nickNamePacket);

                    broadcastStream.Write(sendPacket2, 0, sendPacket2.Length);
                    broadcastStream.Flush();
                }

                broadcastStream.Flush();
            }
        }
    }

    public class clientHandler
    {
        TcpClient clientSocket;
        string userName;
        ConcurrentDictionary<TcpClient, string> connectedUsers;

        public void initializeClient(TcpClient receiveClientSocket, string receiveUserName, ConcurrentDictionary<TcpClient, string> receiveUsers)
        {
            this.clientSocket = receiveClientSocket;
            this.userName = receiveUserName;
            this.connectedUsers = receiveUsers;

            Thread worker = new Thread(clientStream);
            worker.Start();
        }

        private void clientStream()
        {
            while (true)
            {
                string currDatetime = DateTime.Now.ToString("H:mm ");

                try
                {
                    int bufferSize = clientSocket.ReceiveBufferSize;
                    byte[] receivedPacket = new byte[bufferSize];
                    string message;
                    NetworkStream networkStream;

                    networkStream = clientSocket.GetStream();
                    networkStream.Read(receivedPacket, 0, bufferSize);

                    byte[] messagePacket = new byte[receivedPacket.Length - 1];
                    Buffer.BlockCopy(receivedPacket, 1, messagePacket, 0, messagePacket.Length);

                    message = System.Text.Encoding.ASCII.GetString(messagePacket);
                    message = message.Substring(0, message.IndexOf('\0'));

                    if (String.IsNullOrEmpty(message))
                        throw new Exception();
                    else
                    {
                        Console.WriteLine(currDatetime + userName + ": " + message);
                        Program.broadcast(userName, message, 2);
                    }
                }
                catch (Exception)
                {
                    string userName;
                    if (connectedUsers.TryRemove(clientSocket, out userName))
                    {
                        Console.WriteLine(currDatetime + userName + ": disconnected");
                        //Program.broadcast(userName, "disconnected", 3);
                    }

                    Thread.CurrentThread.Abort();
                }
            }
        }
    }
}
