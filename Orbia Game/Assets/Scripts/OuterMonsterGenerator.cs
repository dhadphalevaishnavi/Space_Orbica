using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuterMonsterGenerator : MonoBehaviour
{

    [SerializeField] private GameObject[] drawThisMonster;           // monster formation prefab 

    GameObject monster;
    GameObject findCircle;
    GameObject level;

    List<float> monsterSpeedList = new List<float>();

    public List<GameObject> outerMonsterPrefabList = new List<GameObject>();
  
    public float mSpeed ;           //monster Speed
    int[] monsterDirection = { 1, -1 };
    int[] isMonsterSpeedRandom = { 0, 0, 0, 0, 1 };                 //  0 = not Random speed...........  1 = Same speed for all monsters in a single prefab

    void Awake()
    {

        level = GameObject.Find("Level Generation");
        mSpeed = 30;


    }

    private void Update()
    {
        if (level.GetComponent<LevelGenerator>().GenerateMonsters == true && (level.GetComponent<LevelGenerator>().countHowManyOrbitsGenerated < level.GetComponent<LevelGenerator>().numberOfOrbits))
        {
            if ((level.GetComponent<LevelGenerator>().numberOfOrbits == 2) || ((level.GetComponent<LevelGenerator>().numberOfOrbits == 1) && (level.GetComponent<LevelGenerator>().whichOrbitToDraw == 2)))
            {
                if (mSpeed < 300)                             //increse monster speed after every 3 levels         level.GetComponent<LevelGenerator>().countLevelForBoosterPlacement == 2 && 
                {


                    if (mSpeed < 0)
                        mSpeed *= (-1);

                    mSpeed += 5;
                    mSpeed *= (monsterDirection[Random.Range(0, 2)]);

                }

              
                for (int i = 1; i <= 5; i++)
                {
                    findCircle = GameObject.Find("circle" + i);

                    monster = GameObject.Instantiate(drawThisMonster[Random.Range(0, drawThisMonster.Length)] as GameObject);
                    monster.transform.position = findCircle.transform.position;

                    //int isRandomSpeed = Random.Range(0, 2);

                    int isRandomSpeed = isMonsterSpeedRandom[Random.Range(0, isMonsterSpeedRandom.Length)];

                    for (int j = 0; j < monster.transform.childCount; ++j)
                    {
                        Transform currentMonster = monster.transform.GetChild(j);                                           //find Single monster from monster formation
                        GameObject monsterChild = currentMonster.gameObject;                                                //find Single monster from monster formation

                        if (isRandomSpeed == 0)
                        {
                            monsterChild.GetComponent<MonsterRotation1>().degreesPerSecond = mSpeed;        // set constant speed from monsterRotation1
                        }
                        else
                        {
                            monsterSpeedList.Clear();   //clear list of old speeds
                            monsterSpeedList.Add(mSpeed - 5);
                            monsterSpeedList.Add(mSpeed - 10);
                            monsterSpeedList.Add(mSpeed);
                            monsterSpeedList.Add(mSpeed + 5);
                            monsterSpeedList.Add(mSpeed + 10);

                            monsterChild.GetComponent<MonsterRotation1>().degreesPerSecond = monsterSpeedList[Random.Range(0, monsterSpeedList.Count)]; // set Random speed from monsterRotation1
                        }

                        //   monsterChild.GetComponent<MonsterRotation1>().degreesPerSecond = mSpeed;        // set speed from monsterRotation1
                        monsterChild.GetComponent<MonsterRotation1>().rotationCenter = findCircle;                                              // set rotationCenter from monsterRotation1
                        monsterChild.GetComponent<MonsterRotation1>().v = monsterChild.transform.position - findCircle.transform.position;      // set v from monsterRotation1

                    }

                    outerMonsterPrefabList.Add(monster);                                     // List for Monster formation
                
                }
              

                level.GetComponent<LevelGenerator>().countHowManyOrbitsGenerated += 1;
                if (level.GetComponent<LevelGenerator>().countHowManyOrbitsGenerated >= level.GetComponent<LevelGenerator>().numberOfOrbits)
                {
                    level.GetComponent<LevelGenerator>().countHowManyOrbitsGenerated = 0;
                    level.GetComponent<LevelGenerator>().GenerateMonsters = false;
                }

 
            }
        }
   

    }
}
