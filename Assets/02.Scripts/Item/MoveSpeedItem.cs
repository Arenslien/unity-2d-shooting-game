using UnityEngine;

public class MoveSpeedItem : Item
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerMove playerMove = other.GetComponent<PlayerMove>();
        playerMove.IncreaseSpeed(1f);
    }
}