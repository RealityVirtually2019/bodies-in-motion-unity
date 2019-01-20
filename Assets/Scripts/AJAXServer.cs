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
    public Transform[] bodyPoints = new Transform[17];
    private Vector3[] bodyPointPos = new Vector3[17];
    private bool[] pointAvaiable = new bool[17];

    public string ipAddres = "localhost";
    public int port = 5555;

    private Client client;
    
    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < 17; i++)
        {
            if (bodyPoints[i] != null)
            {
                bodyPointPos[i] = bodyPoints[i].localPosition;
                pointAvaiable[i] = true;
            } else
            {
                pointAvaiable[i] = false;
            }
        }

        client = new Client("http://" + ipAddres + ":" + port);

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
        //Debug.Log("Got stuff: " + e.Message.MessageText);
        if (e.Message.MessageText != null)
        {
            JSONNode json = JSON.Parse(e.Message.MessageText);

            JSONArray args1 = json["args"].AsArray;
            JSONArray args = args1[0].AsArray;
            int argsCount = args.Count;

            for (int i = 0; i < 17; i++)
            {
                if (pointAvaiable[i])
                {
                    JSONObject poseObj = args[i].AsObject;
                    JSONObject pos = poseObj["position"].AsObject;

                    bodyPointPos[i].x = pos["x"].AsFloat;
                    bodyPointPos[i].y = pos["y"].AsFloat;
                }
                
            }
        }
        
    }

    private void OnDestroy()
    {
        client.Close();
        client.Dispose();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 17; i++)
        {
            if (pointAvaiable[i])
            {
                bodyPoints[i].localPosition = bodyPointPos[i];
            }
        }
    }
}
