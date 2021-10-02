using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonChange : MonoBehaviour
{
    [SerializeField] GameObject musicOnButton;
    [SerializeField] GameObject soundOffButton;
    [SerializeField] GameObject musicOffButton;
    [SerializeField] GameObject soundOnButton;

     public UIManage uiRef;

    void Awake()
    {
       

        if (PlayerPrefs.GetInt("Sound On") == 1)            
            soundOffButton.SetActive(false);
        
        if (PlayerPrefs.GetInt("Sound On") == 0)
            soundOnButton.SetActive(false);


        if (PlayerPrefs.GetInt("Music On") == 1)
            musicOffButton.SetActive(false);

        if (PlayerPrefs.GetInt("Music On") == 0)
            musicOnButton.SetActive(false);
    }

    public void clickOnSound()
    {
        uiRef.playClickSound();

        if(PlayerPrefs.GetInt("Sound On") == 1)
        {
            soundOffButton.SetActive(true);
            soundOnButton.SetActive(false);

            PlayerPrefs.SetInt("Sound On", 0);
            SoundManager.soundInstance.soundOn = false;
        }
        else 
        {
            soundOffButton.SetActive(false);
            soundOnButton.SetActive(true);

            PlayerPrefs.SetInt("Sound On", 1);
            SoundManager.soundInstance.soundOn = true;
        }
    }

    public void clickOnMusic()
    {
        uiRef.playClickSound();

        if (PlayerPrefs.GetInt("Music On") == 1)
        {
            musicOffButton.SetActive(true);
            musicOnButton.SetActive(false);

            PlayerPrefs.SetInt("Music On", 0);
            MusicManager.bgSound.Audio.Pause();
        }
        else
        {
            musicOffButton.SetActive(false);
            musicOnButton.SetActive(true);

            PlayerPrefs.SetInt("Music On", 1);
            MusicManager.bgSound.Audio.Play();
        }
    }


}
