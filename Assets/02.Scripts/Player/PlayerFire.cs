using System;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // 목표: 스페이스바를 누를 때마다 총알을 생성해서 발사하고 싶다.
    // 필요 속성
    // - 총알 프리팹
    // - 생성 위치 (총구)

    public GameObject BulletPrefab;
    public GameObject SupportBulletPrefab;

    public Transform FirePointL;
    public Transform FirePointR;
    public Transform SupportFirePointL;
    public Transform SupportFirePointR;

    private float _fireBulletCoolTime = 0.5f;
    private float _currentBulletCoolTime = 0.0f;
    private bool _isBulletFire = false;

    private float _fireSupportBulletCoolTime = 0.3f;
    private float _currentSupportBulletCoolTime = 0.0f;
    private bool _isSupportBulletFire = false;
    private bool _isAutoMode = false;

    private void Update()
    {
        FireBullet(BulletPrefab, new Transform[2] { FirePointL, FirePointR }, ref _isBulletFire);
        FireBullet(SupportBulletPrefab, new Transform[2] { SupportFirePointL, SupportFirePointR },
            ref _isSupportBulletFire);

        CheckCoolTime(ref _isBulletFire, ref _currentBulletCoolTime, _fireBulletCoolTime);
        CheckCoolTime(ref _isSupportBulletFire, ref _currentSupportBulletCoolTime, _fireSupportBulletCoolTime);

        ChangeAutoMode();
    }

    private void FireBullet(GameObject bulletPrefab, Transform[] firePoints, ref bool isFire)
    {
        // 1. 총알 발사 조건: 미발사 상태인 동시에 Auto모드이거나 Space 키 입력
        if (!isFire && (_isAutoMode || Input.GetKeyDown(KeyCode.Space)))
        {
            // 2. 총알 프리팹 배열 생성
            int bulletCount = firePoints.Length;
            GameObject[] bullets = new GameObject[bulletCount];

            // 모든 총알 위치를 포인트 위치로 설정
            for (int i = 0; i < bulletCount; i++)
            {
                bullets[i] = Instantiate(bulletPrefab);
                bullets[i].transform.position = firePoints[i].position;
            }

            // 쿨타임 시작
            isFire = true;
        }
    }

    private void CheckCoolTime(ref bool isFire, ref float currentCoolTime, float bulletCoolTime)
    {
        if (isFire)
        {
            currentCoolTime += Time.deltaTime;
        }

        if (currentCoolTime >= bulletCoolTime)
        {
            isFire = false;
            currentCoolTime = 0.0f;
        }
    }

    private void ChangeAutoMode()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _isAutoMode = !_isAutoMode;
            Debug.Log($"자동 공격 모드 {(_isAutoMode ? "ON" : "OFF")}");
        }
    }
}