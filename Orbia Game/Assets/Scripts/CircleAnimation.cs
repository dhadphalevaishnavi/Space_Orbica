using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleAnimation : MonoBehaviour
{
    [SerializeField] private Animator circleAnimationController;
    GameObject player;
    int flag = 0;

    private void Start()
    {
        player = GameObject.Find("Owl");
       
    }

    private void Update()
    {
        if((player!=null)&&(player.transform.position== transform.position) && flag==0)
        {
            setAnimation();
            flag = 1;
            Invoke("setIdle", 1f);
        }

        if(flag==1)
        {
            Invoke("setIdle", 1f);
        }
    }



    void setIdle()
    {
        circleAnimationController.SetBool("isCircleIdle", true);
    }

    void setAnimation()
    {
        circleAnimationController.SetBool("isCircleIdle", false);
    }
}

