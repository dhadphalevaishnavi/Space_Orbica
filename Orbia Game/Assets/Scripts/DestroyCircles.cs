using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCircles : MonoBehaviour
{
    GameObject level, cir , crystalDestroy  ;
    GameObject innerMonsters, outerMonsters;

    // Start is called before the first frame update
    void Start()
    {
        level = GameObject.Find("Level Generation");
        cir = GameObject.Find("Circle Generation");
        crystalDestroy = GameObject.Find("Booster Generation");

        innerMonsters = GameObject.Find("Inner Monster Generation");
        outerMonsters = GameObject.Find("Outer Monster Generation");

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if((level.GetComponent<LevelGenerator>().destroyPreviusCircles == true) && (cir.GetComponent<CircleGenerator>().circleList.Count!=0))
        {
            destroyCircle();
            destroyMonsters();
        }
                
    }

    void destroyCircle()
    {
        GameObject c;

        for (int i=0 ; i< 6; i++)
        {
            c= cir.GetComponent<CircleGenerator>().circleList[0];                           //Find circle object in list
            cir.GetComponent<CircleGenerator>().circleList.Remove(c);
            Destroy(c);                                                                     //Destroy circle GameObject

            if (i > 0)
            {
                c = crystalDestroy.GetComponent<BoosterGenerator>().BoosterList[0];             //Find Booster object  in list
                crystalDestroy.GetComponent<BoosterGenerator>().BoosterList.Remove(c);          //Remove from List
                Destroy(c);                                                                     //Destroy Booster GameObject
            }
        }


        GameObject.Find("Level Generation").GetComponent<LevelGenerator>().destroyPreviusCircles = false;
        GameObject.Find("Level Generation").GetComponent<LevelGenerator>().levelStarted = true;      //Start New Level
        GameObject.Find("Level Generation").GetComponent<LevelGenerator>().newLevel = true;      //Start New Level


    }

    void destroyMonsters()
    {
        GameObject g;

        for (int i = 0; i < 5 ; i++)
        {
            if (innerMonsters.GetComponent<MonsterGenerator>().monsterPrefabList.Count != 0)
            {
                g = innerMonsters.GetComponent<MonsterGenerator>().monsterPrefabList[0];
                innerMonsters.GetComponent<MonsterGenerator>().monsterPrefabList.Remove(g);
                Destroy(g);
            }
        }

        for (int i = 0; i < 5 ; i++)
        {
            if (outerMonsters.GetComponent<OuterMonsterGenerator>().outerMonsterPrefabList.Count != 0)
            {
                g = outerMonsters.GetComponent<OuterMonsterGenerator>().outerMonsterPrefabList[0];
                outerMonsters.GetComponent<OuterMonsterGenerator>().outerMonsterPrefabList.Remove(g);
                Destroy(g);
            }
        }
        if (outerMonsters.GetComponent<OuterMonsterGenerator>().outerMonsterPrefabList.Count != 0)
        {
            outerMonsters.GetComponent<OuterMonsterGenerator>().outerMonsterPrefabList.Clear();
        }
    }

}
