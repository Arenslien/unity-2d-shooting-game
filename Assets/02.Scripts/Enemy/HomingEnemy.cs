using UnityEngine;

public class HomingEnemy : Enemy
{
    private void Update()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
    }

    protected override void Move()
    {
        Vector2 direction = (playerObject.transform.position - transform.position).normalized;

        transform.Translate(direction * (_moveSpeed * Time.deltaTime));
    }
}