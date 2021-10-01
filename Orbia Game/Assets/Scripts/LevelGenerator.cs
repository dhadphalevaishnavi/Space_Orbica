using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelGenerator : MonoBehaviour
{
    public bool newLevel;
    public float owlOriginalSpeed;
    public int numberOfOrbits;
    public bool levelStarted;
    public bool destroyPreviusCircles;
    public int numberInCircleName;
    public bool GenerateCrystals;
    public bool GenerateMonsters;
    public int countLevelForBoosterPlacement;
    public int countHowManyOrbitsGenerated;
    public int startingCircleVisitCount;                //dont give booster when collided to monster and back to Starting Circle
    public int whichOrbitToDraw;

    public Text crystalText;
    public Text shieldText;
   

    // Start is called before the first frame update
    void Start()
    {
        newLevel = true;
        owlOriginalSpeed = 10f;

        numberOfOrbits =Random.Range(1,3);

        levelStarted = true;
        destroyPreviusCircles = false;
        numberInCircleName = 1;
        countLevelForBoosterPlacement = 1;
        startingCircleVisitCount = 0;
        countHowManyOrbitsGenerated = 0;
      
       
        crystalText.text = PlayerPrefs.GetInt("crystalTotal").ToString();               //SAVED CRYSTALS
        shieldText.text = PlayerPrefs.GetInt("shieldTotal").ToString();               //SAVED SHIELDS

    }

    // Update is called once per frame
    void Update()
    {
        if(newLevel == true && numberOfOrbits == 1)
        {
           
           whichOrbitToDraw = Random.Range(1, 3);                      // 1= draw only inner orbit ,  2= draw only outer orbit
          

        }
    }
}
