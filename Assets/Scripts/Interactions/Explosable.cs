using UnityEngine;

public class Explosable : MonoBehaviour
{

	[SerializeField]
	private ExplosionFragment[] _fragments;

	[SerializeField]
	private float _force = 1;

	[SerializeField]
	private float _lifeTime = 5;

	public void Explode()
	{
		foreach (var fragment in _fragments)
		{
			fragment.gameObject.SetActive(true);
			var force = new Vector2(Random.Range(-1, 1), 1).normalized * _force;
			fragment.Push(force, _lifeTime);
		}

		Destroy(gameObject);
	}
}