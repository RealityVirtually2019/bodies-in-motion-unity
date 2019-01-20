using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Net;
using System.Net.Sockets;
using SocketIOClient;
using SimpleJSON;
//using VolumetricLines;

public class AJAXServer : MonoBehaviour
{
    public string ipAddres = "localhost";
    public int port = 5555;

    public Transform[] bodyPoints = new Transform[17];
    private Vector3[] bodyPointPos = new Vector3[17];
    private bool[] pointAvaiable = new bool[17];

    //public VolumetricLineBehavior[] boneLines = new VolumetricLineBehavior[13];

    public LineRenderer[] boneLines = new LineRenderer[13];

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
        Debug.Log("Node.io Error: " + e.ToString());
    }

    void SocketDisconnect(object sender, System.EventArgs e)
    {
        Debug.Log("Node.io Disconnected... :-(");
    }

    void SocketOpened(object sender, System.EventArgs e)
    {
        Debug.Log("Node.io Connected");

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

        //Head
        if (boneLines[0] != null)
            boneLines[0].SetPositions(new Vector3[] { bodyPoints[0].position, ((bodyPoints[5].position + bodyPoints[6].position) / 2f)});

        //Collar
        if (boneLines[1] != null)
            boneLines[1].SetPositions(new Vector3[] { bodyPoints[5].position, bodyPoints[6].position });

        //RightUpperArm
        if (boneLines[2] != null)
            boneLines[2].SetPositions(new Vector3[] { bodyPoints[6].position, bodyPoints[8].position });

        //LeftUpperArm
        if (boneLines[3] != null)
            boneLines[3].SetPositions(new Vector3[] { bodyPoints[5].position, bodyPoints[7].position });

        //RightLowerArm
        if (boneLines[4] != null)
            boneLines[4].SetPositions(new Vector3[] { bodyPoints[8].position, bodyPoints[10].position });

        //LeftLowerArm
        if (boneLines[5] != null)
            boneLines[5].SetPositions(new Vector3[] { bodyPoints[7].position, bodyPoints[9].position });

        //RightSide
        if (boneLines[6] != null)
            boneLines[6].SetPositions(new Vector3[] { bodyPoints[6].position, bodyPoints[12].position });

        //LeftSide
        if (boneLines[7] != null)
            boneLines[7].SetPositions(new Vector3[] { bodyPoints[5].position, bodyPoints[11].position });

        //Hip
        if (boneLines[8] != null)
            boneLines[8].SetPositions(new Vector3[] { bodyPoints[12].position, bodyPoints[11].position });

        //RightUpperLeg
        if (boneLines[9] != null)
            boneLines[9].SetPositions(new Vector3[] { bodyPoints[12].position, bodyPoints[14].position });

        //LeftUpperLeg
        if (boneLines[10] != null)
            boneLines[10].SetPositions(new Vector3[] { bodyPoints[11].position, bodyPoints[13].position });

        //RightLowerLeg
        if (boneLines[11] != null)
            boneLines[11].SetPositions(new Vector3[] { bodyPoints[14].position, bodyPoints[16].position });

        //LeftLowerLeg
        if (boneLines[12] != null)
            boneLines[12].SetPositions(new Vector3[] { bodyPoints[13].position, bodyPoints[15].position });

        ////Head
        //boneLines[0].StartPos = bodyPoints[0].position;
        //boneLines[0].EndPos = (bodyPoints[5].position + bodyPoints[6].position) / 2f;

        ////Collar
        //boneLines[1].StartPos = bodyPoints[5].position;
        //boneLines[1].EndPos = bodyPoints[6].position;

        ////RightUpperArm
        //boneLines[2].StartPos = bodyPoints[6].position;
        //boneLines[2].EndPos = bodyPoints[8].position;

        ////LeftUpperArm
        //boneLines[3].StartPos = bodyPoints[5].position;
        //boneLines[3].EndPos = bodyPoints[7].position;

        ////RightLowerArm
        //boneLines[4].StartPos = bodyPoints[8].position;
        //boneLines[4].EndPos = bodyPoints[10].position;

        ////LeftLowerArm
        //boneLines[5].StartPos = bodyPoints[7].position;
        //boneLines[5].EndPos = bodyPoints[9].position;

        ////RightSide
        //boneLines[6].StartPos = bodyPoints[6].position;
        //boneLines[6].EndPos = bodyPoints[12].position;

        ////LeftSide
        //boneLines[7].StartPos = bodyPoints[5].position;
        //boneLines[7].EndPos = bodyPoints[11].position;

        ////Hip
        //boneLines[8].StartPos = bodyPoints[12].position;
        //boneLines[8].EndPos = bodyPoints[11].position;

        ////RightUpperLeg
        //boneLines[9].StartPos = bodyPoints[12].position;
        //boneLines[9].EndPos = bodyPoints[14].position;

        ////LeftUpperLeg
        //boneLines[10].StartPos = bodyPoints[11].position;
        //boneLines[10].EndPos = bodyPoints[13].position;

        ////RightLowerLeg
        //boneLines[11].StartPos = bodyPoints[14].position;
        //boneLines[11].EndPos = bodyPoints[16].position;

        ////LeftLowerLeg
        //boneLines[12].StartPos = bodyPoints[13].position;
        //boneLines[12].EndPos = bodyPoints[15].position;
    }
}
