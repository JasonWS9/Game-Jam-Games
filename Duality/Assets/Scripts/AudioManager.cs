using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioSource musicSource;


    [SerializeField] private AudioClip level1Music;
    [SerializeField] private AudioClip level2Music;


    //Allows other functions to access this one without without having to grab the component (Can only be used if there is only ever one of these in the scene)
    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySfx(AudioClip clip)
    {
        sfxSource.pitch = Random.Range(0.85f, 1.15f);
        sfxSource.PlayOneShot(clip);
    }
}
