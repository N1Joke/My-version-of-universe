using UnityEngine;
using Zenject;

public class CameraInstaller : MonoInstaller
{
    [SerializeField] private CameraFollower _cameraFollower;

    public override void InstallBindings()
    {
        Container.Bind<CameraFollower>().FromInstance(_cameraFollower).AsSingle();
        Container.QueueForInject(_cameraFollower);
    }
}