using UnityEngine;

namespace _Project.Develop.Runtime.Gameplay
{
    public sealed class GameplayConsole
    {
        public void RunStarted(int level, int requiredHits, int lives)
        {
            Debug.Log(
                $"Уровень {level}: нужно попаданий {requiredHits}, жизней {lives}\n" +
                "Клавиши 1–9 — лунки слева направо, сверху вниз");
        }

        public void MoleAppeared(int holeNumber)
        {
            Debug.Log($"Крот в лунке {holeNumber}");
        }

        public void Hit(int hits, int required, int runGold)
        {
            Debug.Log($"Попадание {hits}/{required}, золото за забег: {runGold}");
        }

        public void LifeLost(int lives)
        {
            Debug.Log($"Крот ушёл! Осталось жизней: {lives}");
        }

        public void Victory(int level, int runGold, int walletGold)
        {
            Debug.Log(
                $"Победа на уровне {level}! Золото за забег: {runGold}, всего: {walletGold}\n" +
                "Пробел — в меню");
        }

        public void Defeat(int burnedGold)
        {
            Debug.Log(
                $"Поражение. Сгорело золота: {burnedGold}\n" +
                "Пробел — заново, Esc — в меню");
        }
    }
}
