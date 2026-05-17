using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnMouseUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Show UI")]
    public CanvasGroup UI;
    public float fadeSpeed = 5.0f;
    private bool inFade = false;

    [Header("Size UP")]
    public float sizeupMag = 1.2f;
    public float sizeupSpeed = 5f;

    private Vector3 originScale;
    private Vector3 targetScale;

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = originScale;

        if (UI != null)
        {
            inFade = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = originScale;

        if (UI != null)
        {
            inFade = false;
        }
    }

    void Start()
    {
        originScale = transform.localScale;
        targetScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.Lerp(
            transform.localScale,
            targetScale,
            Time.deltaTime * sizeupSpeed
        );

        if (UI != null)
        {
            if (inFade)
            {
                if (UI.alpha > 1)
                {
                    UI.alpha = 1;
                }
                else
                {
                    UI.alpha += Time.deltaTime * fadeSpeed;
                }
            }
            else
            {
                if (UI.alpha < 0)
                {
                    UI.alpha = 0;
                }
                else
                {
                    UI.alpha -= Time.deltaTime * fadeSpeed;
                }
            }
        }
        
    }
}
