using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class PopViewSystem : IEcsRunSystem
    {
        private GameData _gameData;
        private GameUI _gameUi;
        private EcsWorld _world;

        private EcsFilter<Pop, ChangePopViewRequest> _filter;
        private EcsFilter<PopcornViewProvider, RandomizePopRotationViewRequest> _rotationfilter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                ref PopcornViewProvider popView = ref entity.Get<PopcornViewProvider>();
                ref ChangePopViewRequest changePopViewRequest = ref entity.Get<ChangePopViewRequest>();

                if (changePopViewRequest.PopBody ==  StaticData.PopBody.Popcorn)
                {
                    popView.Bodies[(int)StaticData.PopBody.Popcorn].SetActive(true);
                    popView.Bodies[(int)StaticData.PopBody.Skeleton].SetActive(true);
                    popView.Bodies[(int)StaticData.PopBody.RawCorn].SetActive(false);
                }

                if (changePopViewRequest.PopBody == StaticData.PopBody.PopcornWithoutLimbs)
                {
                    popView.Bodies[(int)StaticData.PopBody.Popcorn].SetActive(true);
                    popView.Bodies[(int)StaticData.PopBody.Skeleton].SetActive(false);
                    popView.Bodies[(int)StaticData.PopBody.RawCorn].SetActive(false);
                }

                if (changePopViewRequest.PopBody == StaticData.PopBody.RawCorn)
                {
                    popView.Bodies[(int)StaticData.PopBody.Popcorn].SetActive(false);
                    popView.Bodies[(int)StaticData.PopBody.Skeleton].SetActive(false);
                    popView.Bodies[(int)StaticData.PopBody.RawCorn].SetActive(true);
                }

                entity.Del<ChangePopViewRequest>();
            }

            foreach (var idx in _rotationfilter)
            {
                ref EcsEntity entity = ref _rotationfilter.GetEntity(idx);
                
                _rotationfilter.Get1(idx).Bodies[(int)StaticData.PopBody.Popcorn].transform.Rotate(new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f)), Random.Range(0, 360.0f));

                entity.Del<RandomizePopRotationViewRequest>();
            }
        }
    }
}