using UnityEngine.UI;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager bgSound;
    public AudioSource Audio;

    public Sprite musicOffSprite;
    public Button musicButtonReference;

    private void Awake()
    {
        if(bgSound != null && bgSound != this)
        {
            Destroy(this.gameObject);
            return;
        }


        bgSound = this;
        DontDestroyOnLoad(this);
        
    }

    private void Start()
    {
        Audio = GetComponent<AudioSource>();
        if ( musicButtonReference.image.sprite == musicOffSprite)
            Audio.Pause();
    }

    
}
