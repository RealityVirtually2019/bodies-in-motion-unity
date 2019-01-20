using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseSelector : MonoBehaviour
{
    public GameObject[] poseThings;

    private int activePose = 0;

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

    public void SetPose(int poseNum)
    {
        if (poseNum <= poseThings.Length-1)
        {
            
            poseThings[activePose].SetActive(false);

            activePose = poseNum;
            poseThings[poseNum].SetActive(true);
        }
    }

    public GameObject GetActivePose()
    {
        return poseThings[activePose];
    }
}
