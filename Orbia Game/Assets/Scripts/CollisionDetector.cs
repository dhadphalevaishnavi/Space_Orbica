using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CollisionDetector : MonoBehaviour
{
    public ParticleSystem particals;

    public Text scoreText;
    public Text crystalText;
    public Text shieldText;
    public Text chillyText;
    public Text highScoreText;

    public bool isCollided=false;
    public bool levelRestarted;

    float originalSpeed;
    public bool collidedToMonster;

    [SerializeField]GameObject addCrystalButton;


     GameObject level;
    GameObject innerMonsters , outerMonsters;
 

    private void Awake()
    {
        scoreText = GameObject.Find("Score").GetComponent<Text>();
    
        crystalText = GameObject.Find("Crystal Total").GetComponent<Text>();

        shieldText = GameObject.Find("Shield Button").GetComponentInChildren<Text>();


       

        level = GameObject.Find("Level Generation");

        innerMonsters = GameObject.Find("Inner Monster Generation");

        outerMonsters = GameObject.Find("Outer Monster Generation");
    }

 
    private void Update()
    {
        if (collidedToMonster == true ||  GameObject.Find("UI Manager").GetComponent<UIManage>().resumed == true)
        {
            GetComponent<CircleCollider2D>().enabled = false;

            transform.position = Vector3.MoveTowards(transform.position, GetComponent<OwlMovement>().currentCircle.transform.position, GetComponent<OwlMovement>().speed * Time.deltaTime);

            if (transform.position == GetComponent<OwlMovement>().currentCircle.transform.position)
            {
                GetComponent<CircleCollider2D>().enabled = true;

                isCollided = false;
                collidedToMonster = false;
                GetComponent<OwlMovement>().move = false;

                if (int.Parse(shieldText.text) > 0)
                {
                    int shieldCount = int.Parse(shieldText.text) - 1;                                      //Decrese shield count by 1
                    shieldText.text = shieldCount.ToString();
                    PlayerPrefs.SetInt("shieldTotal", shieldCount);
                }

                GameObject.Find("UI Manager").GetComponent<UIManage>().resumed = false;                 //for pause panel 
            }
        }

    }

   
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NormalCircleTag"))
        {


            if (collidedToMonster != true)
            {
                playSmallCircleSound();
                increseScore(200);
            }

            for (int i = 0; i < innerMonsters.GetComponent<MonsterGenerator>().monsterPrefabList.Count; i++)            // DisActivate monsters from Inner Orbit revolving around target
            {
                GameObject monsterPrefab = innerMonsters.GetComponent<MonsterGenerator>().monsterPrefabList[i];
                
                if (collision.gameObject.transform.position == monsterPrefab.transform.position)
                {
                    if (monsterPrefab.activeInHierarchy == true)
                    {
                        for (int j = 0; j < monsterPrefab.transform.childCount; j++)                                     //EXPLOAD EFFECT
                        {
                            Transform childMonster = monsterPrefab.transform.GetChild(j);
                            Instantiate(particals, childMonster.transform.position, Quaternion.identity);
                        }
                        monsterPrefab.SetActive(false);
                        break;
                    }
                }


            }

            for (int i = 0; i < outerMonsters.GetComponent<OuterMonsterGenerator>().outerMonsterPrefabList.Count; i++)            // DisActivate monsters from Outer Orbit revolving around target
            {
                GameObject monsterPrefab = outerMonsters.GetComponent<OuterMonsterGenerator>().outerMonsterPrefabList[i];

                if (collision.gameObject.transform.position == monsterPrefab.transform.position)
                {
                    if (monsterPrefab.activeInHierarchy == true)
                    {
                        for (int j = 0; j < monsterPrefab.transform.childCount; j++)                                     //EXPLOAD EFFECT
                        {
                            Transform childMonster = monsterPrefab.transform.GetChild(j);
                            Instantiate(particals, childMonster.transform.position, Quaternion.identity);
                        }

                        monsterPrefab.SetActive(false);
                        break;
                    }
                }


            }

        }

            if (collision.gameObject.CompareTag("StartingCircleTag"))                    //When Reached milestone at Starting Circle give Rewards
        {
            level.GetComponent<LevelGenerator>().numberOfOrbits = Random.Range(1, 3);               // Chooes number of orbits
          

            int visits = level.GetComponent<LevelGenerator>().startingCircleVisitCount += 1;

            if(visits==1 && scoreText.text != "0")                      // if(scoreText.text != "0")
            {
                increseCrystals(500);
                increseScore(2500);

               int shieldCount = int.Parse(shieldText.text) + 1;
               shieldText.text = shieldCount.ToString();
               playTaskCompleteSound();

            }
        }


        if(collision.gameObject.CompareTag("MonsterTag"))
        {
         

            isCollided = true;
            playCollisionSound();
                                                 
            if (shieldText.text != "0")                                  //Shields are available......Reset Player Position to Previous Circle
            {
                collidedToMonster = true;
                
            }

            else                                                           //No Shields Available.......RESTART GAME
            {
                levelRestarted = true;
                Text lNumber = GameObject.Find("Level Number").GetComponent<Text>();
                lNumber.text = ("1");

                GameObject.Find("Add Manager").GetComponent<AddManagement>().checkIfAddAvailable();

                if ((int.Parse(crystalText.text) < 1000 && PlayerPrefs.GetInt("Remaining shield Rewards") == 0 && GameObject.Find("Add Manager").GetComponent<AddManagement>().addUnavailable == true) )
                {
                    GameObject.Find("UI Manager").GetComponent<UIManage>().showGameOverPanel();
                }
                
                else 
                {
                    GameObject.Find("UI Manager").GetComponent<UIManage>().playPageChangeSound();                   // SHOW PAUSE PANEL
                    GameObject.Find("UI Manager").GetComponent<UIManage>().Pause();
                }

            

                playPlaySound();
            }
           
        }

        if (collision.gameObject.CompareTag("BigCrystalTag"))
        {
            //increse crystal count by 250
            //increse Score By 500
            increseScore(500);

            increseCrystals(250);
            playRewardSound();

        }

        if (collision.gameObject.CompareTag("SmallCrystalTag"))
        {
            //increse crystal count by 25
            //increse Score By 200

            increseScore(200);

            increseCrystals(25);
            playRewardSound();

        }

        if (collision.gameObject.CompareTag("ShieldTag"))
        {
            //increse Shield Booster count
            //increse Score By 500

            int shieldCount = int.Parse(shieldText.text) + 1;
            shieldText.text = shieldCount.ToString();

            PlayerPrefs.SetInt("shieldTotal" , shieldCount);

            increseScore(200);
            playRewardSound();

        }

        if (collision.gameObject.CompareTag("ChilliTag"))
        {
            //increse Player Speed for 10 seconds
            //increse Score By 500

          

            originalSpeed=level.GetComponent<LevelGenerator>().owlOriginalSpeed;
            if (originalSpeed == 10f)
            {
                GetComponent<OwlMovement>().speed = originalSpeed*2;
               
            }

            
            GameObject.Find("UI Manager").GetComponent<UIManage>().redFlashImage.StartFlashLoop(1.25f, 0f, .3f);                                             //START SCREEN FLASHING
            Invoke("setPlayerSpeedToNormal", 10);
            increseScore(200);
            playRewardSound();
            

        }


    }

    void resetOwlGravity()
    {
        GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    void setPlayerSpeedToNormal()
    {
        GetComponent<OwlMovement>().speed = level.GetComponent<LevelGenerator>().owlOriginalSpeed;
        GameObject.Find("UI Manager").GetComponent<UIManage>().redFlashImage.StopFlashLoop();                                                                 //STOP SCREEN FLASHING
    }

    void increseScore(int Score)                                    //increse score function
    {
        int score = int.Parse(scoreText.text) + Score;
        scoreText.text = score.ToString();

        if (score > PlayerPrefs.GetInt("highScore"))                            //SET HIGHSCORE
        {
            PlayerPrefs.SetInt("highScore", score);
        
        }
    }

    public void increseCrystals(int numberOfCrystals)                              //increse Crystals function
    {
       
        int crystal = int.Parse(crystalText.text) + numberOfCrystals;

        PlayerPrefs.SetInt("crystalTotal", crystal);
        crystalText.text = PlayerPrefs.GetInt("crystalTotal").ToString();
    }



 
    // PLAY SOUNDS

    public void playCollisionSound()
    {
        if (SoundManager.soundInstance.soundOn == true)
            SoundManager.soundInstance.Audio.PlayOneShot(SoundManager.soundInstance.Collision);
    }

    public void playTaskCompleteSound()
    {
        if (SoundManager.soundInstance.soundOn == true)
            SoundManager.soundInstance.Audio.PlayOneShot(SoundManager.soundInstance.TaskComplete);
    }

    public void playRewardSound()
    {
        if (SoundManager.soundInstance.soundOn == true)
            SoundManager.soundInstance.Audio.PlayOneShot(SoundManager.soundInstance.Reward);
    }

    public void playPlaySound()
    {
        if (SoundManager.soundInstance.soundOn == true)
            SoundManager.soundInstance.Audio.PlayOneShot(SoundManager.soundInstance.Play);
    }

    public void playSmallCircleSound()
    {
        if (SoundManager.soundInstance.soundOn == true)
            SoundManager.soundInstance.Audio.PlayOneShot(SoundManager.soundInstance.SmallCircle);
    }
}
