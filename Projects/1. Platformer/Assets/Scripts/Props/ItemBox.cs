using UnityEngine;

public class ItemBox : HittableFromBelow
{
    [SerializeField] GameObject _prefabItem;
    [SerializeField] GameObject _item;
    [SerializeField] Vector2 _itemLaunchVelocity;

    bool _used;
 
    void Start()
    {
        _item?.SetActive(false);
    }

    protected override bool CanUse => !_used ;

    protected override void Use()
    {
        _item = Instantiate(_prefabItem, transform.position + Vector3.up, Quaternion.identity, transform);

        if (_item == null)
            return;

        _used = true;
        _item.SetActive(true);
        Rigidbody2D itemRigidbody = _item.GetComponent<Rigidbody2D>();
        if (itemRigidbody != null)
        {
            itemRigidbody.velocity = _itemLaunchVelocity;
        }
    }
}
