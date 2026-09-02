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
        FireBullet();
        
        
        CheckCoolTime();
        
        ChangeAutoMode();
    }

    private void FireBullet()
    {
        // 1. 키보드 입력 받기: GetKeyDown은 눌렀을 때 한 번
        if (!_isBulletFire && (_isAutoMode || Input.GetKeyDown(KeyCode.Space)))
        {
            // 2. 총알 프리팹을 생성한다.
            // Instantiate는 프리팹을 복사해서 (MonoBehaviour를 상속받는)게임 오브젝트를 생성하고 씬에 넣어주는 기능
            GameObject bulletL = Instantiate(BulletPrefab);
            GameObject bulletR = Instantiate(BulletPrefab);
            
            // 총알 양쪽 발사
            bulletL.transform.position = FirePointL.position; // 생성한 총알의 위치를 나(플레이어)의 위치로
            bulletR.transform.position = FirePointR.position;
            
            // 쿨타임 시작
            _isBulletFire = true;
        }
        
        // 1. 키보드 입력 받기: GetKeyDown은 눌렀을 때 한 번
        if (!_isSupportBulletFire && (_isAutoMode || Input.GetKeyDown(KeyCode.Space)))
        {
            // 2. 보조 총알 양쪽 발사
            GameObject supportBulletL = Instantiate(SupportBulletPrefab);
            GameObject supportBulletR = Instantiate(SupportBulletPrefab);
            supportBulletL.transform.position = SupportFirePointL.position;
            supportBulletR.transform.position = SupportFirePointR.position;
            
            // 쿨타임 시작
            _isSupportBulletFire = true;
        }
        
    }

    private void CheckCoolTime()
    {
        if (_isBulletFire)
        {
            _currentBulletCoolTime += Time.deltaTime;
        }

        if (_currentBulletCoolTime >= _fireBulletCoolTime)
        {
            _isBulletFire = false;
            _currentBulletCoolTime = 0.0f;
        }
        
        if (_isSupportBulletFire)
        {
            _currentSupportBulletCoolTime += Time.deltaTime;
        }

        if (_currentSupportBulletCoolTime >= _fireSupportBulletCoolTime)
        {
            _isSupportBulletFire = false;
            _currentSupportBulletCoolTime = 0.0f;
        }
    }

    private void ChangeAutoMode()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _isAutoMode = !_isAutoMode;
            Debug.Log($"자동 공격 모드 {(_isAutoMode ? "ON": "OFF")}");
        }
    }
}
