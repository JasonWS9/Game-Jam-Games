using System;
using System.Collections;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Transform respawner;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    private KeyCode projectileKey = KeyCode.E;
    private bool canFire = true;
    [SerializeField] private float projectileCooldown = 1f;

    [HideInInspector] public PlayerMovement playerMovement;

    [HideInInspector] public int darknessCount;
    public int maxDarkness = 5;
    private int startingDarkness = 0;

    [HideInInspector] public int lightCount;
     public int maxLight = 5;
    private int startingLight = 0;

    private bool canTakeDamage = true;
    [SerializeField] private float takeDamageCooldown = 1f;
    private enum CurrentZone {light, dark}
    private CurrentZone currentZone;

    public AudioClip lightDeathSound;
    public AudioClip darkDeathSound;
    public AudioClip damageSound;
    public AudioClip projectileSound;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();


        startingDarkness = 0;
        darknessCount = startingDarkness;

        startingLight = 0;
        lightCount = startingLight;
    }

    private void Update()
    {
        if (darknessCount >= maxDarkness)
        {
            PlayerDeath("Dark");
        }

        if (lightCount >= maxLight)
        {
            PlayerDeath("Light");
        }

        if (Input.GetKeyDown(projectileKey))
        {
            FireProjectile();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LightZone"))
        {
            SetZone(CurrentZone.light);
        }

        if (collision.CompareTag("DarkZone"))
        {
            SetZone(CurrentZone.dark);
        }
        if (collision.CompareTag("RespawnPoint"))
        {
            respawner.transform.position = collision.transform.position;
        }

        if (collision.CompareTag("Enemy"))
        {
            if(canTakeDamage)
            {
                switch (currentZone)
                {
                    case CurrentZone.light:
                        AudioManager.instance.PlaySfx(damageSound);
                        PlayerDeath("Light");
                        break;
                    case CurrentZone.dark:
                        AudioManager.instance.PlaySfx(damageSound);
                        PlayerDeath("Dark");
                        break;
                }


                StartCoroutine(DamageCooldown());
            }

        }


        if (collision.CompareTag("End"))
        {
            UIManager.instance.LoadScene("TitleScene");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("LightZone"))
        {
            SetZone(CurrentZone.light);
        }

        if (collision.CompareTag("DarkZone"))
        {
            SetZone(CurrentZone.dark);
        }
    }

    IEnumerator DamageCooldown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(takeDamageCooldown);
        canTakeDamage = true;
    }

    private void SetZone(CurrentZone newZone)
    {
        if (currentZone != newZone)
        {
            currentZone = newZone;
            ZoneEffects();
        }
    }

    private void ZoneEffects()
    {
        switch (currentZone)
        {
            case CurrentZone.light:
                //What happens when player is in light zone

                break;

            case CurrentZone.dark:
                //What happens when player is in dark zone

                break;
        }   
        
    }

    public void AddStacks(int amount)
    {
        if (currentZone == CurrentZone.light)
        {
            lightCount++;
            darknessCount = startingDarkness;
        }

        if (currentZone == CurrentZone.dark)
        {
            darknessCount++;
            lightCount = startingLight;
        }


        UIManager.instance.UpdateUI();
    }

    private void FireProjectile()
    {
        float offset = 0.35f;
        Vector3 spawnOffset = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            spawnOffset = new Vector3(0, offset, 0);
        }

        if (Input.GetKey(KeyCode.S))
        {
            spawnOffset = new Vector3(0, -offset, 0);

        }

        Vector2 shootDirection = playerMovement.isFacingRight ? Vector2.right : Vector2.left;


        if (canFire == true)
        {

            Instantiate(projectilePrefab, firePoint.position + spawnOffset, Quaternion.Euler(0, playerMovement.isFacingRight ? 0 : 180, 0));
            AddStacks(1);
            AudioManager.instance.PlaySfx(projectileSound);
            StartCoroutine(ProjectileCooldown());
        }
           
    }

    IEnumerator ProjectileCooldown()
    {
        canFire = false;

        yield return new WaitForSeconds(projectileCooldown);

        canFire = true;
    }

    private void PlayerDeath(string form)
    {
        transform.position = respawner.transform.position;
        lightCount = startingLight;
        darknessCount = startingDarkness;

        UIManager.instance.UpdateUI();

        switch (form)
        {
            case "Light":
                AudioManager.instance.PlaySfx(lightDeathSound);

                break;

            case "Dark":
                AudioManager.instance.PlaySfx(darkDeathSound);

                break;
        }


    }
}