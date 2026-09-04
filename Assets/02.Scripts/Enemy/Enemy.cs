using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private int _health = 100;
    [SerializeField] protected float _moveSpeed = 2;
    private float _minPositionY = -5.5f;
    private int _damage = 10;

    private void Update()
    {
        Move(); // 기본 이동 방식 --> 각각의 자식 클래스 메서드 적용

        DestroyIfOutOfBounds(); // 기본 오브젝트 제거 (하단 범위 벗어날 경우)
    }

    protected abstract void Move();

    private void DestroyIfOutOfBounds()
    {
        if (transform.position.y <= _minPositionY)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerStatus playerStatus = other.gameObject.GetComponent<PlayerStatus>();

            playerStatus.takeDamage(_damage);

            Destroy(gameObject);
        }
    }
}