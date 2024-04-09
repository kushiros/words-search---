using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeBar : MonoBehaviour
{
    [SerializeField] private GameObject dynamite, dynamiteRestingPlace, barCircle, hand;
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] private SpriteRenderer barFill, barContainer;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private SpriteRenderer explosionBlank;

    [SerializeField] protected UnityEvent OnDynamiteExplode;
    [SerializeField] protected UnityEvent OnDynamiteAboutToExplode;

    [SerializeField] private int numberTotalSplit = 3;
    [SerializeField]  private int numberToSplit = 3;
    private float initialBarWidth;

    private LTDescr tween;
    private bool hasExploded;

    private void Start()
    {
        initialBarWidth = barFill.size.x; // Almacena el ancho inicial de la barra
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _StartTimeBar();
        }
    }

    public void _StartTimeBar()
    {
        if (hasExploded) return;
        StartCoroutine(StartTimeBar());
        hasExploded = true;
    }

    private IEnumerator StartTimeBar()
    {
        tween = LeanTween.scale(dynamite, dynamite.transform.localScale * 1.2f, .5f).setLoopPingPong().setOnUpdate((float _) =>
        {
            tween.setTime(Mathf.Clamp(tween.time - Time.deltaTime * .013f, Mathf.Epsilon, float.MaxValue));
        });

        while (numberToSplit > 3)
        {
            yield return new WaitForSeconds(1f);
            UpdateBarFill();
        }

        ExplodeDynamite();
    }

    public void ResetBarFill(int amount)
    {
        Debug.Log(numberToSplit + "" + amount +"" +(numberTotalSplit+amount));
        if ((numberToSplit+2) > numberTotalSplit)
        {
            numberToSplit = numberTotalSplit;
        }
        else numberToSplit += (amount);

        //UpdateBarFill();
    }

    private void UpdateBarFill()
    {
        float newWidth = initialBarWidth * numberToSplit / numberTotalSplit;
        LeanTween.value(barFill.gameObject, barFill.size.x, newWidth, 1f)
            .setOnUpdate((float value) =>
            {
                barFill.size = new Vector2(value, barFill.size.y);
            });

        numberToSplit--;

        ColorLerp();
    }

    private void ExplodeDynamite()
    {
        LeanTween.cancel(dynamite);
        OnDynamiteAboutToExplode?.Invoke();
        LeanTween.move(dynamite, dynamiteRestingPlace.transform.position, 1f).setEaseOutCubic();
        LeanTween.scale(dynamite, dynamite.transform.localScale * 1.5f, 1f);
        LeanTween.scale(barCircle, Vector3.zero, 1f);
        LeanTween.value(barContainer.size.x, 0f, .5f).setOnUpdate((float x) =>
        {
            barContainer.size = new Vector2(x, barContainer.size.y);
        });
        StartCoroutine(ExplosionSequence());
    }

    private IEnumerator ExplosionSequence()
    {
        yield return new WaitForSeconds(1f);
        LeanTween.rotate(dynamite, new Vector3(0f, 0f, 70f), .05f).setLoopPingPong(3);
        LeanTween.scale(dynamite, dynamite.transform.localScale * 1.5f, .8f);
        LeanTween.color(dynamite, Color.red, 1f);
        yield return new WaitForSeconds(1f);
        explosion.Play();
        OnDynamiteExplode?.Invoke();
        LeanTween.alpha(explosionBlank.gameObject, 1f, .3f).setLoopPingPong(1);
        dynamite.SetActive(false);
    }
    public void ColorLerp()
    {
        Color startColor = Color.green; // Color cuando numberToSplit es 25
        Color midColor = Color.yellow; // Color cuando numberToSplit es 13
        Color endColor = Color.red; // Color cuando numberToSplit es 0

        // Interpola el color entre verde y amarillo cuando numberToSplit está entre 25 y 13
        if (numberToSplit >= 17)
        {
            float t = (numberTotalSplit - numberToSplit) / 12f; // Normaliza el valor entre 0 y 1
            barFill.color = Color.Lerp(startColor, midColor, t);
        }
        // Interpola el color entre amarillo y rojo cuando numberToSplit está entre 13 y 0
        else
        {
            float t = (17 - numberToSplit) / 17f; // Normaliza el valor entre 0 y 1
            barFill.color = Color.Lerp(midColor, endColor, t);
        }
    }
}
