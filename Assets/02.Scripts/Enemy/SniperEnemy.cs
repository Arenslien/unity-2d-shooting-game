using UnityEngine;
using UnityEngine.UIElements;

public class SniperEnemy : Enemy
{
    private Vector2 _direction = new Vector2(0, 0);

    private void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        _direction = (playerObject.transform.position - transform.position).normalized;
        Debug.Log(_direction);
    }

    // 목표: SniperEnemy가 생성된 시점의 Player 위치 방향으로 이동
    protected override void Move()
    {
        // 이동
        transform.Translate(_direction * (MoveSpeed * Time.deltaTime));
    }
}