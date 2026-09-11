using UnityEngine;

public class Bullet : MonoBehaviour
{
    // SerializeField 값
    [SerializeField] private BulletType _type;
    public BulletType Type => _type;

    // UnSerializeField 값
    public float MoveSpeed = 5f;
    public int BulletDamage = 10;

    // Component 변수
    private AudioSource _audioSource;

    // 1. Awake() 때 AudioSource 컴포넌트 할당
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void OnSpawn()
    {
        // 프리팹이 풀에 의해서 활성화 될 때마다
        // 초기화 하는 코드들이 들어간다.

        PlaySound();
    }


    private void PlaySound()
    {
        _audioSource.pitch = UnityEngine.Random.Range(1f, 3f);
        _audioSource.Play();
    }

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
        // 1. 충돌한 객체가 Enemy인 경우 : Enemy와 상호작용 진행
        if (other.gameObject.CompareTag("Enemy")) // 게임오브젝트의 태그 비교
        {
            // 1.1 총알 소멸
            gameObject.SetActive(false);

            // 1.1 충돌한 객체 참조
            Enemy enemy = other.gameObject.GetComponent<Enemy>(); // GetComponent<타입>() --> 해당 겜옵젝의 컴포넌트 참조

            enemy.TakeDamage(BulletDamage);
        }
    }
}