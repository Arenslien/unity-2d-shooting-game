using UnityEngine;

public class Player : MonoBehaviour
{
    // Component 변수 (캐싱)
    [SerializeField] private GameObject _deathEffectPrefab;
    private AudioSource _damagedAudioSource;

    // Player 기본 스탯 변수
    private static int _health = 100;
    private static int _mainBulletDamage = 10;
    private static int _subBulletDamage = 5;
    private static float _attackSpeed = 0.5f;
    private static float _moveSpeed = 3.0f;

    // property
    public static int Health => _health; // 람다식 문법을 활용한 읽기 전용(read only) 프로퍼티
    public static float MoveSpeed => _moveSpeed;

    // static 메서드
    public static void RestoreHealth(int health)
    {
        if (health < 0) return;
        _health += health;
    }

    public static void MoveSpeedUp(float speed)
    {
        if (speed < 0) return;
        _moveSpeed += speed;
    }

    public static void AttackSpeedUp(float speed)
    {
        if (speed < 0) return;
        _attackSpeed += speed;
    }

    public static void AttackPowerUp(int value)
    {
        if (value < 0) return;
        _mainBulletDamage += value;
        _subBulletDamage += (int)(value / 2);
    }


    // Awake: Component 캐싱
    private void Awake()
    {
        _damagedAudioSource = GetComponent<AudioSource>();
    }

    // public 메서드
    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health > 0)
        {
            _damagedAudioSource.Play();
        }
        else
        {
            Instantiate(_deathEffectPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}