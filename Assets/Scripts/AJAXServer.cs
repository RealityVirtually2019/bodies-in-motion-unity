using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Net;
using System.Net.Sockets;
using SocketIOClient;
using SimpleJSON;

public class AJAXServer : MonoBehaviour
{
       

    Client client = new Client("http://localhost:5555");
    
    // Start is called before the first frame update
    void Start()
    {



        client.Opened += SocketOpened;
        client.Message += Message;
        client.SocketConnectionClosed += SocketDisconnect;
        client.Error += SocketError;

        client.Connect();
        
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
        //Debug.Log("Got Stuff: " + e.ToString());
        Debug.Log("Got stuff: " + e.Message.MessageText);
        if (e.Message.MessageText != null)
        {
            JSONNode json = JSON.Parse(e.Message.MessageText);

            JSONArray args1 = json["args"].AsArray;
            JSONArray args = args1[0].AsArray;
            int argsCount = args.Count;
        }
        
        //e.Message.Json.GetFirstArgAs
        //PoseInfoMessage poseInfo = JsonUtility.FromJson<PoseInfoMessage>(e.Message.MessageText);
        //object[] args = (object[])(e.Message.Json.args[0]);
    }

    private void OnDestroy()
    {
        client.Close();
        client.Dispose();
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
