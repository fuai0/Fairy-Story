using UnityEngine;
using UnityEngine.EventSystems;

public class StatToolTip_UI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject statTip;

    public void OnPointerEnter(PointerEventData eventData)
    {
        statTip.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        statTip.SetActive(false);
    }
}
