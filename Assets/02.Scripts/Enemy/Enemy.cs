using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed;
    private float _minPositionY = -5.5f;

    private void Update()
    {
        Move();

        // 3. 오브젝트 삭제
        if (transform.position.y <= _minPositionY)
        {
            Destroy(gameObject);
        }
    }

    private void Move()
    {
        // 1. 방향 설정
        Vector2 direction = new Vector2(0, -1); // Vector2.down;

        // 2. 오브젝트 이동
        transform.Translate(direction * (Speed * Time.deltaTime));
    }
}