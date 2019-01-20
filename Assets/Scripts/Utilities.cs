﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities
{
    public static float SimilarityScore(Transform[] obj1, Transform[] obj2)
    {
        if (obj1.Length != obj2.Length)
        {
            Debug.Log("ERROR! Different GameObject array lengths are passed to similarityScore()!");
            return Mathf.Infinity;
        }
        float score = 0;
        for(int i = 0; i < obj1.Length; i++)
        {
            if (obj1[i] != null && obj2[i] != null)
            {
                Vector3 dif = obj1[i].position - obj2[i].position;
                float delta = Mathf.Sqrt(dif[0] * dif[0] + dif[1] * dif[1] + dif[2] * dif[2]);
                score += delta;

                float deltaMin = 0.2f;
                float deltaRange = 1f;
                if (i == 0)
                {
                    Debug.Log(delta);
                }
                delta = Mathf.Max(delta - deltaMin, 0);
                delta = Mathf.Min(delta, deltaRange);
                float R = 0.89f / deltaRange * delta;
                float G = 0.27f / deltaRange * delta;
                float B = 1 - 0.47f / deltaRange * delta;
                obj1[i].GetComponent<Renderer>().material.color = new Color(R, G, B);
            }
        }
        return score;
    }
}
