using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    [SerializeField] private UiService _uiService;
    [SerializeField] private DataManager _dataManager;
    [SerializeField] private LoadSceneController _sceneController;

    public override void InstallBindings()
    {
        DontDestroyOnLoad(this);

        //initialisations

        //UI
        var uiInstance = Container.InstantiatePrefabForComponent<UiService>(_uiService);
        Container.Bind<UiService>().FromInstance(uiInstance).AsSingle();

        //Data
        var dataManagerInstance = Container.InstantiatePrefabForComponent<DataManager>(_dataManager);
        Container.Bind<DataManager>().FromInstance(dataManagerInstance).AsSingle();

        //Scene controller
        var sceneControllerInstance = Container.InstantiatePrefabForComponent<LoadSceneController>(_sceneController);
        Container.Bind<LoadSceneController>().FromInstance(sceneControllerInstance).AsSingle();
    }
}
