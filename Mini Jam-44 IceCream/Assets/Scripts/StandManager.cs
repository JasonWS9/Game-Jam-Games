using NUnit.Framework;
using UnityEngine;

public class StandManager : MonoBehaviour
{

    [SerializeField] private ParticleSystem particles;

    private bool isBroken = false;

    [SerializeField] private float minTimeToBreak = 20f;
    [SerializeField] private float maxTimeToBreak = 60f;


    private float nextBreakTime;

    private float timeBroken;
    private float customerAnger;

    [SerializeField] private float damageInterval = 30f;

    [SerializeField] private AudioClip breakAudio;
    [SerializeField] private AudioClip fixAudio;

    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        audioSource = GetComponent<AudioSource>();
        particles.Stop();

        SetNextBreakTime();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextBreakTime && isBroken == false)
        {
            BreakMachine();
        }

        if (isBroken)
        {
            timeBroken += Time.deltaTime;
        } if (timeBroken > damageInterval)
        {
            GameManager.instance.AddCustomerAnger(1);
            timeBroken = 0;
        }
    }


    private void SetNextBreakTime()
    {
        nextBreakTime = Time.time + (Random.Range(minTimeToBreak, maxTimeToBreak) * GameManager.instance.timeToBreakModifier);
    }

    private void BreakMachine()
    {
        isBroken = true;
        var sizeOverLifetime = particles.sizeOverLifetime;
        var main = particles.main;

        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(breakAudio, 1.5f);
        audioSource.pitch = 1f;

        particles.Stop();
        particles.Play();

        sizeOverLifetime.enabled = false;
        //Debug.Log("Machine Broken");

        GameManager.instance.UpdateBrokenMachineCount(1);

    }


    public void FixMachine()
    {
        if (isBroken)
        {
            //Debug.Log("Machine Fixed");
            SetNextBreakTime();
            particles.Stop();

            audioSource.PlayOneShot(fixAudio, 5);
            audioSource.PlayOneShot(fixAudio, 5);


            GameManager.instance.UpdateBrokenMachineCount(-1);
            GameManager.instance.fixedMachinesCount++;

            // Enable Size over Lifetime and make particles shrink to 0
            var sizeOverLifetime = particles.sizeOverLifetime;
            sizeOverLifetime.enabled = true;


        } else
        {
            //Debug.Log("Machine isnt Broken");
        }
        isBroken = false;
    }
}
