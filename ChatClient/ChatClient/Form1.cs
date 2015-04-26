using System;
using System.Windows.Forms;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Drawing;

namespace ChatClient
{
    public partial class Form1 : Form
    {
        TcpClient clientSocket;
        NetworkStream serverStream;
        string messagesData;
        string connectedUsersData;
        bool isConnected;
        Thread worker;

        public Form1()
        {
            InitializeComponent();
            toggleFields();

            KeyPreview = true;
            KeyDown += new KeyEventHandler(Form1_KeyDown);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (isConnected)
                    button_send.PerformClick();
                else
                    button_connect.PerformClick();
            }
        }

        private void toggleFields()
        {
            if(isConnected)
            {
                textBox_ipaddr.Enabled = false;
                textBox_port.Enabled = false;
                textBox_nickname.Enabled = false;
                button_connect.Text = "disconnect";

                textBox_connected_users.Enabled = true;
                textBox_connected_users.BackColor = Color.White;
                textBox_messages.Enabled = true;
                textBox_messages.BackColor = Color.White;
                textBox_message.Enabled = true;
                button_send.Enabled = true;

                textBox_message.Focus();
            }
            else
            {
                textBox_ipaddr.Enabled = true;
                textBox_port.Enabled = true;
                textBox_nickname.Enabled = true;
                button_connect.Text = "connect";

                textBox_connected_users.Text = "";
                textBox_connected_users.Enabled = false;
                textBox_connected_users.BackColor = SystemColors.Control;
                textBox_messages.Enabled = false;
                textBox_messages.BackColor = SystemColors.Control;
                textBox_message.Enabled = false;
                button_send.Enabled = false;
            }
        }

        private byte[] Combine(byte[] a, byte[] b)
        {
            byte[] c = new byte[a.Length + b.Length];
            System.Buffer.BlockCopy(a, 0, c, 0, a.Length);
            System.Buffer.BlockCopy(b, 0, c, a.Length, b.Length);
            return c;
        }

        private void button_connect_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                clientSocket.Close();
                serverStream.Close();
                worker.Abort();

                isConnected = false;
                toggleFields();
            }
            else
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;

                    clientSocket = new TcpClient();
                    clientSocket.Connect(textBox_ipaddr.Text, Convert.ToInt32(textBox_port.Text));
                    serverStream = clientSocket.GetStream();

                    byte[] packetType = { 0x01 };
                    byte[] nickName = System.Text.Encoding.ASCII.GetBytes(textBox_nickname.Text + '\0');
                    byte[] sendPacket = Combine(packetType, nickName);

                    serverStream.Write(sendPacket, 0, sendPacket.Length);
                    serverStream.Flush();

                    worker = new Thread(receiveMessage);
                    worker.IsBackground = true;
                    worker.Start();

                    isConnected = true;
                    toggleFields();
                }
                catch (Exception)
                {
                    isConnected = false;
                    toggleFields();

                    MessageBox.Show("Could not connect to the server");
                }
            }
        }

        private void button_send_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(textBox_message.Text))
                {
                    byte[] packetType = { 0x01 };
                    byte[] message = System.Text.Encoding.ASCII.GetBytes(textBox_message.Text + '\0');
                    byte[] sendPacket = Combine(packetType, message);

                    serverStream.Write(sendPacket, 0, sendPacket.Length);
                    serverStream.Flush();

                    textBox_message.Text = "";
                }
            }
            catch (Exception)
            {
                isConnected = false;
                toggleFields();
            }
        }

        private void receiveMessage()
        {
            while (true)
            {
                try
                {
                    serverStream = clientSocket.GetStream();

                    int bufferSize = clientSocket.ReceiveBufferSize;
                    byte[] receivedPacket = new byte[bufferSize];
                    serverStream.Read(receivedPacket, 0, bufferSize);

                    byte[] packetData = new byte[receivedPacket.Length - 1];
                    Buffer.BlockCopy(receivedPacket, 1, packetData, 0, packetData.Length);

                    if (receivedPacket[0] == 0x02)
                    {
                        connectedUsersData = System.Text.Encoding.ASCII.GetString(packetData);
                        updateConnectedUsersTextbox();
                    }
                    else
                    {
                        string returndata = System.Text.Encoding.ASCII.GetString(packetData);
                        messagesData = "" + returndata;
                        updateMessageTextbox();
                    }
                }
                catch (Exception)
                {
                    serverDropped();
                    clientSocket.Close();
                    serverStream.Close();
                    Thread.CurrentThread.Abort();
                }
            }
        }

        private void serverDropped()
        {
            if (this.InvokeRequired)
                this.Invoke(new MethodInvoker(serverDropped));
            else
            {
                isConnected = false;
                toggleFields();
            }
        }

        private void updateMessageTextbox()
        {
            if (this.InvokeRequired)
                this.Invoke(new MethodInvoker(updateMessageTextbox));
            else
            {
                if(String.IsNullOrEmpty(textBox_messages.Text))
                    textBox_messages.AppendText(messagesData);
                else
                    textBox_messages.AppendText(Environment.NewLine + messagesData);
            }
        }

        private void updateConnectedUsersTextbox()
        {
            if (this.InvokeRequired)
                this.Invoke(new MethodInvoker(updateConnectedUsersTextbox));
            else
            {
                textBox_connected_users.Text = connectedUsersData;
            }
        }
    }
}