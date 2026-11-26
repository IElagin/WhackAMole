using System.Text;
using _Project.Develop.Runtime.Meta.Features.Economy;
using UnityEngine;

namespace _Project.Develop.Runtime.App.MainMenu
{
    public sealed class MainMenuScreen
    {
        private readonly WalletService _wallet;
        private readonly LevelSelector _levelSelector;
        private string _status;

        public MainMenuScreen(WalletService wallet, LevelSelector levelSelector)
        {
            _wallet = wallet;
            _levelSelector = levelSelector;
        }

        public void PrepareStatus()
        {
            int levelCount = _levelSelector.LevelCount;

            StringBuilder builder = new();

            builder.AppendLine("Главное меню");
            builder.AppendLine($"Золото: {_wallet.Get(CurrencyId.Gold)}");

            for (int level = 1; level <= levelCount; level++)
            {
                string state = _levelSelector.IsOpen(level) ? "открыт" : "закрыт";
                builder.AppendLine($"Уровень {level} — {state}");
            }

            builder.Append($"Клавиши 1…{levelCount} — выбрать уровень");

            _status = builder.ToString();
        }

        public void ShowStatus() => Debug.Log(_status);

        public void ShowLevelLocked(int levelNumber)
        {
            Debug.Log($"Уровень {levelNumber} закрыт: сначала пройдите предыдущий");
        }
    }
}
