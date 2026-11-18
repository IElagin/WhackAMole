using UnityEngine;
using VContainer;

namespace _Project.Develop.Runtime.Gameplay.Moles
{
    public sealed class Mole : MonoBehaviour
    {
        private GameplayConsole _console;
        private int _holeNumber;

        public bool IsVisible => gameObject.activeSelf;

        [Inject]
        private void Construct(GameplayConsole console)
        {
            _console = console;
        }

        public void Setup(int holeNumber)
        {
            _holeNumber = holeNumber;

            Hide();
        }

        public void Show()
        {
            gameObject.SetActive(true);

            _console.MoleAppeared(_holeNumber);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
