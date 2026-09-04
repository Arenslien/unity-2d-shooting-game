using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float MoveSpeed;
    public int BulletDamage;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 direction = Vector2.up; // new Vector2(0, 1)과 동일!                // 1. 방향 설정
        transform.Translate(direction * (MoveSpeed * Time.deltaTime)); // 2. 발사 (이동)
    }

    // 트리거 관련 이벤트
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 1. 충돌한 경우 바로 총알 게임 오브젝트 제거
        Destroy(gameObject);

        // 2. 충돌한 객체가 Enemy인 경우 : Enemy와 상호작용 진행
        if (other.gameObject.CompareTag("Enemy")) // 게임오브젝트의 태그 비교
        {
            // 2.1 충돌한 객체 참조
            Enemy enemy = other.gameObject.GetComponent<Enemy>(); // GetComponent<타입>() --> 해당 겜옵젝의 컴포넌트 참조

            enemy.TakeDamage(BulletDamage);
        }
    }
}