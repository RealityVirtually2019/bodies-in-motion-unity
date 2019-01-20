using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public Transform[] modelHandsUp;
    public Transform[] modelHandsDown;
    public Transform[] modelHandsForward;
    public Transform[] modelSquat;

    public float lastUpdateTime { get; private set; }

    Transform[] bodyPoints;

    // Start is called before the first frame update
    void Start()
    {
        bodyPoints = GameObject.Find("PoseFollower").GetComponent<AJAXServer>().bodyPoints;
        lastUpdateTime = -10f;
    }

    // Update is called once per frame
    void Update()
    {
        Transform[] currentModel = null;
        if (Time.time - lastUpdateTime > 0.2f)
        {
            switch (GameObject.Find("Poses").GetComponent<PoseSelector>().currentPose)
            {
                case 0:
                    currentModel = modelHandsDown;
                    break;
                case 1:
                    currentModel = modelHandsForward;
                    break;
                case 2:
                    currentModel = modelHandsUp;
                    break;
                case 3:
                    currentModel = modelSquat;
                    break;
                default:
                    break;
            }
            float score = Utilities.SimilarityScore(currentModel, bodyPoints);
            lastUpdateTime = Time.time;
        }
    }
}
