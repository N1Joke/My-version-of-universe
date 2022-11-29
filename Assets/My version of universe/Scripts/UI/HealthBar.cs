using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Gradient _gradient;

    private Slider _slider;
    private Camera _camera;

    public bool follow;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _camera = Camera.main;
    }

    private void FixedUpdate()
    {
        if (follow)
        {
            transform.LookAt(_camera.transform);
        }
    }

    public void SetMinMaxValue(int min, int max)
    {
        _slider.minValue = min;
        _slider.maxValue = max;
    }

    public void SetHealth(int value)
    {
        if (value <= _slider.maxValue && value >= _slider.minValue)
            _slider.value = value;

        //if (_slider.value == _slider.maxValue)
        //hide health bar
    }
}
