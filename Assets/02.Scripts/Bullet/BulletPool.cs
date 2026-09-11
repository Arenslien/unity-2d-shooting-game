using UnityEngine;

// 오브젝트 풀링: 오브젝트의 Pool(웅덩이: 창고)을 만들어두고,
// 그 창고 안에 게임 오브젝트를 미리 필요한 만큼 채워두고,
// 필요할 때마다 꺼내서 사용하고 필요 없으면 반환하는 식으로 (활성화/비활성화)
// 메모리 할당과 해제를 최소화해서 성능 UP!
public class BulletPool : MonoBehaviour
{
    // [SerializeField] 변수 선언
    [Header("총알 프리팹들")]
    [SerializeField] private Bullet[] _bulletPrefabs;

    [Header("풀 사이즈")]
    [SerializeField] private int _poolSize;

    // 생성한 총알을 담아둘 풀
    private Bullet[,] _pool; // 후에 사이즈 결정 예정 (_poolSize 초기값 없음)

    // 싱글톤 패턴을 위한 static Instance 선언
    private static BulletPool _instance = null;
    public static BulletPool Instance => _instance;

    // 1. 초기 기능 실행
    private void Awake()
    {
        // 1.1 싱글톤 패턴 설정
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;

        // 1.2 Bullet 프리팹 초기화
        InitializeBullet();
    }

    private void InitializeBullet()
    {
        // 창고 크기 만큼 총알을 미리 만들어서 집어 넣는다.
        _pool = new Bullet[_bulletPrefabs.Length, _poolSize];

        // 총알 프리팹 별 총알 개수만큼 생성
        for (int i = 0; i < _bulletPrefabs.Length; i++)
        {
            Bullet bulletPrefabs = _bulletPrefabs[i]; // [메인 총알, 서브 총알]

            for (int j = 0; j < _poolSize; j++)
            {
                // 첫 번째 인자: 인스턴스 만들 프리팹,
                // 두 번째 인자: 부모 게임 오브젝트 안에 속하게끔 결정할 것
                Bullet bullet = Instantiate(bulletPrefabs, gameObject.transform);
                bullet.gameObject.SetActive(false);
                _pool[i, j] = bullet;
            }
        }
    }

    // 목표: 원하는 타입의 Bullet 중 Pool에서 비활성화된 것 반환
    public Bullet GetBullet(BulletType bulletType)
    {
        for (int i = 0; i < _bulletPrefabs.Length; i++)
        {
            if (_pool[i, 0].Type != bulletType) continue;

            for (int j = 0; j < _poolSize; j++)
            {
                Bullet bullet = _pool[i, j];
                if (bullet.gameObject.activeSelf == false)
                {
                    bullet.gameObject.SetActive(true);
                    bullet.OnSpawn();
                    return bullet;
                }
            }
        }

        return null;
    }
}