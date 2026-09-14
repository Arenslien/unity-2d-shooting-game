using UnityEngine;

enum UpgradeButtonType
{
    AttackPower,
    AttackSpeed,
    MoveSpeed
}

public class UI_UpgradeButton : MonoBehaviour
{
    // SerializeField 변수
    [SerializeField] UpgradeButtonType _upgradeButtonType;

    public void Upgrade()
    {
        switch (_upgradeButtonType)
        {
            case UpgradeButtonType.AttackPower:
                Player.AttackPowerUp(5);
                Debug.Log("AttackPower");
                break;
            case UpgradeButtonType.AttackSpeed:
                Player.AttackSpeedUp(0.1f);
                Debug.Log("AttackSpeed");
                break;
            case UpgradeButtonType.MoveSpeed:
                Player.MoveSpeedUp(0.1f);
                Debug.Log("MoveSpeed");
                break;
        }
    }
}