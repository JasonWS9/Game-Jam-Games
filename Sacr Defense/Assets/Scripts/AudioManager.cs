using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    public AudioClip placeSound;
    public AudioClip explodeSound;
    public AudioClip loseSound;
    public AudioClip enemyBottomSound;

    private AudioSource audioSource;

    void Start()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAudioClip(AudioClip clip)
    {
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(clip);
        audioSource.pitch = 1;
    }
}
