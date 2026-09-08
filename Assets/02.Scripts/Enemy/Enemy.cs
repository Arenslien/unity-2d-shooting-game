using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private int _health = 100;
    [SerializeField] protected float _moveSpeed = 1;
    [SerializeField] private int _damage;

    // 적이 소지한 드랍아이템 테이블
    [SerializeField] private Item[] _dropItems = new Item[] { };
    [SerializeField] private int _dropProbability = 30;
    [SerializeField] private GameObject _deathEffectPrefab; // 죽을 때 생성할 이펙트 프리팹

    // 애니메이션 적용 필드
    private Animator _animator;
    private string _parameterName = "IsHit";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

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
            DropItem();

            SpawnDeathEffect();

            Destroy(gameObject);
        }
    }

    private void SpawnDeathEffect()
    {
        Instantiate(_deathEffectPrefab, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();

            player.TakeDamage(_damage);

            Destroy(gameObject);
        }
        else if (other.CompareTag("Bullet"))
        {
            Bullet bullet = other.GetComponent<Bullet>();

            _animator.SetTrigger(_parameterName);
        }
    }

    // Todo: Scriptable Object를 사용해서 리팩토링
    private void DropItem()
    {
        if (_dropItems == null || _dropItems.Length == 0) return;

        int randomPercent = UnityEngine.Random.Range(0, 100);

        if (randomPercent >= _dropProbability) return;

        int itemIndex = UnityEngine.Random.Range(0, _dropItems.Length);

        Item dropItem = Instantiate(_dropItems[itemIndex]);
        dropItem.transform.position = transform.position;
    }

    public void TakeExplosion()
    {
        SpawnDeathEffect();
        Destroy(gameObject);
    }
}