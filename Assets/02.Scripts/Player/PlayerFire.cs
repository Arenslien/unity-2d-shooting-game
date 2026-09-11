using System;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // 목표: 스페이스바를 누를 때마다 총알을 생성해서 발사하고 싶다.
    // 필요 속성
    // - 총알 프리팹
    // - 생성 위치 (총구)
    public Transform LeftFirePoint;
    public Transform RightFirePoint;
    public Transform SubLeftFirePoint;
    public Transform SubRightFirePoint;

    private float _mainCoolTimer = 1f;
    private float _mainCoolTime = 1f;
    private float _subCoolTimer = 1.5f;
    private float _subCoolTime = 1.5f;

    private bool _isAutoMode = false;

    // Bullet Pool
    private BulletPool _bulletPool = null;

    private void Start()
    {
        _bulletPool = BulletPool.Instance;
    }

    private void Update()
    {
        // 자동 공격 모드 토글
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ToggleAutoMode();
        }

        // Main/Sub Bullet 발사 쿨타임 체크
        _mainCoolTimer += Time.deltaTime;
        _subCoolTimer += Time.deltaTime;

        // 총알 쿨타임 만족 && 오토 모드 or 스페이스바 입력
        if (_mainCoolTimer >= _mainCoolTime && (_isAutoMode || Input.GetKeyDown(KeyCode.Space)))
        {
            Fire(BulletType.Main, LeftFirePoint, RightFirePoint);

            _mainCoolTimer = 0f;
        }

        if (_subCoolTimer >= _subCoolTime && (_isAutoMode || Input.GetKeyDown(KeyCode.Space)))
        {
            Fire(BulletType.Sub, SubLeftFirePoint, SubRightFirePoint);

            _subCoolTimer = 0f;
        }
    }

    private void Fire(BulletType bulletType, Transform leftPoint, Transform rightPoint)
    {
        Bullet leftBullet = BulletPool.Instance.GetBullet(bulletType);

        leftBullet.transform.position = leftPoint.position;

        Bullet rightBullet = BulletPool.Instance.GetBullet(bulletType);

        rightBullet.transform.position = rightPoint.position;
    }

    private void ToggleAutoMode()
    {
        _isAutoMode = !_isAutoMode;
        Debug.Log($"자동 공격 모드 {(_isAutoMode ? "ON" : "OFF")}");
    }

    public void IncreaseAttackSpeed(float speed)
    {
        if (_mainCoolTimer > 0.5f)
        {
            _mainCoolTimer -= speed;
        }

        if (_subCoolTimer > 0.8f)
        {
            _subCoolTimer -= speed;
        }
    }

    public float MainAttackSpeed => _mainCoolTimer;
}