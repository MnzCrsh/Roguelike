//using Player;
//using UnityEngine;
//using Zenject;

//public class ControllerInstaller : MonoInstaller
//{
//    [SerializeField] private PlayerController _player;

//    public override void InstallBindings()
//    {
//        BindController();
//    }

//    private void BindController()
//    {
//        Container.Bind<PlayerController>()
//                 .FromInstance(_player)
//                 .AsTransient();
//    }
//}