using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageRewardTime : MonoBehaviour
{
 

    private void Update()
    {
        System.DateTime currentDateTime = System.DateTime.Now;
  
        if (PlayerPrefs.HasKey("Last Refreshed Date") == false)                                      // save last activation date
        {
            PlayerPrefs.SetString("Last Refreshed Date", currentDateTime.Date.ToString());
        }


        System.DateTime savedDateTime;
        savedDateTime = System.DateTime.Parse(PlayerPrefs.GetString("Last Refreshed Date"));

        int diffInHours = (currentDateTime - savedDateTime).Hours;                                  

        bool refreshRewards=false ;

 
        if(diffInHours >=16)
        {
            PlayerPrefs.SetInt("Reward Refreshed", 1);
        }


        if (PlayerPrefs.HasKey("Reward Refreshed") == false)
        {
            PlayerPrefs.SetInt("Reward Refreshed", 1);
        }


        if (PlayerPrefs.GetInt("Reward Refreshed") == 1)
        {
            refreshRewards = true;
            PlayerPrefs.SetInt("Reward Refreshed", 0);
        }

        if ( PlayerPrefs.HasKey("Remaining Crystal Rewards") == false || refreshRewards == true)            // FOR FIRST TIME PLAYING 
        {
            PlayerPrefs.SetInt("Remaining Crystal Rewards", 1);                                             //INITIALIZE 
          
        }

        if (PlayerPrefs.HasKey("Remaining shield Rewards") == false || refreshRewards == true)                              
        {
            PlayerPrefs.SetInt("Remaining shield Rewards", 1);
            
        }

        if ( PlayerPrefs.HasKey("Daily Reward Claimed") == false || refreshRewards == true)
        {
            PlayerPrefs.SetInt("Daily Reward Claimed", 0);
           
        }


        ////////////////////////                   WHEN NEW DAY STARTS RESET REWARD PREFS        int diffInHours = (currentDateTime - savedDateTime).Hours;   \\\\\\\\\\\\\\\\\\\\\\\\\\



    }

}
