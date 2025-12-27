using System.Collections;
using UnityEngine;

public class UnitScript : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private bool canAttack = true;

    [SerializeField] private float attackCooldown = 3f;

    [SerializeField] private LayerMask enemyLayer;


    [SerializeField] private float selfDestructRadius = 2f;

    [SerializeField] private GameObject selfDestructRadiusIndicator;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        float selfDestructDiameter = selfDestructRadius * 2;
        selfDestructRadiusIndicator.transform.localScale = new Vector3(selfDestructDiameter, selfDestructDiameter, 1f);
    }

    private void OnMouseDown()
    {
        Debug.Log("should show ui");
        UnitActionsUI.Instance.ShowUnitActions(this);
    }

   public void Attack(string attackType)
    {
        if (canAttack)
        {
            if (GameManager.instance.playerEnergy >= 1)
            {
                Debug.Log("Preforming Attack: " + attackType);
                StartCoroutine(AttackCooldown());
            }
            else { Debug.Log("Not enough energy"); }


            switch (attackType)
            {
                case "Attack1":

                    SelfDestruct();

                break;

                case "Attack2":

                break;

                case "Movement":

                break;
            }
        }

    }


    public IEnumerator AttackCooldown()
    {
        canAttack = false;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        spriteRenderer.color = Color.cyan;
    }

    private void SelfDestruct()
    {
        float scoreAmount = 10f;
        scoreAmount = 10f;
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, selfDestructRadius, enemyLayer);

        foreach (Collider2D enemy in hits)
        {
            Destroy(enemy.gameObject);
            GameManager.instance.enemiesKilled++;
            GameManager.instance.score += (Mathf.RoundToInt(scoreAmount * GameManager.instance.scoreMultiplier));
            scoreAmount += 5;
            GameManager.instance.UpdateText();
        }

        GameObject sdIndicator = Instantiate(selfDestructRadiusIndicator, transform.position, Quaternion.identity);

        Debug.Log("Should self destruct");
        Destroy(gameObject);
        UnitPlacer.instance.unitCount--;
        GameManager.instance.UpdateEnergy(-1);
        GameManager.instance.UpdateText();

        AudioManager.instance.PlayAudioClip(AudioManager.instance.explodeSound);

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, selfDestructRadius);
    }

}
