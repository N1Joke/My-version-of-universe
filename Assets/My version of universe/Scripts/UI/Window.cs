using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window : MonoBehaviour
{
    [SerializeField] private Button _closeButton;

    private void Start()
    {
        if (_closeButton)
            _closeButton.onClick.AddListener(CloseWindow);
    }

    protected virtual void CloseWindow()
    {
        gameObject.SetActive(false);
    }
}
