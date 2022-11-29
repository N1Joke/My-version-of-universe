using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extractable : MonoBehaviour
{
    [SerializeField] private ResourceType _resource = ResourceType.Tree;
    [SerializeField] private InstrumentalState _miningTool;
    [SerializeField] private int _resourceHealth = 3;
    [SerializeField] private float _respawnDelayExtractable = 4;
    [SerializeField] private GameObject _meshObj;
    [SerializeField] private BoxCollider[] _colliders;

    [Header("Animation taking damage")]
    [SerializeField] private float _shakeTime = 0.5f;
    [SerializeField] private Vector3 _shakeValue = new Vector3(0.025f, 0f, 0.025f);
    [SerializeField] private int _shakeCount = 10;
    [Header("Animation respawn")]
    [SerializeField] private float _animTime = 0.5f;

    private int _startHealth;
    public InstrumentalState MiningTool => _miningTool;

    public ResourceType Resource => _resource;

    public bool Destructed { private set; get; }

    private void Start()
    {
        _startHealth = _resourceHealth;
    }

    public bool ApplyDamage(int damage)
    {
        if (damage <= 0)
            return false;

        _resourceHealth -= damage;

        if (_resourceHealth <= 0)
        {
            VisualiseDestruction();
            
            return true;
        }

        VisualiseDamage();
        return false;
    }

    protected virtual void VisualiseDestruction()
    {
        Destructed = true;
        _meshObj.SetActive(false);
        foreach (var col in _colliders)
        {
            col.enabled = false;
        }

        StartCoroutine(RespawnExtractable());
    }

    private IEnumerator RespawnExtractable()
    {
        yield return new WaitForSeconds(_respawnDelayExtractable);
        _meshObj.SetActive(true);
        var baseScale = _meshObj.transform.localScale;
        _meshObj.transform.localScale = Vector3.zero;
        LeanTween.scale(_meshObj,baseScale, _animTime).setEaseInOutBounce();
        yield return new WaitForSeconds(_animTime + 0.01f);
        Destructed = false;
        _resourceHealth = _startHealth;
        foreach (var col in _colliders)
        {
            col.enabled = true;
        }
    }

    private void VisualiseDamage()
    {
        Shake(_shakeTime, _shakeCount);
    }

    private void Shake(float time, int shakeCount)
    {
        ShakeX(shakeCount, time / shakeCount);
    }

    private void ShakeX(int shakeCount, float time, bool forward = true)
    {
        if (shakeCount <= 0)
            return;
        shakeCount -= 1;
        forward = !forward;
        LeanTween.moveX(_meshObj, _meshObj.transform.position.x + (_shakeValue.x * (forward ? 1 : -1)), time).setOnComplete(() =>
        {            
            ShakeZ(shakeCount, time, forward);
        });
    }

    private void ShakeZ(int shakeCount, float time, bool forward = true)
    {
        if (shakeCount <= 0)
            return;
        shakeCount -= 1;        
        LeanTween.moveZ(_meshObj, _meshObj.transform.position.z + (_shakeValue.z * (forward ? 1 : -1)), time).setOnComplete(() =>
        {
            ShakeX(shakeCount, time, forward);
        });
    }

    private void ShakeY(int shakeCount, float time)
    {
        if (shakeCount <= 0)
            return;
        shakeCount -= 1;
        LeanTween.moveY(_meshObj, _meshObj.transform.position.y + _shakeValue.y, time).setOnComplete(() =>
        {
            ShakeX(shakeCount, time);
        });
    }
}