using UnityEngine;

// 목표: 내가 플레이어와 부딪히면(아이템 획득 시) 해당 아이템 효과 반영
public class AttackSpeedItem : Item
{
    protected override void ApplyItemEffect()
    {
        PlayerFire playerFire = _playerObject.GetComponent<PlayerFire>();
        playerFire.IncreaseAttackSpeed(0.1f);
        Debug.Log($"공격 속도 증가 - 현재 공속: {playerFire.MainAttackSpeed}");
    }
}