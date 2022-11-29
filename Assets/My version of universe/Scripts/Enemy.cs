using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private HealthBar _healthBar;

    private float _timeSinceLastAttack;
    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _healthBar.follow = true;
        _healthBar.SetMinMaxValue(0,_health);
    }

    private void FixedUpdate()
    {
        _timeSinceLastAttack += Time.deltaTime;              
        
        if (_target)
        {
            UpdateDestination();
        }
    }

    private void UpdateDestination()
    {
        _agent.SetDestination(_target.position);
    }

    public void SetTarget(Transform target)
    {
        NavMeshManager.Instance.UpdateNavMeshSurface();        
        _target = target;
    }

    public void TakeDamage(int value)
    {
        if (value <= 0)
            return;

        _health -= value;

        _healthBar.SetHealth(_health);

        if (_health <= 0)
            IvokeDeath();
    }

    private void IvokeDeath()
    {
       Destroy(gameObject);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && _timeSinceLastAttack >= _attackSpeed)
        {
            var fightSystem = collision.gameObject.GetComponent<PlayerFightSystem>();

            fightSystem.TakeDamage(_damage);

            _timeSinceLastAttack = 0f;
        }        
    }
}
