using UnityEngine;
using Zenject;
using Player;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private PlayerBehavior _player;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform playerSpawnPoint;

    public override void InstallBindings()
    {
        BindPlayer();
    }

    private void BindPlayer()
    {
        var playerInstance =
            Container.InstantiatePrefabForComponent<PlayerBehavior>
            (_player, playerSpawnPoint.position, Quaternion.identity, parentTransform: null);

        Container.Bind<PlayerBehavior>()
                 .FromInstance(playerInstance)
                 .AsTransient();
    }
}