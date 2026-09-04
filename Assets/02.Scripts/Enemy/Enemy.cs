using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private int _health = 100;
    [SerializeField] protected float _moveSpeed = 2;
    [SerializeField] private int _damage;

    private void Update()
    {
        Move(); // 기본 이동 방식 --> 각각의 자식 클래스 메서드 적용
    }

    protected abstract void Move();

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
        if (!other.CompareTag("Player")) return;
        PlayerStatus playerStatus = other.gameObject.GetComponent<PlayerStatus>();

        playerStatus.TakeDamage(_damage);

        Destroy(gameObject);
    }
}