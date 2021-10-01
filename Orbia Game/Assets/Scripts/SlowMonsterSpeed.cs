using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlowMonsterSpeed : MonoBehaviour
{
    float originalMonsterSpeed;
    GameObject ui;

    // Start is called before the first frame update
    void Start()
    {
        originalMonsterSpeed = GetComponent<MonsterRotation1>().degreesPerSecond;
        ui = GameObject.Find("UI Manager");
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name != "Starting Scene")
        {
            if (ui.GetComponent<UIManage>().slowMonster == true && ui.GetComponent<UIManage>().are10SecondsOver == false)
            {
                slowDownMonsterSpeed();
            }

            if (ui.GetComponent<UIManage>().slowMonster == false)
            {
                setMonsterSpeedToNormal();
            }
        }
    }

    public void slowDownMonsterSpeed()
    {
        GetComponent<MonsterRotation1>().degreesPerSecond = originalMonsterSpeed / 2;
       
    }

    public void setMonsterSpeedToNormal()
    {
        GetComponent<MonsterRotation1>().degreesPerSecond = originalMonsterSpeed;
    }
}
