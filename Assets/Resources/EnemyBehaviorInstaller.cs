using Enemy;
using UnityEngine;
using Zenject;

public class EnemyBehaviorInstaller : MonoInstaller
{
    [SerializeField] private EnemyBehavior enemy;
    
    public override void InstallBindings()
    {
        Container.Bind<EnemyBehavior>()
            .FromInstance(enemy)
            .AsSingle()
            .NonLazy();
    }
}