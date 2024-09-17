using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class TimerSystem<TTimerFlag> : IEcsRunSystem where TTimerFlag : struct
    {
        private EcsFilter<Timer<TTimerFlag>> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref var timer = ref _filter.Get1(idx);
                timer.Value -= Time.deltaTime;
                if (timer.Value <= 0)
                {
                    _filter.GetEntity(idx).Get<TimerDoneEvent<TTimerFlag>>();
                    _filter.GetEntity(idx).Del<Timer<TTimerFlag>>();
                }
            }
        }
    }
}