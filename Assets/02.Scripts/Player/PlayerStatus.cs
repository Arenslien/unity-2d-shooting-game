using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private int _health = 100;

    public void takeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Debug.Log("플레이어 체력: 0");
            Destroy(gameObject);
        }
    }
}