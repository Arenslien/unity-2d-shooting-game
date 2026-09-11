using UnityEngine;

public class MoveSpeedItem : Item
{
    protected override void ApplyItemEffect()
    {
        PlayerMove playerMove = _playerObject.GetComponent<PlayerMove>();
        playerMove.IncreaseSpeed(1f);

        Debug.Log($"현재 이동 속도: {playerMove.Speed}");
    }
}