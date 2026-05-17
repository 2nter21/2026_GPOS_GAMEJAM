using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class AlertScript : MonoBehaviour
{
    public float stayTime = 0.5f;
    public float fadeTime = 1.0f;

    private TextMeshProUGUI _text;

    private void OnEnable()
    {
        _text = GetComponent<TextMeshProUGUI>();
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(stayTime);

        Color startColor = _text.color;
        float timer = 0f;

        while (timer < fadeTime)
        {
            timer += Time.deltaTime;

            float alpha = Mathf.Lerp(1f, 0f, timer / fadeTime);
            _text.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

            yield return null;
        }

        Destroy(gameObject);
    }
}
