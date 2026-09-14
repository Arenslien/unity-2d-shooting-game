using UnityEngine;

public class MoveSpeedItem : Item
{
    protected override void ApplyItemEffect()
    {
        Player.MoveSpeedUp(1f);
        Debug.Log($"현재 이동 속도: {Player.MoveSpeed}");
    }
}