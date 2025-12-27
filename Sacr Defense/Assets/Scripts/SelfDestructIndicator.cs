using UnityEngine;

public class SelfDestructIndicator : MonoBehaviour
{

    private Animator anim;

    [SerializeField] private float lifetime = 1f;

    void Start()
    {
        anim = GetComponent<Animator>();

        Destroy(gameObject, lifetime);
    }

}
