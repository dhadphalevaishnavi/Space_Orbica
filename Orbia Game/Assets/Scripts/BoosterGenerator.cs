using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoosterGenerator : MonoBehaviour
{
    public GameObject Crystal;
    public GameObject Chilly;
    public GameObject Shield;
    public GameObject BigCrystal;
    GameObject level;
    public List<GameObject> BoosterList = new List<GameObject>();

    // Start is called before the first frame update
    void Awake()
    {
        level = GameObject.Find("Level Generation");

        InvokeRepeating("activateBoosters", 1.5f, 4);
        InvokeRepeating("disactivateBoosters", 0.01f,4);

    }

    // Update is called once per frame
    void Update()
    {

        if (level.GetComponent<LevelGenerator>().GenerateCrystals == true)
            GenerateCrystals();


    }

    void GenerateCrystals()             //Generate Boosters
    {
       
        for(int i=1; i<6; i++)
        {
            GameObject circle = GameObject.Find("circle" + i);
            GameObject crystal;

            if ((level.GetComponent<LevelGenerator>().countLevelForBoosterPlacement == 1) && (i == 1))             // Generate Chilly at 1st circle when Level =1
            {
               
                crystal = Instantiate(Chilly as GameObject);
                crystal.transform.position = circle.transform.position;
                crystal.name = "Chilly Booster" ;

            }

            else if ((level.GetComponent<LevelGenerator>().countLevelForBoosterPlacement == 2) && (i == 3))             // Generate Shield at 3rd circle when Level =2
            {

                crystal = Instantiate(Shield as GameObject);
                crystal.transform.position = circle.transform.position;
                crystal.name = "Shield Booster";

            }

            else if ((level.GetComponent<LevelGenerator>().countLevelForBoosterPlacement == 3) && (i == 4))             // Generate Big Crystal at 4th circle when Level =3
            {

                crystal = Instantiate(BigCrystal as GameObject);
                crystal.transform.position = circle.transform.position;
                crystal.name = "Big Crystal Booster";

            }

            else
            {
                crystal = Instantiate(Crystal as GameObject);
                crystal.transform.position = circle.transform.position;
                crystal.name = "crystal" + i;
            }

            BoosterList.Add(crystal);
        }
        level.GetComponent<LevelGenerator>().GenerateCrystals = false;
    }

    void activateBoosters()
    {
        for(int i=0; i<BoosterList.Count; i++)
        {
            GameObject booster = BoosterList[i];
            if ( (booster.transform.position.y > GameObject.Find("Owl").transform.position.y) )
                booster.gameObject.SetActive(true);
        }
       
    }

    void disactivateBoosters()
    {
        for (int i = 0; i < BoosterList.Count; i++)
        {
            GameObject booster = BoosterList[i];
            booster.gameObject.SetActive(false);
        }
      
    }
}


