using UnityEngine;
using UnityEngine.EventSystems;

public class OnMouseUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject obj;

    public void OnPointerEnter(PointerEventData eventData)
    {
        obj.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        obj.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
