using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float Speed;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        // 2. 방향 설정
        Vector2 direction = Vector2.up; // new Vector2(0, 1)과 동일!

        // 3. 발사
        transform.Translate(direction * Speed * Time.deltaTime);

        if (transform.position.y > 5.2)
        {
            Destroy(gameObject);
        }
    }
}