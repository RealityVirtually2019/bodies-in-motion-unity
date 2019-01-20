using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public Transform[] modelHandsUp;

    public float lastUpdateTime { get; private set; }

    Transform[] bodyPoints;

    // Start is called before the first frame update
    void Start()
    {
        bodyPoints = GameObject.Find("PoseFollower").GetComponent<AJAXServer>().bodyPoints;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastUpdateTime > 0.2f)
        {
            float score = Utilities.SimilarityScore(modelHandsUp, bodyPoints);
            lastUpdateTime = Time.time;
        }
}
}
