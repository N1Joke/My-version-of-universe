using UnityEngine;
using Zenject;

public class TopDownPlayerInstaller : MonoInstaller
{
    [SerializeField] private Player _player;
    [SerializeField] private Transform _spawnPoint;

    public override void InstallBindings()
    {
        var playerInstance = Container.InstantiatePrefabForComponent<Player>(_player, _spawnPoint.position, Quaternion.identity, null);
        Container.Bind<Player>().FromInstance(_player).AsSingle().WithArguments(UiService.Instance);
        Container.QueueForInject(_player);
    }
}