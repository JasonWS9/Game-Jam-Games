using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [SerializeField] private GameObject[] route;
    private GameObject target;
    int routeIndex = 0;

    [SerializeField] private float speed = 5f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        target = route[routeIndex];
        MoveTo(target);

        if(Vector2.Distance(transform.position, target.transform.position) < 0.1 )
        {
            routeIndex++;

            if(routeIndex >= route.Length)
            {
                routeIndex = 0;
            }
        }
    }


    private void MoveTo(GameObject target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }
}
