using UnityEngine;

public class UI_AutoButton : MonoBehaviour
{
    // 버튼을 클릭하면 토글하고 싶다.
    // - 플레이어의 자동 이동
    // - 플레이어의 자동 공격

    private bool _autoMode = false;
    private Player _player;

    private void Start()
    {
        _player = GameObject.FindAnyObjectByType<Player>();
    }


    public void AutoToggle()
    {
        _autoMode = !_autoMode;

        _player.GetComponent<PlayerFire>().ToggleAutoMode();
        _player.GetComponent<PlayerAutoMove>().ToggleAutoMode();

        _player.GetComponent<PlayerMove>().enabled = !_autoMode;
    }
}