using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private int _health = 100;
    [SerializeField] protected float _moveSpeed = 1;
    [SerializeField] private int _damage;

    // 적이 소지한 드랍아이템 테이블
    private Item[] _dropItems = new Item[] { };

    private void Update()
    {
        Move(); // 기본 이동 방식 --> 각각의 자식 클래스 메서드 적용
    }

    protected abstract void Move();

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            DropItem();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        Player player = other.gameObject.GetComponent<Player>();

        player.TakeDamage(_damage);

        Destroy(gameObject);
    }

    private void DropItem()
    {
        int randomPercent = UnityEngine.Random.Range(0, 100);

        if (randomPercent >= 30) return;

        int itemIndex = UnityEngine.Random.Range(0, 3);

        Item dropItem = Instantiate(_dropItems[itemIndex]);
        dropItem.transform.position = transform.position;
    }

    public void SetDropItems(Item[] items)
    {
        _dropItems = items;
    }
}