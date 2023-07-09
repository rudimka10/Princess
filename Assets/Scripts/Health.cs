using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{

    [SerializeField]
    private int _health = 5;
    
    [SerializeField]
    private int _healthMax = 5;

    [SerializeField]
    private bool _connectHealthBarOnStart;

    [Space]

    [SerializeField]
    private UnityEvent _onDeath;

    HealthBar _healthBar;

    private void Start()
    {
        if (_connectHealthBarOnStart)
            _healthBar = FindObjectOfType<HealthBar>();
    }

    public void Damage(int value)
    {
        OnHealthChanged(-value);
    }

    public void Heal(int value)
    {
        OnHealthChanged(value);
    }

    public void Kill()
    {
        OnHealthChanged(-_healthMax);
    }

    public void Restore()
    {
        OnHealthChanged(_healthMax);
    }

    private void OnHealthChanged(int healthDelta)
    {
        _health = Mathf.Clamp(_health + healthDelta, 0, _healthMax);

        if (_healthBar != null)
            _healthBar.Value = _health;

        if (_health > 0)
            return;

        if (_onDeath != null)
            _onDeath.Invoke();
        else
            Destroy(gameObject);
    }
}