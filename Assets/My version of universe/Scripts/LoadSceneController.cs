using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class LoadSceneController : MonoBehaviour
{
    [SerializeField] private int _currentSceneIndex = 1;

    public static LoadSceneController Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;        
    }

    private void Start()
    {
        LoadScene();
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(_currentSceneIndex);
    }
}
