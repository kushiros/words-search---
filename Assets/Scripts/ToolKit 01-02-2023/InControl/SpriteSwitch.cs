using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InControl
{
    public class SpriteSwitch : MonoBehaviour
    {
        public SpriteRenderer spr;
        public Sprite[] sprites;
        int currentIndex;
        public bool loop = true;
        public void NextSprite()
        {
            currentIndex++;
            fixCurrentIndex();
            SetSpriteByIndex(currentIndex);
        }
        public void PrevSprite()
        {
            currentIndex--;
            fixCurrentIndex();
            SetSpriteByIndex(currentIndex);
        }

        protected void fixCurrentIndex()
        {
            if (currentIndex > sprites.Length - 1)
            {
                currentIndex = 0;
                if (!loop) currentIndex = sprites.Length - 1;
            }
            if (currentIndex < 0)
            {
                if (!loop) currentIndex = 0;
                currentIndex = sprites.Length - 1;
            }
        }

        public void SetSpriteByIndex(int index)
        {
            spr.sprite = sprites[index];
        }
    }
}