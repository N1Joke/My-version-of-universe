using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExtractor : MonoBehaviour
{
    [SerializeField] private Animator _animator;   
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _attackSpeed = 0.75f;
    [SerializeField] private ToolSwitch _toolSwitch;

    private List<Extractable> _collidedExtractables = new List<Extractable>();

    private float _timeSinceLastAttack = 0f;
    private bool _isAttacking = false;
    private UiService _uIManager;

    private void FixedUpdate()
    {
        if (_timeSinceLastAttack >= _attackSpeed)
        {
            UpdateDamageToExtractables();            
            _timeSinceLastAttack = 0f;
        }

        _timeSinceLastAttack += Time.fixedDeltaTime;
    }

    public void Construct(UiService uiService)
    {
        _uIManager = uiService;       
    }

    private void UpdateDamageToExtractables()
    {
        if (_collidedExtractables.Count == 0)
        {
            if (_isAttacking)
            {
                _animator.SetBool("Attacking", false);
                _isAttacking = false;
            }
            _toolSwitch.SwitchState(InstrumentalState.None);
            return;
        }

        if (!_isAttacking)
        {
            _animator.SetBool("Attacking", true);
            _isAttacking = true;
        }

        Vibration.Vibrate(1.5f, 0.0f);

        _toolSwitch.SwitchState(_collidedExtractables[0].MiningTool);

        for (int i = _collidedExtractables.Count - 1; i >= 0; i--)
        {
            DataManager.Instance.ChangeResourceCount(_collidedExtractables[i].Resource, _damage);

            if (_collidedExtractables[i].ApplyDamage(_damage))
            {                
                _collidedExtractables.Remove(_collidedExtractables[i]);                
            }            
        }

        if (_collidedExtractables.Count == 0)
        {
            _animator.SetBool("Attacking", false);
            _isAttacking = false;
            _toolSwitch.SwitchState(InstrumentalState.None);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Extractable extractable))
        {
            if (!_collidedExtractables.Contains(extractable) && !extractable.Destructed)
                _collidedExtractables.Add(extractable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Extractable extractable))
        {
            if (_collidedExtractables.Contains(extractable) && !extractable.Destructed)
                _collidedExtractables.Remove(extractable);
        }
    }
}
