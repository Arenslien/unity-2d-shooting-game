using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float MoveSpeed;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        // 2. 방향 설정
        Vector2 direction = Vector2.up; // new Vector2(0, 1)과 동일!

        // 3. 발사
        transform.Translate(direction * (MoveSpeed * Time.deltaTime));

        if (transform.position.y > 5.2)
        {
            Destroy(gameObject);
        }
    }

    // 충돌 관련 이벤트 (Enter --> Stay --> Exit)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("충돌 했다.");

        // 나 죽고
        Destroy(gameObject);
        // 너 죽자

        // 충돌한 객체가 Enemy일 때만 없애자
        // - 태그 사용
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // GetComponent<타입>() --> 게임 오브젝트가 가지고 있는 컴포넌트를 창조

            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.Health -= 40;
            if (enemy.Health <= 0)
            {
                Destroy(collision.gameObject);
            }
        }
    }

    private void OnCollisionStay2D(Collider2D collision)
    {
        Debug.Log("충돌 중.");
    }
}