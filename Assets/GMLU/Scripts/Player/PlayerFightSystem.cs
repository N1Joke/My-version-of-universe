using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFightSystem : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _healRate;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private Animator _animator;
    [SerializeField] private ToolSwitch _toolSwitch;
    [SerializeField] private CanvasGroup _canvasGroup;

    private int _fullHealth;
    private float _timeSinceLastAttack;
    private float _timeSinceChangeHealth;    

    private void Start()
    {
        _fullHealth = _health;
        _healthBar.SetMinMaxValue(0, _fullHealth);
        _healthBar.follow = true;
        _canvasGroup.alpha = 0f;
    }

    private void FixedUpdate()
    {
        _timeSinceLastAttack += Time.fixedDeltaTime;
        _timeSinceChangeHealth += Time.fixedDeltaTime;

        if (_timeSinceChangeHealth >= _healRate)
            Heal();        
    }

    private void Heal()
    {
        if (_health >= _fullHealth)
        {
            _canvasGroup.alpha = 0f;
            return;
        }
        _health++;
        _healthBar.SetHealth(_health);
        _timeSinceChangeHealth = 0f;
    }

    public void TakeDamage(int value)
    {
        if (value <= 0)
            return;

        _canvasGroup.alpha = 1f;

        _health -= value;

        _timeSinceChangeHealth = 0f;

        _healthBar.SetHealth(_health);

        if (_health <= 0)
            IvokeDeath();
    }

    private void IvokeDeath()
    {
        Debug.Log("death");
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && _timeSinceLastAttack >= _attackSpeed)
        {
            var enemy = collision.gameObject.GetComponent<Enemy>();
            _animator.SetTrigger("Attack");
            enemy.TakeDamage(_damage);
            Vibration.Vibrate(1.5f, 0.0f);
            _toolSwitch.SwitchState(InstrumentalState.Sword);
            _timeSinceLastAttack = 0f;
        }
    }
}
