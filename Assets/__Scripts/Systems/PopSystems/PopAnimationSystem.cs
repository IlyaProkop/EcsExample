using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class PopAnimationSystem : IEcsRunSystem
    {
        private EcsFilter<Pop, AnimatorProvider, ChangePopAnimationRequest> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref EcsEntity entity = ref _filter.GetEntity(idx);
                ref AnimatorProvider entityAnimator = ref entity.Get<AnimatorProvider>();
                ref ChangePopAnimationRequest changeAnimationAction = ref entity.Get<ChangePopAnimationRequest>();

                if (changeAnimationAction.Animation == StaticData.PopAnimations.IsWalking)
                {
                    int walkRandom = Random.Range(0, 2);
                    SetAnimation(entityAnimator.Value, changeAnimationAction.Animation, walkRandom, StaticData.PopAnimations.WalkingIndex);
                }
                else if (changeAnimationAction.Animation == StaticData.PopAnimations.IsJump)
                {
                    int jumpRandom = Random.Range(0, 3);
                    SetAnimation(entityAnimator.Value, changeAnimationAction.Animation, jumpRandom, StaticData.PopAnimations.JumpIndex);
                }
                else
                    SetAnimation(entityAnimator.Value, changeAnimationAction.Animation);

                entity.Del<ChangePopAnimationRequest>();
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