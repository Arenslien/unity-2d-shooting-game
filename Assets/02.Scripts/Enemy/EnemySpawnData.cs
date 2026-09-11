using UnityEngine;

// Data 클래스: 순수하게 데이터를 보관하고 전달하는 목적으로 만든 클래스
// - 로직이 있으면 안됨.

[System.Serializable]
public class EnemySpawnData
{
    public GameObject EnemyPrefab;
    public int Weight;
}