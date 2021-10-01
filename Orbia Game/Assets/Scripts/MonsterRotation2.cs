using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRotation2 : MonoBehaviour
{
    GameObject rotationCenter;

    public float degreesPerSecond;
    private Vector3 v;

    void Start()
    {


        rotationCenter = GameObject.Find("circle0");
        v = transform.position - rotationCenter.transform.position;
    }

    void Update()
    {
        v = Quaternion.AngleAxis(degreesPerSecond * Time.deltaTime, Vector3.forward) * v;
        transform.position = rotationCenter.transform.position + v;
    }
}
