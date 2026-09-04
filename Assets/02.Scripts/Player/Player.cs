using UnityEngine;

public class Player : MonoBehaviour
{
    private int _health = 100;

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Debug.Log("플레이어 체력: 0");
            Destroy(gameObject);
        }
    }

    public void RestoreHealth(int health)
    {
        _health += health;
    }
}