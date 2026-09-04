using UnityEngine;

// 목표: 내가 플레이어와 부딪히면(아이템 획득 시) 해당 아이템 효과 반영
public class AttackSpeedItem : Item
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerFire playerFire = other.GetComponent<PlayerFire>();
        playerFire.IncreaseAttackSpeed(0.1f);
    }
}