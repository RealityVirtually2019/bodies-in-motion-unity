using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseSelector : MonoBehaviour
{
    public GameObject[] poseThings;
    public float poseChangeInteral = 10f;

    // Start is called before the first frame update
    void Start()
    {
        if (poseThings.Length > 0)
        {
            for (int i = 0; i < poseThings.Length; i++)
            {
                poseThings[i].SetActive(false);
            }

            poseThings[0].SetActive(true);
        }
    }

    float lastCallTime = -10;
    int ctr = 0;
    private void Update()
    {
        if (Time.time - lastCallTime > poseChangeInteral)
        {
            SetPose(ctr);
            ctr++;
            ctr = ctr % 4;
            lastCallTime = Time.time;
        }
    }

    public void SetPose(int poseNum)
    {
        if (poseNum <= poseThings.Length - 1)
        {
            for (int i = 0; i < poseThings.Length; i++)
            {
                // poseThings[i].SetActive(false);
                if (i != poseNum)
                    StartCoroutine(unselectPoseThing(i, 3));
            }

            // poseThings[poseNum].SetActive(true);
            StartCoroutine(selectPoseThing(poseNum, 3));
        }
    }

    private IEnumerator selectPoseThing(int poseNum, float v)
    {
        poseThings[poseNum].SetActive(true);
        Vector3 pos = poseThings[poseNum].transform.localPosition;
        pos.y = -3f;
        poseThings[poseNum].transform.localPosition = pos;
        float step = (v / 5.88f) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f)
        {
            t += step;
            pos.y = pos.y + v * Time.deltaTime;
            poseThings[poseNum].transform.localPosition = pos;
            yield return new WaitForFixedUpdate();
        }
        pos.y = 2.88f;
        poseThings[poseNum].transform.localPosition = pos;
    }

    private IEnumerator unselectPoseThing(int poseNum, float v)
    {
        Vector3 pos = poseThings[poseNum].transform.localPosition;
        pos.y = 2.88f;
        poseThings[poseNum].transform.localPosition = pos;
        float step = (v / 5.88f) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f)
        {
            t += step;
            pos.y = pos.y - v * Time.deltaTime;
            poseThings[poseNum].transform.localPosition = pos;
            yield return new WaitForFixedUpdate();
        }
        pos.y = -3f;
        poseThings[poseNum].transform.localPosition = pos;
        poseThings[poseNum].SetActive(false);
    }
}
