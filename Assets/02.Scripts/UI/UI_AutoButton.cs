using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

public class UI_AutoButton : MonoBehaviour
{
    // 버튼을 클릭하면 토글하고 싶다.
    // - 플레이어의 자동 이동
    // - 플레이어의 자동 공격

    private bool _autoMode = false;
    private Player _player;

    // 버튼 이미지 관련
    [SerializeField] private Sprite _onSprite;
    [SerializeField] private Sprite _offSprite;
    private Image _myImage;

    private void Start()
    {
        _player = GameObject.FindAnyObjectByType<Player>();
        _myImage = GetComponent<Image>();
    }

    public void AutoToggle()
    {
        _autoMode = !_autoMode;

        _player.GetComponent<PlayerFire>().ToggleAutoMode();
        _player.GetComponent<PlayerAutoMove>().ToggleAutoMode();

        _player.GetComponent<PlayerMove>().enabled = !_autoMode;

        _myImage.sprite = _autoMode ? _onSprite : _offSprite;
    }
}