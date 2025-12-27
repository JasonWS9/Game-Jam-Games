using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    [SerializeField] private AudioSource sfxSource;

    public AudioSource musicSource;

    private void Awake()
    {
        if (instance == null)
        {

            instance = this;
            DontDestroyOnLoad(gameObject);

        } else
        {
            Destroy(gameObject);
        }
    }








    public AudioClip jumpSFX;
    public AudioClip loopSFX;
    public AudioClip damageSFX;
    public AudioClip doubleJumpSFX;
    public AudioClip upgradeSFX;
    public AudioClip dashSFX;

    private void Start()
    {
        musicSource = GetComponent<AudioSource>();


        if (!musicSource.isPlaying)
        {
            musicSource.Play();
        } else
        {
            musicSource.Stop();
        }
    }

    public void PlayAudio(AudioClip clip, bool randomizePitch)
    {
        if (randomizePitch)
        {
            sfxSource.pitch = Random.Range(0.9f, 1.1f);
            sfxSource.PlayOneShot(clip);
            sfxSource.pitch = 1f;
        } else
        {
            sfxSource.PlayOneShot(clip);
        }
    }

}
