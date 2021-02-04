using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour
{

    public static ScreenFade main;

    public float fadeDuration;
    public AnimationCurve fadeCurve;

    public SpriteRenderer imageRef;
    private Coroutine fadeRoutine;

    private void Awake()
    {
        main = this;
    }

    public void FadeIn()
    {
        if (fadeRoutine != null)
        {
            StopCoroutine(fadeRoutine);
        }

        fadeRoutine = StartCoroutine(FadeRoutine(true));
    }

    public void FadeOut()
    {
        if (fadeRoutine != null)
        {
            StopCoroutine(fadeRoutine);
        }

        fadeRoutine = StartCoroutine(FadeRoutine(false));
    }

    private IEnumerator FadeRoutine(bool isFadingIn)
    {
        float startTime = Time.time;

        imageRef.color = new Color
        (
            imageRef.color.r,
            imageRef.color.g,
            imageRef.color.b,
            isFadingIn ? 1f : 0f
        );

        while (Time.time - startTime < fadeDuration)
        {
            float t = (Time.time - startTime) / fadeDuration;   // 0 -> 1 
            t = fadeCurve.Evaluate(t);

            imageRef.color = new Color
            (
                imageRef.color.r,
                imageRef.color.g,
                imageRef.color.b,
                isFadingIn ? Mathf.Lerp(1f, 0f, t) : Mathf.Lerp(0f, 1f, t)
            );

            yield return null;
        }

        // Finalize.
        imageRef.color = new Color
        (
            imageRef.color.r,
            imageRef.color.g,
            imageRef.color.b,
            isFadingIn ? 0f : 1f
        );

        yield return 1;
    }
}
