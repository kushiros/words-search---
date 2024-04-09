using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace visualization
{
    [RequireComponent(typeof(Image))]
    public class HandUI : MonoBehaviour
    {
        private Image hand;

        public KeyCode button = KeyCode.Mouse0;
        public Vector2 offset;
        public Sprite idle;
        public Sprite click;
        RectTransform handTransform;

        public UnityEvent OnClick;
        public UnityEvent OnHoldClick;
        public UnityEvent OnReleaseClick;

        public CursorControls cursorControls;
        [Serializable]
        public struct CursorControls
        {
            public CursorControls( KeyCode _buttonToShowHand, bool _isToggle = true)
            {
                isToggle = _isToggle;
                buttonToShowHand = _buttonToShowHand;
            }
            public KeyCode buttonToShowHand;
            public bool isToggle;
        }

        public AnimationControls cursorAnimation;
        [Serializable]
        public struct AnimationControls
        {
            public bool smoothMovement;
            public float speed;
        }

        void Start()
        {
            hand = GetComponent<Image>();
            hand.sprite = idle;
            handTransform = GetComponent<RectTransform>();
            handTransform.anchoredPosition = Input.mousePosition + new Vector3(offset.x, offset.y);
        }

            void Update()
        {
            UpdatePosition();

            if (!cursorControls.isToggle) hand.enabled = Input.GetKeyDown(cursorControls.buttonToShowHand);
            else if (Input.GetKeyDown(cursorControls.buttonToShowHand))
            {
                hand.enabled = !hand.enabled;
            }
            
            if (Input.GetKeyDown(button))
            {
                OnClick?.Invoke();
                hand.sprite = click;
            }
            if (Input.GetKey(button))
            {
                OnHoldClick?.Invoke();
            }
            else if (Input.GetKeyUp(button))
            {
                OnReleaseClick?.Invoke();
                hand.sprite = idle;
            }
        }

        public void UpdatePosition()
        {
            Vector3 objectivePosition = Input.mousePosition + new Vector3(offset.x, offset.y);
            if (!cursorAnimation.smoothMovement) handTransform.anchoredPosition = objectivePosition;
            else handTransform.anchoredPosition = Vector3.Lerp(handTransform.anchoredPosition, objectivePosition, Time.deltaTime * cursorAnimation.speed);
        }
    }
}