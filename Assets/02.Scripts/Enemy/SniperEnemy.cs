using UnityEngine;
using UnityEngine.UIElements;

public class SniperEnemy : Enemy
{
    private Vector2 _direction = new Vector2(0, 0);

    private void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");

        if (playerObject == null) return;

        _direction = (playerObject.transform.position - transform.position).normalized;

        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(0, 0, angle + 90);
    }

    // 목표: SniperEnemy가 생성된 시점의 Player 위치 방향으로 이동
    protected override void Move()
    {
        // 이동
        transform.Translate(_direction * (_moveSpeed * Time.deltaTime), Space.World);
    }
}