using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {
    DanceMoves danceMoves;
    float[,] danceMove;
    GameObject[] bodyParts;
    int bodyPartsFrameCounter;
    float thisIterationStartTime;

	// Use this for initialization
	void Start () {
        danceMoves = new DanceMoves();
        danceMove = new float[danceMoves.move1.Length/40, 40];
        for (int i = 0; i < danceMove.Length; i++)
        {
            danceMove[i / 40, i % 40] = (float)danceMoves.move1[i];
          //  if (i % 40 != 0)
            //    danceMove[i / 40, i % 40] *= 10;
        }
        bodyParts = GameObject.Find("Main").GetComponent<Parameters>().bodyParts;
        bodyPartsFrameCounter = bodyParts.Length;
	}
	
	// Update is called once per frame
	void Update () {
        if (bodyPartsFrameCounter >= danceMove.GetLength(0))
        {
            Debug.Log("Restart");
            for(int i = 0; i < bodyParts.Length; i++)
            {
                Vector3 position = new Vector3(danceMove[0, i * 3 + 1], danceMove[0, i * 3 + 2], danceMove[0, i * 3 + 3]);
                bodyParts[i].transform.localPosition = position;
            }
            bodyPartsFrameCounter = 1;
            thisIterationStartTime = Time.time;
        }
        for (int i = 0; i < bodyParts.Length; i++)
        {
            Vector3 velocity = new Vector3(danceMove[bodyPartsFrameCounter, i * 3 + 1], danceMove[bodyPartsFrameCounter, i * 3 + 2], danceMove[bodyPartsFrameCounter, i * 3 + 3]);
            bodyParts[i].transform.localPosition += velocity * Time.deltaTime * 3;
        }
        if (Time.time - thisIterationStartTime > danceMove[bodyPartsFrameCounter, 0])
            bodyPartsFrameCounter++;
    }

    private void transformBoneBetweenPoints(GameObject bone, Vector3 p1, Vector3 p2)
    {

    }
}
