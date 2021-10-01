using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlGenerator : MonoBehaviour
{
    [SerializeField] private GameObject OwlPlayer;
    GameObject owl;

    private void Start()
    {
       
        owl= (GameObject)Instantiate(OwlPlayer, Vector3.zero, Quaternion.identity);
        owl.transform.position = new Vector3(0.004f, -6f);
        owl.name = "Owl";

    }
}
