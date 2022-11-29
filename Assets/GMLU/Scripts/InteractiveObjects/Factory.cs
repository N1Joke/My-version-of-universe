using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Factory : MonoBehaviour
{
    [SerializeField] private ResourceType _inputResource;
    [SerializeField] private int _inputCount;
    [SerializeField] private ResourceType _outputResource;
    [SerializeField] private int _outputCount;
    [SerializeField] private Button _produceButton;
    [SerializeField] private GameObject _canvas;

    private void Start()
    {
        _produceButton.onClick.AddListener(Produce);
        _canvas.GetComponent<Canvas>().worldCamera = Camera.main;
        _canvas.SetActive(false);
    }

    private void Produce()
    {
        if (DataManager.Instance.ChangeResourceCount(_inputResource, -_inputCount))
            DataManager.Instance.ChangeResourceCount(_outputResource, _outputCount);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            _canvas.SetActive(true);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            _canvas.SetActive(false);
    }
}
