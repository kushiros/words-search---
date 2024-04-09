using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace InControl
{
    public abstract class LeanAnimation : ToolsParent
    {
        public float animTime;
      //  public LeanTweenType animEaseType;
        [BoundedCurve(1,0,0,1,1)] public AnimationCurve easeCurve;
        public bool ignoreScaledTime = false;
        public Events animEvents;

        LTDescr anim;
        [Serializable]
        public struct Events
        {
            public UnityEvent OnAnimationStart;
            public UnityEvent OnAnimationFinish;
        }

        protected override void _OnRunAction()
        {
            if (!enabled || !gameObject.activeInHierarchy) return;
            animEvents.OnAnimationStart?.Invoke();
            anim = LeanTween.value(gameObject, UpdateAnimation, 0, 1, animTime).setEase( easeCurve).setOnComplete(OnAnimationFinished).setIgnoreTimeScale(ignoreScaledTime);
        }

        public void _PauseAnimation()
        {
            anim.pause();
        }

        protected virtual void OnAnimationFinished()
        {
            animEvents.OnAnimationFinish ?. Invoke();
        }

        public void SetAnimTime(float newTime)
        {
            animTime = newTime;
        }

        public abstract void UpdateAnimation(float percentage);
    }
}