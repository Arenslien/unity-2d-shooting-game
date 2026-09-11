using System;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Item : MonoBehaviour
{
    // 전역 정적 변수: 플레이어
    protected static GameObject _playerObject = null;

    // [SerializeField] 아이템 값: 아이템 획득 이펙트, 속도
    [SerializeField] protected GameObject _itemAcquireEffectPrefab;
    [SerializeField] private float _speed = 2f;

    // 일반 private 값: 자석모드 쿨타임, 현재 쿨타임, 마그넷 모드
    private float _magnetModeCoolTime = 3f;
    private float _currentTime = 0;
    private bool _isMagnetMode = false;

    // 모든 Item 클래스의 객체들은 동일한 하나의 player 객체를 공유
    private void Awake()
    {
        if (_playerObject == null)
        {
            _playerObject = GameObject.FindWithTag("Player");
        }
    }

    private void Update()
    {
        if (!_isMagnetMode)
        {
            CheckItemCoolTime();
        }
        else
        {
            FlyToPlayer();
        }
    }

    private void CheckItemCoolTime()
    {
        if (_currentTime < _magnetModeCoolTime)
        {
            _currentTime += Time.deltaTime;
        }
        else
        {
            _isMagnetMode = true;
        }
    }

    private void FlyToPlayer()
    {
        if (_playerObject == null) return;

        // 1. 방향 설정
        Vector2 direction = (_playerObject.transform.position - transform.position).normalized;

        // 2 이동
        transform.Translate(_speed * Time.deltaTime * direction);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        ApplyItemEffect();

        Instantiate(_itemAcquireEffectPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    protected abstract void ApplyItemEffect();
}