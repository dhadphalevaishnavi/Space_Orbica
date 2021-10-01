using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* 
*   RANDOM ROTATION
*=======================================================================
*
*    public GameObject rotationCenter;

    GameObject nearestCircleToMonster;

    public float degreesPerSecond;
    List<float> chooesSpeed = new List<float>();
    private Vector3 v;

    void Start()
    {
        chooesSpeed.Add(-80);
        chooesSpeed.Add(-180);
        chooesSpeed.Add(-50);

        degreesPerSecond = chooesSpeed[Random.Range(0, chooesSpeed.Count)];

           int i = 1;
           rotationCenter = GameObject.Find("circle"+1);
               


  
        v = transform.position -rotationCenter.transform.position;
    }

    void Update()
    {
        v = Quaternion.AngleAxis(degreesPerSecond * Time.deltaTime, Vector3.forward) * v;
        transform.position = rotationCenter.transform.position + v;
    }

*
*/


public class MonsterRotation1 : MonoBehaviour
{

    public GameObject rotationCenter;

    public float degreesPerSecond;
    public Vector3 v;

 
    void Update()
    {
        if (rotationCenter != null)
        {
           
            v = Quaternion.AngleAxis(degreesPerSecond * Time.deltaTime, Vector3.forward) * v;
            transform.position = rotationCenter.transform.position + v;
        }
    }


}

/*
 * Original Code for Rotation
 * 
     GameObject rotationCenter;
 
    public float degreesPerSecond;
    private Vector3 v;

    void Start()
    {
 

        rotationCenter = GameObject.Find("circle0");
        v = transform.position -rotationCenter.transform.position;
    }

    void Update()
    {
        v = Quaternion.AngleAxis(degreesPerSecond * Time.deltaTime, Vector3.forward) * v;
        transform.position = rotationCenter.transform.position + v;
    }

 */