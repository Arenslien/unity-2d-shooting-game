using UnityEngine;

// 목표: 나(DestroyZone)와 충돌한 다른 게임 오브젝트는 누구든 파괴해버리겠다.
public class DestroyZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
    }
}