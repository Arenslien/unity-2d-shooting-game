using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private ItemSpawnDataTableSO _itemDataTable;

    [SerializeField] private int _health = 100;
    [SerializeField] protected float _moveSpeed = 1;
    [SerializeField] private int _damage;

    // 적이 소지한 드랍아이템 테이블
    [SerializeField] private GameObject _deathEffectPrefab; // 죽을 때 생성할 이펙트 프리팹

    // 애니메이션 적용 필드
    private Animator _animator;
    private string _parameterName = "IsHit";

    // Todo: Enemy가 공격 당할 때 재생시켜주는 피격 사운드
    private AudioSource _damagedAudioSource;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _damagedAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Move(); // 기본 이동 방식 --> 각각의 자식 클래스 메서드 적용
    }

    protected abstract void Move();

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health > 0)
        {
            _damagedAudioSource.Play();
        }
        else
        {
            SpawnDeathEffect();
            DropItem();

            ScoreManager.Instance.AddScore(100);

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

    private void DropItem()
    {
        if (_itemDataTable == null || _itemDataTable.Items.Length == 0) return;

        int dropProbability = UnityEngine.Random.Range(0, 100);
        if (dropProbability >= 30) return;

        int totalWeight = 0;
        foreach (ItemSpawnData data in _itemDataTable.Items)
        {
            totalWeight += data.Weight;
        }

        int randomWeight = UnityEngine.Random.Range(0, totalWeight);

        int cumulativeWeight = 0;
        foreach (ItemSpawnData data in _itemDataTable.Items)
        {
            cumulativeWeight += data.Weight;
            if (cumulativeWeight > randomWeight)
            {
                GameObject dropItem = Instantiate(data.ItemPrefab);
                dropItem.transform.position = transform.position;
                break;
            }
        }
    }

    public void TakeExplosion()
    {
        SpawnDeathEffect();
        Destroy(gameObject);
    }
}