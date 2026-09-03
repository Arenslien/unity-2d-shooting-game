using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float MoveSpeed;
    public float BulletDamage;
    private float _maxPositionX = 5.2f;

    private void Update()
    {
        Move();

        DestoryIfOutOfBounds();
    }

    private void Move()
    {
        Vector2 direction = Vector2.up; // new Vector2(0, 1)과 동일!                // 1. 방향 설정
        transform.Translate(direction * (MoveSpeed * Time.deltaTime)); // 2. 발사 (이동)
    }

    private void DestoryIfOutOfBounds()
    {
        if (transform.position.y > _maxPositionX)
        {
            Destroy(gameObject);
        }
    }

    // 충돌 관련 이벤트 (Enter --> Stay --> Exit)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 1. 충돌한 경우 바로 총알 게임 오브젝트 제거
        Destroy(gameObject);

        // 2. 충돌한 객체가 Enemy인 경우 : Enemy와 상호작용 진행
        if (collision.gameObject.CompareTag("Enemy")) // 게임오브젝트의 태그 비교
        {
            // 2.1 충돌한 객체 참조
            Enemy enemy = collision.gameObject.GetComponent<Enemy>(); // GetComponent<타입>() --> 해당 겜옵젝의 컴포넌트 참조
            enemy.Health -= BulletDamage; // 총알 데미지 반영

            // 2.2 Enemy 체력 체크
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