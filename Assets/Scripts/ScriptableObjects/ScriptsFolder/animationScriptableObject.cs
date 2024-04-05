using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu]
public class animationScriptableObject : ScriptableObject
{
    public float jumpHeight = 5f;  // Altura del salto
    public float jumpDuration = 1f;


    public void Jump(GameObject _gameObject)
    {
        float _originalTransform = _gameObject.transform.position.y;
        // Mueve el GameObject hacia arriba y luego hacia abajo para crear el efecto de salto
        LeanTween.moveY(_gameObject, _originalTransform + jumpHeight, jumpDuration / 2)
            .setEase(LeanTweenType.easeOutQuad)  // Suaviza la subida
            .setOnComplete(() =>
            {
                // Mueve el GameObject hacia abajo después de alcanzar la altura máxima
                LeanTween.moveY(_gameObject, _originalTransform, jumpDuration / 2)
                    .setEase(LeanTweenType.easeInQuad);  // Suaviza la caída
            });
    }
}
