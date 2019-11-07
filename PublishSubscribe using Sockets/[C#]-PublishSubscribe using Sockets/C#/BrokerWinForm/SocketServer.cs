
//Houssem Dellai
//houssem.dellai@live.com
//This app is based on a simple code from SharpDevelop.
  
using System;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Xml.Serialization;
using System.IO;
using System.Text;

namespace BrokerWinForm
{
	/// <summary>
	/// Description of SocketServer.	
	/// </summary>
	public class SocketServer : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.RichTextBox richTextBoxReceivedMsg;
		private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBoxMsg;
		private System.Windows.Forms.Button buttonStopListen;
        private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.Button buttonStartListen;
		private System.Windows.Forms.Button buttonClose;
		
		
		const int MAX_CLIENTS = 10;
		
		public AsyncCallback pfnWorkerCallBack ;
		private  Socket m_mainSocket;
		private  Socket [] m_workerSocket = new Socket[10];
        private RichTextBox richTextBoxFromSubscribers;
        private Label label6;
        private Label label7;
        private RichTextBox richTextBoxFromPublishers;
		private int m_clientCount = 0;

        //My added attributes

        /// <summary>
        /// Data is the class to contain the published objects with adress of their publishers
        /// and will contain also the requested objects and their subscribers.
        /// </summary>
        Data data = new Data();
        //private List<Publisher> Publishers = new List<Publisher>();

		public SocketServer()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			// Display the local IP address on the GUI
			textBoxIP.Text = GetIP();
		}
		
		[STAThread]
		public static void Main(string[] args)
		{
			Application.Run(new SocketServer());
		}
		
		#region Windows Forms Designer generated code
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent() {
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonStartListen = new System.Windows.Forms.Button();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonStopListen = new System.Windows.Forms.Button();
            this.textBoxMsg = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.richTextBoxReceivedMsg = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.richTextBoxFromSubscribers = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.richTextBoxFromPublishers = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(324, 470);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(88, 24);
            this.buttonClose.TabIndex = 11;
            this.buttonClose.Text = "Close";
            this.buttonClose.Click += new System.EventHandler(this.ButtonCloseClick);
            // 
            // buttonStartListen
            // 
            this.buttonStartListen.BackColor = System.Drawing.Color.Blue;
            this.buttonStartListen.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStartListen.ForeColor = System.Drawing.Color.Yellow;
            this.buttonStartListen.Location = new System.Drawing.Point(227, 16);
            this.buttonStartListen.Name = "buttonStartListen";
            this.buttonStartListen.Size = new System.Drawing.Size(88, 40);
            this.buttonStartListen.TabIndex = 4;
            this.buttonStartListen.Text = "Start Listening";
            this.buttonStartListen.UseVisualStyleBackColor = false;
            this.buttonStartListen.Click += new System.EventHandler(this.ButtonStartListenClick);
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(88, 16);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.ReadOnly = true;
            this.textBoxIP.Size = new System.Drawing.Size(120, 20);
            this.textBoxIP.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Port";
            // 
            // buttonStopListen
            // 
            this.buttonStopListen.BackColor = System.Drawing.Color.Red;
            this.buttonStopListen.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStopListen.ForeColor = System.Drawing.Color.Yellow;
            this.buttonStopListen.Location = new System.Drawing.Point(321, 16);
            this.buttonStopListen.Name = "buttonStopListen";
            this.buttonStopListen.Size = new System.Drawing.Size(88, 40);
            this.buttonStopListen.TabIndex = 5;
            this.buttonStopListen.Text = "Stop Listening";
            this.buttonStopListen.UseVisualStyleBackColor = false;
            this.buttonStopListen.Click += new System.EventHandler(this.ButtonStopListenClick);
            // 
            // textBoxMsg
            // 
            this.textBoxMsg.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxMsg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxMsg.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.textBoxMsg.Location = new System.Drawing.Point(123, 478);
            this.textBoxMsg.Name = "textBoxMsg";
            this.textBoxMsg.ReadOnly = true;
            this.textBoxMsg.Size = new System.Drawing.Size(192, 13);
            this.textBoxMsg.TabIndex = 14;
            this.textBoxMsg.Text = "None";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(88, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(238, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "Message Received From Clients";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(88, 40);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(40, 20);
            this.textBoxPort.TabIndex = 0;
            this.textBoxPort.Text = "8000";
            // 
            // richTextBoxReceivedMsg
            // 
            this.richTextBoxReceivedMsg.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.richTextBoxReceivedMsg.Location = new System.Drawing.Point(88, 87);
            this.richTextBoxReceivedMsg.Name = "richTextBoxReceivedMsg";
            this.richTextBoxReceivedMsg.ReadOnly = true;
            this.richTextBoxReceivedMsg.Size = new System.Drawing.Size(238, 129);
            this.richTextBoxReceivedMsg.TabIndex = 9;
            this.richTextBoxReceivedMsg.Text = "";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Server IP";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 478);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 16);
            this.label3.TabIndex = 13;
            this.label3.Text = "Status Message:";
            // 
            // richTextBoxFromSubscribers
            // 
            this.richTextBoxFromSubscribers.Location = new System.Drawing.Point(220, 275);
            this.richTextBoxFromSubscribers.Name = "richTextBoxFromSubscribers";
            this.richTextBoxFromSubscribers.Size = new System.Drawing.Size(192, 163);
            this.richTextBoxFromSubscribers.TabIndex = 15;
            this.richTextBoxFromSubscribers.Text = "";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(220, 256);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(192, 16);
            this.label6.TabIndex = 16;
            this.label6.Text = "Requests from Subscribers";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(12, 256);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(192, 16);
            this.label7.TabIndex = 18;
            this.label7.Text = "Updates from Publishers";
            // 
            // richTextBoxFromPublishers
            // 
            this.richTextBoxFromPublishers.Location = new System.Drawing.Point(16, 275);
            this.richTextBoxFromPublishers.Name = "richTextBoxFromPublishers";
            this.richTextBoxFromPublishers.Size = new System.Drawing.Size(192, 163);
            this.richTextBoxFromPublishers.TabIndex = 17;
            this.richTextBoxFromPublishers.Text = "";
            // 
            // SocketServer
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(424, 503);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.richTextBoxFromPublishers);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.richTextBoxFromSubscribers);
            this.Controls.Add(this.textBoxMsg);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.richTextBoxReceivedMsg);
            this.Controls.Add(this.buttonStopListen);
            this.Controls.Add(this.buttonStartListen);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxPort);
            this.Name = "SocketServer";
            this.Text = "SocketServer";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
		void ButtonStartListenClick(object sender, System.EventArgs e)
		{
			try
			{
				// Check the port value
				if(textBoxPort.Text == ""){
					MessageBox.Show("Please enter a Port Number");
					return;
				}
				string portStr = textBoxPort.Text;
				int port = System.Convert.ToInt32(portStr);
				// Create the listening socket...
				m_mainSocket = new Socket(AddressFamily.InterNetwork, 
				                          SocketType.Stream, 
				                          ProtocolType.Tcp);
				IPEndPoint ipLocal = new IPEndPoint (IPAddress.Any, port);
				// Bind to local IP Address...
				m_mainSocket.Bind( ipLocal );
				// Start listening...
				m_mainSocket.Listen (4);
				// Create the call back for any client connections...
				m_mainSocket.BeginAccept(new AsyncCallback (OnClientConnect), null);
				
				UpdateControls(true);
			}
			catch(SocketException se)
			{
				MessageBox.Show ( se.Message );
			}

		}

		private void UpdateControls( bool listening ) 
		{
			buttonStartListen.Enabled 	= !listening;
			buttonStopListen.Enabled 	= listening;
		}
	    
		/// <summary>
        /// This is the call back function, which will be invoked when a client is connected
		/// </summary>
		/// <param name="asyn"></param>
		public void OnClientConnect(IAsyncResult asyn)
		{
			try
			{
				// Here we complete/end the BeginAccept() asynchronous call
				// by calling EndAccept() - which returns the reference to
				// a new Socket object
				m_workerSocket[m_clientCount] = m_mainSocket.EndAccept (asyn);
				// Let the worker Socket do the further processing for the 
				// just connected client
				WaitForData(m_workerSocket[m_clientCount]);

                //MessageBox.Show("I am connected to "
                //        + IPAddress.Parse(((IPEndPoint)m_workerSocket[m_clientCount].RemoteEndPoint).Address.ToString()) 
                //        + "on port number "
                //        + ((IPEndPoint)m_workerSocket[m_clientCount].RemoteEndPoint).Port.ToString()
                //    );

				// Now increment the client count
				++m_clientCount;
				// Display this client connection as a status message on the GUI	
				String str = String.Format("Client # {0} connected", m_clientCount);
				textBoxMsg.Text = str;
								
				// Since the main Socket is now free, it can go back and wait for
				// other clients who are attempting to connect
				m_mainSocket.BeginAccept(new AsyncCallback ( OnClientConnect ),null);
            }
			catch(ObjectDisposedException)
			{
				System.Diagnostics.Debugger.Log(0,"1","\n OnClientConnection: Socket has been closed\n");
			}
			catch(SocketException se)
			{
				MessageBox.Show ( se.Message );
			}
			
		}

		public class SocketPacket
		{
			public System.Net.Sockets.Socket m_currentSocket;
			public byte[] dataBuffer = new byte[1];
		}

		/// <summary>
        /// Start waiting for data from the client
		/// </summary>
		/// <param name="soc"></param>
		public void WaitForData(System.Net.Sockets.Socket soc)
		{
			try
			{
				if  ( pfnWorkerCallBack == null ){		
					// Specify the call back function which is to be 
					// invoked when there is any write activity by the 
					// connected client
					pfnWorkerCallBack = new AsyncCallback (OnDataReceived);
				}
				SocketPacket theSocPkt = new SocketPacket ();
				theSocPkt.m_currentSocket 	= soc;
				// Start receiving any data written by the connected client
				// asynchronously
				soc .BeginReceive (theSocPkt.dataBuffer, 0, 
				                   theSocPkt.dataBuffer.Length,
				                   SocketFlags.None,
				                   pfnWorkerCallBack,
				                   theSocPkt);
			}
			catch(SocketException se)
			{
				MessageBox.Show (se.Message );
			}

		}
		
        /// <summary>
        /// This the call back function which will be invoked when the socket
        //  detects any client writing of data on the stream
        /// </summary>
        /// <param name="asyn"></param>
		public  void OnDataReceived(IAsyncResult asyn)
		{
			try
			{
				SocketPacket socketData = (SocketPacket)asyn.AsyncState ;

				int iRx  = 0 ;
				// Complete the BeginReceive() asynchronous call by EndReceive() method
				// which will return the number of characters written to the stream 
				// by the client
				iRx = socketData.m_currentSocket.EndReceive (asyn);
				char[] chars = new char[iRx +  1];
				System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
				int charLen = d.GetChars(socketData.dataBuffer, 
				                         0, iRx, chars, 0);
				System.String szData = new System.String(chars);
				richTextBoxReceivedMsg.AppendText(szData);
                
                //when we are sure we received the entire message
                //pass the Object received into parameter 
                //after removing the flag indicating the end of message
                if (richTextBoxReceivedMsg.Text.Contains("ENDOFMESSAGE"))
                {
                    RetrieveReceivedEventData(
                        richTextBoxReceivedMsg.Text.Remove(richTextBoxReceivedMsg.Text.IndexOf("ENDOFMESSAGE"), 12),
                        socketData.m_currentSocket.RemoteEndPoint
                        );
                }

				// Continue the waiting for data on the Socket
				WaitForData( socketData.m_currentSocket );
			}
			catch (ObjectDisposedException )
			{
				System.Diagnostics.Debugger.Log(0,"1","\nOnDataReceived: Socket has been closed\n");
			}
			catch(SocketException se)
			{
				MessageBox.Show (se.Message );
			}
        }

        /// <summary>
        /// Retrieve the sent Object.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="endPoint"></param>
        private void RetrieveReceivedEventData(string p, EndPoint endPoint)
        {
            EventData receivedEventData = DeserializeReceivedEventData(p);
            if (receivedEventData.Details == null || receivedEventData.Details == "")
            {
                richTextBoxFromSubscribers.Text += "\n/////////////// \n" + richTextBoxReceivedMsg.Text;

                //empty the textbox to read correctly the next request, 
                //to not be confused by "ENDOFMESSAGE" which will appear in every request.
                richTextBoxReceivedMsg.Text = null;

                AnalyseRequestFromSubscriber(endPoint, receivedEventData);
            }
            //Only the publisher sends Details.
            else
            {
                //save the sent event in history
                richTextBoxFromPublishers.Text += "\n/////////////// \n" + richTextBoxReceivedMsg.Text;

                //empty the textbox to read correctly the next request, 
                //to not be confused by "ENDOFMESSAGE" which will appear in every request.
                richTextBoxReceivedMsg.Text = null;

                AnalyseRequestFromPublisher(endPoint, receivedEventData);
            }
        }

        /// <summary>
        /// Objects received from Publishers will be stored.
        /// </summary>
        /// <param name="endPoint"></param>
        /// <param name="receivedEventData"></param>
        private void AnalyseRequestFromPublisher(EndPoint endPoint, EventData receivedEventData)
        {
            //MessageBox.Show("flag outside foreach Publisher");
            //Register the Event in the appropriate Publisher's list
            //If this Publisher already exists, then just add this Event to his list
            Boolean eventAdded = false;
            foreach (Publisher publisher in data.Publishers)
            {
                if (publisher.IpEndPoint == (IPEndPoint)endPoint)
                {
                    publisher.Events.Add(receivedEventData);
                    eventAdded = true;
                }
            }
            //If the Publisher don't exist
            if (!eventAdded)
            {
                data.Publishers.Add(new Publisher((IPEndPoint)endPoint, receivedEventData));
            }
        }

        /// <summary>
        /// Deserialize an XMLString.
        /// </summary>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        private EventData DeserializeReceivedEventData(string xmlString)
        {
            //Deserialize the received Event
            XmlSerializer xmlSerializer;
            MemoryStream memStream = null;
            EventData eventData = new EventData();
            try
            {
                xmlSerializer = new XmlSerializer(typeof(EventData));
                byte[] bytes = new byte[xmlString.Length];
                Encoding.UTF8.GetBytes(xmlString, 0, xmlString.Length, bytes, 0);
                memStream = new MemoryStream(bytes);
                object objectFromXml = xmlSerializer.Deserialize(memStream);
                eventData = (EventData)objectFromXml;
            }
            catch (Exception Ex)
            {
                //throw Ex;
                MessageBox.Show(Ex.Message);
            }
            finally
            {
                if (memStream != null)
                    memStream.Close();
            }
            return eventData;
        }

        /// <summary>
        /// Response to subscribers requests.
        /// </summary>
        /// <param name="endPoint"></param>
        /// <param name="eventData"></param>
        private void AnalyseRequestFromSubscriber(EndPoint endPoint, EventData eventData)
        {
            //look for the requested Event,
            //Loop to be optimized..
            foreach (Publisher publisher in data.Publishers)
            {
                foreach (EventData evnt in publisher.Events)
                {
                    if (evnt.Name == eventData.Name)
                    {
                        //if the requested event already exist, 
                        //then send it to the appropriate subscriber
                        SendEventDetailsToSubscriber(evnt, endPoint);
                        return;
                    }
                }
            }
            //if the requested event was not found, then wait until it'll be found

        }

        /// <summary>
        /// After finding the event, send it to the subscriber who requested it.
        /// </summary>
        /// <param name="requestedEvnt"></param>
        /// <param name="subscriberEndPoint"></param>
        private void SendEventDetailsToSubscriber(EventData requestedEvnt, EndPoint subscriberEndPoint)
        {
            try
            {
                Object objData = GetSerializedEvent(requestedEvnt);
                byte[] byteData = System.Text.Encoding.UTF8.GetBytes(objData.ToString() + "ENDOFMESSAGE");
                for (int i = 0; i < m_clientCount; i++)
                {
                    if (m_workerSocket[i].RemoteEndPoint == subscriberEndPoint)
                    {
                        if (m_workerSocket[i] != null)
                        {
                            if (m_workerSocket[i].Connected)
                            {
                                m_workerSocket[i].Send(byteData);
                            }
                        }
                    }
                }
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        /// <summary>
        /// Serialize a given EventData Object.
        /// </summary>
        /// <param name="evnt"></param>
        /// <returns></returns>
        private String GetSerializedEvent(EventData evnt)
        {
            StreamWriter stWriter = null;
            XmlSerializer xmlSerializer;
            string buffer;
            try
            {
                xmlSerializer = new XmlSerializer(typeof(EventData));
                MemoryStream memStream = new MemoryStream();
                stWriter = new StreamWriter(memStream);
                System.Xml.Serialization.XmlSerializerNamespaces xs = new XmlSerializerNamespaces();
                xs.Add("", "");
                xmlSerializer.Serialize(stWriter, evnt, xs);
                buffer = Encoding.UTF8.GetString(memStream.GetBuffer());
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                if (stWriter != null)
                    stWriter.Close();
            }
            return buffer;
        }

        //to be removed
		void ButtonSendMsgClick(object sender, System.EventArgs e)
		{
            //try
            //{
            //    Object objData = richTextBoxSendMsg.Text;
            //    byte[] byData = System.Text.Encoding.UTF8.GetBytes(objData.ToString ());
            //    for(int i = 0; i < m_clientCount; i++){
            //        if(m_workerSocket[i] != null){
            //            if(m_workerSocket[i].Connected){
            //                m_workerSocket[i].Send (byData);
            //            }
            //        }
            //    }
				
            //}
            //catch(SocketException se)
            //{
            //    MessageBox.Show (se.Message );
            //}
		}
		
		void ButtonStopListenClick(object sender, System.EventArgs e)
		{
			CloseSockets();			
			UpdateControls(false);
		}
	
       /// <summary>
       /// Get the server's adress.
       /// </summary>
       /// <returns></returns>
	   String GetIP()
	   {	   
	   		String strHostName = Dns.GetHostName();
		
		   	// Find host by name
		   	IPHostEntry iphostentry = Dns.GetHostByName(strHostName);
		
		   	// Grab the first IP addresses
		   	String IPStr = "";
		   	foreach(IPAddress ipaddress in iphostentry.AddressList){
		        IPStr = ipaddress.ToString();
		   		return IPStr;
		   	}
		   	return IPStr;
	   }

	   void ButtonCloseClick(object sender, System.EventArgs e)
	   {
	   		CloseSockets();
	   		Close();
	   }

       /// <summary>
       /// Close sockets.
       /// </summary>
	   void CloseSockets()
	   {
	   		if(m_mainSocket != null){
	   			m_mainSocket.Close();
	   		}
			for(int i = 0; i < m_clientCount; i++){
				if(m_workerSocket[i] != null){
					m_workerSocket[i].Close();
					m_workerSocket[i] = null;
				}
			}	
	   }
	}
}
