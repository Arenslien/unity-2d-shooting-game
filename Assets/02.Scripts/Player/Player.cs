using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _deathEffectPrefab;

    // 캡슐화
    // - 데이터 은닉
    // - 메서드를 통한 상태 변경
    private int _health = 100;

    // 잘 설계된 클래스는
    // - 필드 (인스턴스 변수)
    // - 필드에 잘못된 값이 할당되지 않게 막고, 정상적으로 동작하는 메서드

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Instantiate(_deathEffectPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }

    public void RestoreHealth(int health)
    {
        if (health < 0)
        {
            Debug.Log("힐량은 음수일 수 없습니다.");
            return;
        }

        _health += health;
    }

    // public int GetHealth()
    // {
    //     return _health;
    // }

    // Property
    // public int Health
    // {
    //     set
    //     {
    //         if (value < 0) return;
    //         _health = value;
    //     }
    //     
    //     get
    //     {
    //         return _health;
    //     }
    // }

    public int Health => _health; // 람다식 문법을 활용한 읽기 전용(read only) 프로퍼티
}