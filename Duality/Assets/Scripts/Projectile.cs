using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] private float airTime = 3f;
    [SerializeField] float speed = 5f;


    void Start()
    {
        Destroy(gameObject, airTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);

            Destroy(gameObject);
        }

        
    }

    
}
