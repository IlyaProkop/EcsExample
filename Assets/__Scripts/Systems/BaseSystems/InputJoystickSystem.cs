using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class InputJoystickSystem : IEcsInitSystem, IEcsRunSystem
    {
        private GameData _gameData;

        //private FloatingJoystick _joystick;

        public void Init()
        {
            //_joystick = _gameData.SceneData.Joystick;
        }

        public void Run()
        {
            //_gameData.RuntimeData.JoystickXPosition = _joystick.Horizontal;
            //_gameData.RuntimeData.JoystickZPosition = _joystick.Vertical;
            //_gameData.RuntimeData.JoystickPointerDown = _joystick.IsPointerDown;
        }
    }
}