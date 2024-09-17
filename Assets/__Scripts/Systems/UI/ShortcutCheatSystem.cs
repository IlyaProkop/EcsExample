using System;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class ShortcutCheatSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private WorldGameUI _worldGameUi;
        private EcsWorld _world;

        public void Run()
        {
            if (Input.GetKeyDown(KeyCode.R))
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                    _gameData.ResetData();

            if (Input.GetKeyDown(KeyCode.Alpha1)) Time.timeScale = Math.Abs(Time.timeScale - 1.0f) < 0.01 ? 0.0f : 1.0f;

            if (Input.GetKeyDown(KeyCode.Alpha2)) Time.timeScale = 2.0f;

            if (Input.GetKeyDown(KeyCode.Alpha3)) Time.timeScale = 3.0f;

            if (Input.GetKeyDown(KeyCode.U)) _gameUi.TriggerShowStateAllScreen();
            
            if (Input.GetKeyDown(KeyCode.W)) _worldGameUi.TriggerShowStateAllScreen();
            
            if (Input.GetKeyDown(KeyCode.V))
                for (int i = 1; i < 2; i++)
                    _world.NewEntity().Get<BuyConveyorRequest>().ConveyorId = i;
            
            if (Input.GetKeyDown(KeyCode.B))
                for (int i = 1; i < 4; i++)
                    _world.NewEntity().Get<BuyConveyorRequest>().ConveyorId = i;
            
            if (Input.GetKeyDown(KeyCode.Z))
                _world.NewEntity().Get<TapPopButtonEvent>();
            
            if (Input.GetKeyDown(KeyCode.M))
                _world.NewEntity().Get<EarnMoneyEvent>().Value = 100000;

            if (Input.GetKeyDown(KeyCode.G))
                for (int i = 0; i < 100; i++)
                    _world.NewEntity().Get<AddGoldPopEvent>();
        }
    }
}