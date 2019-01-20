using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities
{
    public static float SimilarityScore(GameObject[] obj1, GameObject[] obj2)
    {
        if (obj1.Length != obj2.Length)
        {
            Debug.Log("ERROR! Different GameObject array lengths are passed to similarityScore()!");
            return Mathf.Infinity;
        }
        float score = 0;
        for(int i = 0; i < obj1.Length; i++)
        {
            Vector3 dif = obj1[i].transform.position - obj2[i].transform.position;
            score += Mathf.Sqrt(dif[0] * dif[0] + dif[1] * dif[1] + dif[2] * dif[2]);
        }
        return score;
    }
}
