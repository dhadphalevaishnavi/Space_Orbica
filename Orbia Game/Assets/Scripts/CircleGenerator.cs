using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleGenerator : MonoBehaviour
{
    public GameObject Circle;
    public GameObject StartingCircle;
    

    public List<GameObject> circleList = new List<GameObject>();

    Vector3 preCirclePosition; //To start next level.............circle5 position 
   
    
    GameObject level;
    GameObject cameraPosition;
    GameObject circle;
    Text lNumber , lNumberHeading;     //level Number
    Transform sCircle;
    public GameObject preCircle;


    float xDifference = 1.5f;
    float minXBound , maxXBound ;
    float yMinDifference , yMaxDifference;
   

    void Awake()
    {
     

        level = GameObject.Find("Level Generation");
        lNumber = GameObject.Find("Level Number").GetComponent<Text>();
        lNumberHeading = GameObject.Find("Level Number Heading").GetComponent <Text>();

        if (level.GetComponent<LevelGenerator>().numberOfOrbits==1)
        {
           
            //yMinDifference = 2;
            yMinDifference = 1.8f;
            yMaxDifference = 6.65f;
        }
        else
        {
/*            yMinDifference = 3.5f;
            yMaxDifference = 6.2f;*/

            yMinDifference = 2.8f;
            yMaxDifference = 6.2f;
        }

        
    }

    private void Update()
    {
        if (level.GetComponent<LevelGenerator>().newLevel == true)
        {
            GenerateCircles();
            lNumberHeading.text = "Level " + lNumber.text;
        }
        lNumber.transform.position = sCircle.position;

  
    }

    private void GenerateCircles()
    {
        cameraPosition = GameObject.Find("Main Camera");
        minXBound = (cameraPosition.transform.position.x + xDifference) * (-1);
        maxXBound = cameraPosition.transform.position.x + xDifference;

        for (int i=0; i<6; i++)
        {
            

            if (i==0)
            {
                circle = GameObject.Instantiate(StartingCircle as GameObject);
                float x=Random.Range(minXBound, maxXBound);


                // float y = -3.65f;
                float y = -2.65f;

                if (preCirclePosition != null)
                {
                     y = preCirclePosition.y + Random.Range(yMinDifference, yMaxDifference);
                }
                circle.transform.position = new Vector3(x,y);

                circle.name = "circle"+i;

                sCircle = circle.transform;

                lNumber.text = (int.Parse(lNumber.text) + 1).ToString();


            }
            else
            {
                circle = GameObject.Instantiate(Circle as GameObject);
                preCircle = GameObject.Find("circle" + (i - 1));

                float x = Random.Range(minXBound, maxXBound);
                float y = preCircle.transform.position.y+Random.Range(yMinDifference, yMaxDifference);
                circle.transform.position = new Vector3(x, y);

                circle.name = "circle"+i;
            }

            if(i==5)
            {
                preCirclePosition = circle.transform.position;
            }

            circleList.Add(circle);
           // print(circleList[i].name);
           

        }

        level.GetComponent<LevelGenerator>().newLevel = false;
        level.GetComponent<LevelGenerator>().GenerateCrystals = true;
        level.GetComponent<LevelGenerator>().GenerateMonsters = true;
        level.GetComponent<LevelGenerator>().startingCircleVisitCount = 0;

        level.GetComponent<LevelGenerator>().countLevelForBoosterPlacement += 1;            //for generating Shield , Chilli , Big Crystal Booster
        if(level.GetComponent<LevelGenerator>().countLevelForBoosterPlacement >3)
        {
            level.GetComponent<LevelGenerator>().countLevelForBoosterPlacement = 1;
        }

    }

}

