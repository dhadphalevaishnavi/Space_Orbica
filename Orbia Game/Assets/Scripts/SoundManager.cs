using UnityEngine.UI;
using UnityEngine;

public class SoundManager:MonoBehaviour 
{
    public AudioSource Audio;

    public AudioClip Click;
    public AudioClip pageChange;
    public AudioClip Play;
    public AudioClip Collision;
    public AudioClip TaskComplete;
    public AudioClip Reward;
    public AudioClip SmallCircle;

    public Sprite soundOffSprite;
    public Button soundButtonReference;

    public bool soundOn = true;

    public static SoundManager soundInstance;

    private void Awake()
    {
        if (soundInstance != null && soundInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        soundInstance = this;
        DontDestroyOnLoad(this);

    }

    private void Start()
    {
        if ( soundButtonReference.image.sprite == soundOffSprite)
            soundOn = false;

    }
}
