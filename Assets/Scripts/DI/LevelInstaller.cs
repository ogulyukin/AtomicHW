using System;
using Core;
using Player;
using UI;
using UnityEngine;
using Zenject;
using Zomby;

namespace DI
{
    public sealed class LevelInstaller : MonoInstaller
    {
        [SerializeField] private PlayerModel playerModel;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private ZombySpawnSystemConfig zombySpawnSystemConfig;
        [SerializeField] private EndGamePopUpView endGamePopUpView;

        public override void InstallBindings()
        {
            Container.Bind<PlayerModel>().FromInstance(playerModel).AsSingle();
            Container.BindInterfacesAndSelfTo<InputManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInputController>().AsSingle();
            Container.Bind<Camera>().FromInstance(mainCamera).AsSingle();
            Container.BindInterfacesAndSelfTo<CameraController>().AsSingle();
            Container.Bind<ZombySpawnSystemConfig>().FromInstance(zombySpawnSystemConfig).AsSingle();
            Container.BindInterfacesAndSelfTo<ZombySpawnSystem>().AsSingle();
            ViewsBinding();
        }

        private void ViewsBinding()
        {
            Container.Bind<UITextView>().FromComponentsInHierarchy().AsCached();
            Container.BindInterfacesAndSelfTo<GameInfoController>().AsSingle();
            Container.Bind<EndGamePopUpView>().FromInstance(endGamePopUpView).AsSingle();
            Container.BindInterfacesAndSelfTo<EndGamePopUpController>().AsSingle();
        }
    }
}