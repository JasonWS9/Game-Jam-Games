using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    public float speed = 5f;

    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == ("DeathZone"))
        {
            ReachBottom();
        }
    }



    private void ReachBottom()
    {
        Destroy(gameObject);
        GameManager.instance.UpdateEnergy(-1);
        AudioManager.instance.PlayAudioClip(AudioManager.instance.enemyBottomSound);

    }
}
