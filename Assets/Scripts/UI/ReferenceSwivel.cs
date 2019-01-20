using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceSwivel : MonoBehaviour
{
    public Transform camera;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 rot = camera.rotation.eulerAngles;
        rot.x = 0;
        rot.z = 0;
        Quaternion quatRot = new Quaternion();
        quatRot.eulerAngles = rot;
        transform.rotation = quatRot;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rot = camera.rotation.eulerAngles;
        rot.x = 0;
        rot.z = 0;
        Quaternion quatRot = new Quaternion();
        quatRot.eulerAngles = rot;

        Quaternion currentRot = transform.rotation;

        Quaternion newRot = Quaternion.RotateTowards(currentRot, quatRot, .5f);
        transform.rotation = newRot;
    }
}
