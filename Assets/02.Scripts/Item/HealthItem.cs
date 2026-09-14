using UnityEngine;

public class HealthItem : Item
{
    protected override void ApplyItemEffect()
    {
        Player.RestoreHealth(10);
        Debug.Log($"현재 체력: {Player.Health}");
    }
}