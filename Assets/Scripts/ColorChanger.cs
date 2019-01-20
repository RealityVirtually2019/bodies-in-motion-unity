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
        if (Time.time - lastUpdateTime > 0.2f)
        {
                    Utilities.SimilarityScore(modelHandsDown, bodyPoints);
                    Utilities.SimilarityScore(modelHandsForward, bodyPoints);
                    Utilities.SimilarityScore(modelHandsUp, bodyPoints);
     
                    Utilities.SimilarityScore(modelSquat, bodyPoints);
                
  
            lastUpdateTime = Time.time;
        }
    }
}
