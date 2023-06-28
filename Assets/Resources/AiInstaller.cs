using Enemy;
using UnityEngine;
using Zenject;

public class AiInstaller : MonoInstaller
{
    [SerializeField] private AIManager aiManager;
    public override void InstallBindings()
    {
        Container.Bind<AIManager>()
            .FromInstance(aiManager)
            .AsSingle()
            .NonLazy();
    }
}