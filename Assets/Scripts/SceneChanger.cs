using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    public Transform[] modelHandsUp;
    public Transform fireEffect;

    Light sunlight;
    Transform[] bodyPoints;
    float lastUpdateTime;
    float maxScoreCutoff = 10f;
    // Start is called before the first frame update
    void Start()
    {
        sunlight = GameObject.Find("Sunlight").GetComponent<Light>();
        bodyPoints = GameObject.Find("PoseFollower").GetComponent<AJAXServer>().bodyPoints;
        lastUpdateTime = -100;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastUpdateTime > 1f)
        {
            float score = Utilities.SimilarityScore(modelHandsUp, bodyPoints);
            float fireEffectSize = Mathf.Max(1f - score * 0.7f / maxScoreCutoff, 0.3f);
            fireEffect.localScale = new Vector3(fireEffectSize, fireEffectSize, fireEffectSize);
            float lightIntensity = Mathf.Max(2.4f - score * 2.4f / maxScoreCutoff, 0.01f);
            sunlight.intensity = lightIntensity;
            DynamicGI.UpdateEnvironment();
            lastUpdateTime = Time.time;
        }
    }
}
