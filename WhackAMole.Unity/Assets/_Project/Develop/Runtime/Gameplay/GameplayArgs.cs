namespace _Project.Develop.Runtime.Gameplay
{
    public sealed class GameplayArgs
    {
        public GameplayArgs(int levelNumber)
        {
            LevelNumber = levelNumber;
        }

        public int LevelNumber { get; }
    }
}
