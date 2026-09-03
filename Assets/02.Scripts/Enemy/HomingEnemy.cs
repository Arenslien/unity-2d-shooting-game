using UnityEngine;

public class HomingEnemy : Enemy
{
    protected override void Move()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");

        Vector2 direction = (playerObject.transform.position - transform.position).normalized;

        transform.Translate(direction * (_moveSpeed * Time.deltaTime));
    }
}