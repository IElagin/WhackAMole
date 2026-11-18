using UnityEngine.InputSystem;

namespace _Project.Develop.Runtime.App.Input
{
    public sealed class KeyboardInput
    {
        private static readonly Key[] DigitKeys =
        {
            Key.Digit1, Key.Digit2, Key.Digit3, Key.Digit4, Key.Digit5,
            Key.Digit6, Key.Digit7, Key.Digit8, Key.Digit9,
        };

        private static readonly Key[] NumpadKeys =
        {
            Key.Numpad1, Key.Numpad2, Key.Numpad3, Key.Numpad4, Key.Numpad5,
            Key.Numpad6, Key.Numpad7, Key.Numpad8, Key.Numpad9,
        };

        public bool TryGetPressedNumber(out int number)
        {
            number = 0;

            Keyboard keyboard = Keyboard.current;

            if (keyboard == null)
                return false;

            for (int i = 0; i < DigitKeys.Length; i++)
            {
                if (keyboard[DigitKeys[i]].wasPressedThisFrame || keyboard[NumpadKeys[i]].wasPressedThisFrame)
                {
                    number = i + 1;
                    return true;
                }
            }

            return false;
        }

        public bool WasConfirmPressed()
        {
            Keyboard keyboard = Keyboard.current;

            return keyboard != null && keyboard.spaceKey.wasPressedThisFrame;
        }

        public bool WasBackPressed()
        {
            Keyboard keyboard = Keyboard.current;

            return keyboard != null && keyboard.escapeKey.wasPressedThisFrame;
        }
    }
}
