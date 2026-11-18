using _Project.Develop.Runtime.Gameplay.Levels;
using _Project.Develop.Runtime.Meta.Features.Economy;
using _Project.Develop.Runtime.Meta.Features.Progress;

namespace _Project.Develop.Runtime.Gameplay.Run
{
    public sealed class RunFinisher
    {
        private readonly WalletService _wallet;
        private readonly PlayerProgressService _playerProgress;
        private readonly CurrentLevel _currentLevel;
        private readonly GameplayConsole _console;

        public RunFinisher(
            WalletService wallet,
            PlayerProgressService playerProgress,
            CurrentLevel currentLevel,
            GameplayConsole console)
        {
            _wallet = wallet;
            _playerProgress = playerProgress;
            _currentLevel = currentLevel;
            _console = console;
        }

        public void Win(int runGold)
        {
            if (runGold > 0)
                _wallet.Add(CurrencyId.Gold, runGold);

            _playerProgress.CompleteLevel(_currentLevel.Number);

            _console.Victory(_currentLevel.Number, runGold, _wallet.Get(CurrencyId.Gold));
        }

        public void Lose(int runGold)
        {
            _console.Defeat(runGold);
        }
    }
}
