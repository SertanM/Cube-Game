using CubeGame.Inputs;
using CubeGame.Player;
using Zenject;

namespace CubeGame
{
    public sealed class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<InputManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerManager>().FromComponentInHierarchy().AsSingle();
        }
    }
}