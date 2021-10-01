using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManage : MonoBehaviour
{
    public Text crystalTotal;
    public Text shieldText;
    public Text chillyText;
    public Text shieldAmount;
    public Text chillyAmount;
    public Text slowAmount;
    public Text slowText;

    public Text scoreText;
    public Text highScoreText;
    public Text originalScoreText;

    public Sprite soundOn;
    public Sprite soundOff;
    public Sprite musicOff;
    public Sprite musicOn;

    public Button soundButton;
    public Button musicButton;

 
    public GameObject musicManager;

    [SerializeField] private FlashScreen greenFlashImage;
    public FlashScreen redFlashImage;

    public GameObject pauseMenu;
    [SerializeField] GameObject homeMenu;
    [SerializeField] GameObject restartMenu;
    [SerializeField] GameObject rewardMenu;
    [SerializeField] GameObject getCrystalMenu;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject noAddPanel;

    public GameObject buyButton;
    public GameObject yesAddButton;

    public GameObject restartBtn;
    public GameObject homeBtn;
    public GameObject addCrystalButton;


    public bool getFreeCrystals;


    [SerializeField]AddManagement addRef;

    public bool slowMonster, are10SecondsOver = true , resumed;
    bool areChilly10SecondsOver=true;

    float originalSpeed;
    bool suffeciantAmount;
 

    string previousScene;

 
    private void Awake()
    {
        pauseMenu.SetActive(false);
        homeMenu.SetActive(false);
        restartMenu.SetActive(false);
        getCrystalMenu.SetActive(false);
        noAddPanel.SetActive(false);

        if (PlayerPrefs.GetInt("Daily Reward Claimed") == 0)
        {
            rewardMenu.SetActive(true);
      
            Time.timeScale = 0;
        }

    }

    public void rewardCollected()
    {
        //increse Crystals function

        playClickSound();

        int numberOfCrystals = 500;
        int crystal = int.Parse(crystalTotal.text) + numberOfCrystals ;

        rewardMenu.SetActive(false);
        Time.timeScale = 1;

        PlayerPrefs.SetInt("Daily Reward Claimed", 1);

        PlayerPrefs.SetString("Last Refreshed Date", System.DateTime.Now.Date.ToString());
        
        PlayerPrefs.SetInt("crystalTotal", crystal);
        crystalTotal.text = PlayerPrefs.GetInt("crystalTotal").ToString();
        
    }

    public void increseShields()
    {
      
        int amount = int.Parse(shieldAmount.text);
        decreseCrystals(amount);

        if(suffeciantAmount == true)
        {
            shieldText.text = (int.Parse(shieldText.text) + 1).ToString();
        }
    }




    public void increseChillys()
    {
       

        if (areChilly10SecondsOver == true)
        {
            int amount = int.Parse(chillyAmount.text);
            decreseCrystals(amount);

            if (suffeciantAmount == true)
            {
                

                originalSpeed = GameObject.Find("Level Generation").GetComponent<LevelGenerator>().owlOriginalSpeed;
                if (originalSpeed == 10f)
                {
                    GameObject.Find("Owl").GetComponent<OwlMovement>().speed = originalSpeed * 2;
               
                    areChilly10SecondsOver = false;

                }

               
                redFlashImage.StartFlashLoop(1.25f, 0f, .3f);                                  // START SCREEN FLASHING
                Invoke("setPlayerSpeedToNormal", 10);

            }
        }
    }


    void setPlayerSpeedToNormal()
    {
        GameObject.Find("Owl").GetComponent<OwlMovement>().speed = originalSpeed;
   
        areChilly10SecondsOver = true;

      
           redFlashImage.StopFlashLoop();                                                         //STOP SCREEN FLASHING
    }



    public void decreseCrystals(int amount)                              //decrese Crystals function
    {
        int crystal = int.Parse(crystalTotal.text) ;
        if(crystal >= amount)
        {
            crystal = int.Parse(crystalTotal.text) - amount;
            crystalTotal.text = crystal.ToString();
            suffeciantAmount = true;

            PlayerPrefs.SetInt("crystalTotal", crystal);
           
        }
        else
        {
            suffeciantAmount = false;
        }
       
    }


    public void slowMonsterSpeed()
    {
      

        if (are10SecondsOver == true)
        {
            int amount = int.Parse(slowAmount.text);
            decreseCrystals(amount);


            if (suffeciantAmount == true)
            {
              
               
                slowMonster = true;
                are10SecondsOver = false;
                greenFlashImage.StartFlashLoop(1.25f, 0f, .3f);                                  // START SCREEN FLASHING
                Invoke("setNormalMonsterSpeed", 10f);

                
            }
        }
    }

    void setNormalMonsterSpeed()
    {
        slowMonster = false;
   
        are10SecondsOver = true;

        greenFlashImage.StopFlashLoop();                                                       //STOP SCREEN FLASHING
    }

    public void clickOnOptions()
    {
        playPageChangeSound();
        previousScene = SceneManager.GetActiveScene().name;
 
        SceneManager.LoadScene("Options Scene");

    }

    public void showGetCrystalPanel()
    {
        addRef.checkIfAddAvailable();
 
        if(addRef.addUnavailable == false && PlayerPrefs.GetInt("Remaining Crystal Rewards") >0)                          // ADD AVAILABLE  and daily rewards are remaining
        {
            getCrystalMenu.SetActive(true);                                                                              // SHOW ADD CRYSTAL PANEL
            pauseMenu.SetActive(false);
            noAddPanel.SetActive(false);
        }
        else
        {
            noAddPanel.SetActive(true);
        }
        

        if (musicButton.image.sprite == musicOn)
            MusicManager.bgSound.Audio.Pause();

        Time.timeScale = 0;
    }

    public void addCrystals()
    {
        if (PlayerPrefs.GetInt("Remaining Crystal Rewards") > 0)
        {
            getFreeCrystals = true;
            addRef.showAdd();
      
        }
        else
        {
            noAddPanel.SetActive(true);
            getCrystalMenu.SetActive(false);
         
        }
        
    }

 
    public  void backFromAddPanel()
    {

        noAddPanel.SetActive(false);
        Time.timeScale = 1;
        if (musicButton.image.sprite == musicOn)
            MusicManager.bgSound.Audio.Play();

    }

    public void clickOnBuyButton()
    {
 

        decreseCrystals(1000);
        if (suffeciantAmount == true)
        {
            
            Resume();
        }
        else
        {
            pauseMenu.SetActive(false);
            noAddPanel.SetActive(false);
            showGameOverPanel();
        }

    }

    public void shieldAdReward()
    {
         
         if (PlayerPrefs.GetInt("Remaining shield Rewards") > 0   )
        {
            addRef.showAdd();
        }

    }
   
    public void showGameOverPanel()
    {
        GameObject.Find("Owl").GetComponent<TrailRenderer>().enabled = false;

        gameOverPanel.SetActive(true);

        scoreText.text = "Score : " + originalScoreText.text;
        highScoreText.text = "Highscore : " + PlayerPrefs.GetInt("highScore").ToString();
    }

    public void clickOnPlay()
    {
        playPlaySound();
        SceneManager.LoadScene("Orbia Game Scenes");
    }
 
    public void clickOnBack()
    {
        playPageChangeSound();
        if (SceneManager.GetActiveScene().name == "Credit Scene")
        {
            SceneManager.LoadScene("Options Scene");
        }
        else
        {
 
            SceneManager.LoadScene(previousScene);
        }
    }


    public void clickOnCredit()
    {
        
        playPageChangeSound();
        SceneManager.LoadScene("Credit Scene");
    }



    public void clickOnSound()
    {
        playClickSound();

        if (soundButton.image.sprite == soundOn)
        {
            soundButton.image.sprite = soundOff;
            SoundManager.soundInstance.soundOn = false;


        }
        else
        {
            soundButton.image.sprite = soundOn;
            SoundManager.soundInstance.soundOn = true;
        }

      
        SceneManager.LoadScene("Options Scene");
    }

    public void clickOnMusic()
    {
        playClickSound();
        if (musicButton.image.sprite == musicOn)
        {
            musicButton.image.sprite = musicOff;
            MusicManager.bgSound.Audio.Pause();

        }
        else
        {
            musicButton.image.sprite = musicOn;
            MusicManager.bgSound.Audio.Play();

        }

        
        SceneManager.LoadScene("Options Scene");
    }

    public void playClickSound()
    {
        if(SoundManager.soundInstance.soundOn == true )
            SoundManager.soundInstance.Audio.PlayOneShot(SoundManager.soundInstance.Click);
    }

    public void playPageChangeSound()
    {
        if (SoundManager.soundInstance.soundOn == true)
            SoundManager.soundInstance.Audio.PlayOneShot(SoundManager.soundInstance.pageChange);
    }

    public void playPlaySound()
    {
        if (SoundManager.soundInstance.soundOn == true)
            SoundManager.soundInstance.Audio.PlayOneShot(SoundManager.soundInstance.Play);
    }

    ////////////////////////////////////////////         PAUSE SCREEN              \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

 
    public void clickOnHomeButton()
    {
        homeMenu.SetActive(true);
    }

    public void clickOnRestartButton()
    {
        restartMenu.SetActive(true);
    }

    public void Pause()
    {
        playClickSound();
        pauseMenu.SetActive(true);

        restartBtn.SetActive(false);
        homeBtn.SetActive(false);
        addCrystalButton.SetActive(false);


        addRef.checkIfAddAvailable();

        if(addRef.addUnavailable == false && PlayerPrefs.GetInt("Remaining shield Rewards") > 0)
        {
          
            buyButton.SetActive(false);
            yesAddButton.SetActive(true);
        }

        else if (PlayerPrefs.GetInt("Remaining shield Rewards") == 0 && addRef.addUnavailable == true && int.Parse(crystalTotal.text)>= 1000)
        {
         
            buyButton.SetActive(true);
            yesAddButton.SetActive(false);
        }

        else if(addRef.addUnavailable == true && int.Parse(crystalTotal.text) >= 1000)
        {
           
            buyButton.SetActive(true);
            yesAddButton.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("Remaining shield Rewards") == 0 && addRef.addUnavailable == false && int.Parse(crystalTotal.text) >= 1000)
        {
          
            buyButton.SetActive(true);
            yesAddButton.SetActive(false);
        }

        else if (int.Parse(crystalTotal.text) < 1000 && addRef.addUnavailable == true)
        {
            showGameOverPanel();
        }

        else if (int.Parse(crystalTotal.text) < 1000 && addRef.addUnavailable == false && PlayerPrefs.GetInt("Remaining shield Rewards") == 0)
        {
            showGameOverPanel();
        }

 
        if (musicButton.image.sprite == musicOn)
            MusicManager.bgSound.Audio.Pause();

        Time.timeScale = 0;
    }

    public void Resume()
    {
        playClickSound();
        pauseMenu.SetActive(false);
        homeMenu.SetActive(false);
        restartMenu.SetActive(false);
        getCrystalMenu.SetActive(false);
        noAddPanel.SetActive(false);

        restartBtn.SetActive(true);
        homeBtn.SetActive(true);
        addCrystalButton.SetActive(true);


        Time.timeScale = 1;

        if (musicButton.image.sprite == musicOn)
            MusicManager.bgSound.Audio.Play();

        resumed = true;
    }

    public void gotoHome()
    {
        playClickSound();
        Time.timeScale = 1;
        gameOverPanel.SetActive(false);

        if (musicButton.image.sprite == musicOn)
            MusicManager.bgSound.Audio.Play();
        SceneManager.LoadScene("Starting Scene");
    }

    public void restart()
    {
        playPlaySound();
        Time.timeScale = 1;
        restartMenu.SetActive(false);
        gameOverPanel.SetActive(false);

        GameObject.Find("Owl").GetComponent<TrailRenderer>().enabled = true;

        if (musicButton.image.sprite == musicOn)
            MusicManager.bgSound.Audio.Play();
        SceneManager.LoadScene("Orbia Game Scenes");
    }
   
   

}
