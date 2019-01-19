using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Net;
using System.Net.Sockets;
using SocketIOClient;

public class AJAXServer : MonoBehaviour
{
    //Socket test = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

    Client client = new Client("http://localhost:5555");
    
    // Start is called before the first frame update
    void Start()
    {

        client.Opened += SocketOpened;
        client.Message += Message;
        client.SocketConnectionClosed += SocketDisconnect;
        client.Error += SocketError;

        client.Connect();
       
        /* Debug.Log("Establishing Connection to thing");
        test.Connect("localhost", 5555);
        Debug.Log("Connection established");*/
        
    }

    void SocketError(object sender, System.EventArgs e)
    {
        Debug.Log("Error: " + e.ToString());
    }

    void SocketDisconnect(object sender, System.EventArgs e)
    {
        Debug.Log("Disconnected... :-(");
    }

    void SocketOpened(object sender, System.EventArgs e)
    {
        Debug.Log("Connected");

    }

    void Message(object sender, MessageEventArgs e)
    {
        Debug.Log("Got stuff: " + e.Message.RawMessage);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (test.Available > 0)
        {
            byte[] buffer = new byte[test.Available];

            test.Receive(buffer);

            Debug.Log(buffer);
        }
        */
    }
}
