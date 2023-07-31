using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ExplosionFragment : MonoBehaviour
{

	private Rigidbody2D _rigidbody;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
	}

	public void Push(Vector2 force, float lifeTime)
	{
		transform.parent = null;
		_rigidbody.AddForce(force);
		Invoke(nameof(Clear), lifeTime);
	}

	private void Clear()
	{
		Destroy(gameObject);
	}
}