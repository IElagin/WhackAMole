using _Project.Develop.Runtime.App.Input;
using _Project.Develop.Runtime.Gameplay.Moles;

namespace _Project.Develop.Runtime.App.Gameplay
{
    public sealed class WhackInput
    {
        private readonly KeyboardInput _input;
        private readonly WhackService _whackService;

        public WhackInput(KeyboardInput input, WhackService whackService)
        {
            _input = input;
            _whackService = whackService;
        }

        public void Tick()
        {
            if (_input.TryGetPressedNumber(out int number) == false)
                return;

            _whackService.TryWhack(number - 1);
        }
    }
}
