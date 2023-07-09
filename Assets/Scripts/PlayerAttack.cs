using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField]
    private float _attackDelay = 0.4f;

    [SerializeField]
    private int _damage = 1;

    [Space]

    [SerializeField]
    private Transform _aimPoint;

    [SerializeField]
    private float _searchRadius;

    [Space]

    [SerializeField]
    private Animator animator;

    private float _timer;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _timer <= 0)
        {
            animator.SetTrigger("attack");
            _timer = _attackDelay;
            var enemies = FindEnemiesAttacked();
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Damage(_damage);
            }
        }
    }

    private void FixedUpdate()
    {
        if (_timer > 0)
            _timer -= Time.fixedDeltaTime;
    }

    private List<Health> FindEnemiesAttacked()
    {
        var allEnemies = FindObjectsOfType<EnemyController>();
        var attacked = new List<Health>();

        foreach (var e in allEnemies)
        {
            if (Vector3.Distance(_aimPoint.position, e.transform.position) <= _searchRadius)
                if (e.TryGetComponent(out Health health) && health.IsAlive)
                {
                    attacked.Add(health);
                }
        }

        return attacked;
    }
}