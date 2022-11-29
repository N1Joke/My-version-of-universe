using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerExtractor _playerExtractor;
    [SerializeField] private PlayerFightSystem _playerFightSystem;
    [SerializeField] private ToolSwitch _toolSwitch;
    [SerializeField] private Rigidbody _rigidbody;

    public PlayerMovement PlayerMovement => _playerMovement;
    public PlayerExtractor PlayerExtractor => _playerExtractor;
    public PlayerFightSystem PlayerFightSystem => _playerFightSystem;
    public ToolSwitch ToolSwitch => _toolSwitch;
    public Rigidbody TargetRigitbody => _rigidbody;

    [Inject]
    public void Construct()
    {
        _playerExtractor.Construct(UiService.Instance);
        _playerMovement.Construct(UiService.Instance.TouchController);
    }
}
