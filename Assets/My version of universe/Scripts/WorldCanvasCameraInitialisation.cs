using UnityEngine;

public class WorldCanvasCameraInitialisation : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;

    private void Awake()
    {
        _canvas.worldCamera = Camera.main;
    }
}

