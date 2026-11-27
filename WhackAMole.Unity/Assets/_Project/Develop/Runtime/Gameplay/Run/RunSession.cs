using _Project.Develop.Runtime.Gameplay.Levels;

namespace _Project.Develop.Runtime.Gameplay.Run
{
    public sealed class RunSession
    {
        private readonly CurrentLevel _currentLevel;

        public RunSession(CurrentLevel currentLevel)
        {
            _currentLevel = currentLevel;
        }

        public int Hits { get; private set; }

        public int Lives { get; private set; }

        public int Gold { get; private set; }

        public int RequiredHits => _currentLevel.Config.RequiredHits;

        public bool HasRequiredHits => Hits >= RequiredHits;

        public bool HasLives => Lives > 0;

        public void Reset()
        {
            Hits = 0;
            Gold = 0;
            Lives = _currentLevel.Config.Lives;
        }

        public void AddHit()
        {
            Hits++;
            Gold += _currentLevel.Config.GoldPerHit;
        }

        public void LoseLife() => Lives--;
    }
}
