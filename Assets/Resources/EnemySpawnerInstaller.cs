using RoomEvents;
using UnityEngine;
using Zenject;

public class EnemySpawnerInstaller : MonoInstaller
{
    [SerializeField] private EnemySpawner enemySpawner;
    public override void InstallBindings()
    {
        Container.Bind<EnemySpawner>()
            .FromInstance(enemySpawner)
            .AsSingle()
            .NonLazy();
    }
}