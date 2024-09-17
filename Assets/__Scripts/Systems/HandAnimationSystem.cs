using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class HandAnimationSystem : IEcsRunSystem
    {
        private EcsFilter<HandProvider, AnimatorProvider, ChangeHandAnimationRequest> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                ref AnimatorProvider entityAnimator = ref entity.Get<AnimatorProvider>();
                ref ChangeHandAnimationRequest changeAnimationAction = ref entity.Get<ChangeHandAnimationRequest>();
                
                SetAnimation(entityAnimator.Value, changeAnimationAction.Animation);
                entity.Del<ChangeHandAnimationRequest>();
            }
        }

        private void SetAnimation(Animator animator, int animation, int randomAnimation = -1, string animationRandom = null)
        {
            if (animator.GetBool(animation))
                return;
            Utility.ResetAnimtor(animator);

            if (animationRandom != null)
                animator.SetInteger(animationRandom, randomAnimation);

            animator.SetTrigger(animation);
        }
    }
}