using UnityEngine;

public class HealthItem : Item
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Player player = other.GetComponent<Player>();
        player.RestoreHealth(10);

        Debug.Log($"현재 체력: {player.Health}");
        Destroy(gameObject);
    }
}