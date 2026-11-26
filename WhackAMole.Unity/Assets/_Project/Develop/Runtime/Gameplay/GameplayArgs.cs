using _Project.Develop.Runtime.Infrastructure.SceneManagement;

namespace _Project.Develop.Runtime.Gameplay
{
    public sealed class GameplayArgs : IInputSceneArgs
    {
        public GameplayArgs(int levelNumber)
        {
            LevelNumber = levelNumber;
        }

        public int LevelNumber { get; }
    }
}
