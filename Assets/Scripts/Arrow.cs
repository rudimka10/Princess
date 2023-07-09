using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Arrow : MonoBehaviour
{

    private Rigidbody2D _rigidbody;
    private BoxCollider2D _collider;
    private Transform _location;
    private int _damage;

    public void Fire(int damage, Vector3 location)
    {
        _damage = damage;

        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.TryGetComponent(out PlayerController player))
        {
            var health = player.GetComponent<Health>();
            health?.Damage(_damage);
            Destroy(gameObject);
        }
        else
        {
            _rigidbody.bodyType = RigidbodyType2D.Kinematic;
            _collider.isTrigger = true;
        }


    }
}
