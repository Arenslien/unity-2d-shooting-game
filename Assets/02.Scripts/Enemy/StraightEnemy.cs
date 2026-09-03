using UnityEngine;

public class StraightEnemy : Enemy
{
    protected override void Move()
    {
        // 1. 방향 설정
        Vector2 direction = new Vector2(0, -1); // Vector2.down;

        // 2. 오브젝트 이동
        transform.Translate(direction * (_moveSpeed * Time.deltaTime));
    }
}