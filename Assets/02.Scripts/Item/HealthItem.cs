using UnityEngine;

public class HealthItem : Item
{
    protected override void ApplyItemEffect()
    {
        Player player = _playerObject.GetComponent<Player>();
        player.RestoreHealth(10);

        Debug.Log($"현재 체력: {player.Health}");
    }
}