using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class PopInitSystem : IEcsRunSystem
    {
        private GameData _gameData;

        private EcsFilter<Pop> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);

            }
        }
    }
}