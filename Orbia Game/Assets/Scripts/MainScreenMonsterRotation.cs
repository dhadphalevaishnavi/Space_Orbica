using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreenMonsterRotation : MonoBehaviour
{
    [SerializeField] private GameObject drawThisMonster;
   
    public GameObject rotationCenter;
    GameObject monster;

    // Start is called before the first frame update
    void Start()
    {
       

        monster = GameObject.Instantiate(drawThisMonster as GameObject);
        monster.transform.position = rotationCenter.transform.position;

        for (int j = 0; j < monster.transform.childCount; ++j)
        {
            Transform currentMonster = monster.transform.GetChild(j);                                           //find Single monster from monster formation
            GameObject monsterChild = currentMonster.gameObject;                                                //find Single monster from monster formation


            monsterChild.GetComponent<MonsterRotation1>().degreesPerSecond = 30;        // set constant speed from monsterRotation1         monsterSpeedList[Random.Range(0, monsterSpeedList.Count)]
            monsterChild.GetComponent<MonsterRotation1>().rotationCenter = monster;                                              // set rotationCenter from monsterRotation1
            monsterChild.GetComponent<MonsterRotation1>().v = monsterChild.transform.position - monster.transform.position;      // set v from monsterRotation1
        }
    }

   
}
