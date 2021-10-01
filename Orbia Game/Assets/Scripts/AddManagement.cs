using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class AddManagement : MonoBehaviour, IUnityAdsListener
{

#if UNITY_IOS
    string gameID = "4379886";

#else
    string gameID = "4379887";

#endif

    public UIManage uiRef;
 //   public CollisionDetector collisionScript;

    [SerializeField] Text crystalText;
    [SerializeField] GameObject getCrystalMenu;
    [SerializeField] GameObject noAddPanel;
    [SerializeField] GameObject shieldPanel;

    public bool addUnavailable ;


    bool resumeGame;

    private void Start()
    {
        
        Advertisement.Initialize(gameID );
        Advertisement.AddListener(this);

        crystalText = GameObject.Find("Crystal Total").GetComponent<Text>();

    }

    public void checkIfAddAvailable()
    {
        if (gameID == "4379887")                                                                // REWARD ADD FOR ANDROID
        {
            if (Advertisement.IsReady("Rewarded_Android"))
            {
                addUnavailable = false;
            }
            else
                addUnavailable = true;
        }


        if (gameID == "4379886")                                                                // REWARD ADD FOR IOS
        {
            if (Advertisement.IsReady("Rewarded_iOS"))
            {
                addUnavailable = false;
            }
            else
                addUnavailable = true;

        }
    }

    public void showAdd()
    {
       

        if (gameID == "4379887")                                                                // REWARD ADD FOR ANDROID
        {
            if (Advertisement.IsReady("Rewarded_Android"))
            {

                Advertisement.Show("Rewarded_Android");
                resumeGame = true;
                addUnavailable = false;
            }
            else
            {
                Debug.Log("ADD NOT READY.... try again later");
                addUnavailable = true;
            }
        }


        if (gameID == "4379886")                                                                // REWARD ADD FOR IOS
        {
            if (Advertisement.IsReady("Rewarded_iOS"))
            {
                Advertisement.Show("Rewarded_iOS");
                addUnavailable = false;
                resumeGame = true;
            }
            else
            {
                Debug.Log("ADD NOT READY.... try again later");
                addUnavailable = true;
            }
        }

        if(addUnavailable == true)
        {
            shieldPanel.SetActive(false);
            getCrystalMenu.SetActive(false);
            noAddPanel.SetActive(true);


        }

        if (uiRef.musicButton.image.sprite == uiRef.musicOn)
            MusicManager.bgSound.Audio.Pause();
    }

    public void OnUnityAdsReady(string placementId)
    {
       // throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidError(string message)
    {
       // throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidStart(string placementId)
    {
       // throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // throw new System.NotImplementedException();

        if ((placementId == "Rewarded_Android" || placementId == "Rewarded_iOS") && showResult == ShowResult.Finished && uiRef.getFreeCrystals == true)
        {
           
            getCrystalReward(250);
            uiRef.getFreeCrystals = false;

            if (uiRef.musicButton.image.sprite == uiRef.musicOn)
                MusicManager.bgSound.Audio.Play();

            int reduceOne = PlayerPrefs.GetInt("Remaining Crystal Rewards") - 1;
            PlayerPrefs.SetInt("Remaining Crystal Rewards", reduceOne);

        }

        else if ((placementId == "Rewarded_Android" || placementId == "Rewarded_iOS" ) && showResult == ShowResult.Finished && resumeGame == true )
        {
            
 
                int reduceOne = PlayerPrefs.GetInt("Remaining shield Rewards") - 1;
                PlayerPrefs.SetInt("Remaining shield Rewards", reduceOne);

                if (reduceOne == 0)
                {
                    uiRef.buyButton.SetActive(true);
                    uiRef.yesAddButton.SetActive(false);
                }
                uiRef.Resume();
                resumeGame = false;

        }



    }

    public void getCrystalReward(int numberOfCrystals)                              //increse Crystals function
    {

        int crystal = int.Parse(crystalText.text) + numberOfCrystals;

        PlayerPrefs.SetInt("crystalTotal", crystal);
        crystalText.text = PlayerPrefs.GetInt("crystalTotal").ToString();

        Time.timeScale = 1;
        getCrystalMenu.SetActive(false);                                                //disActivate GET CRYSTAL PANEL
    }
}
