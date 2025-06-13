using UnityEngine;
using UnityEngine.EventSystems;

public class ShowImageNextToButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject imageToHide;
    public RectTransform hoverImage; 
    public Vector2 offset = new Vector2(100f, 0f); 

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverImage != null&&imageToHide!=null)
        {
            hoverImage.gameObject.SetActive(true);
            imageToHide.gameObject.SetActive(false);


            RectTransform canvasRect = hoverImage.GetComponentInParent<Canvas>().GetComponent<RectTransform>();

            
            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position);

            
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPoint, Camera.main, out localPoint);

            
            hoverImage.anchoredPosition = localPoint + offset;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (hoverImage != null&&imageToHide!=null)
        {
            hoverImage.gameObject.SetActive(false);
            imageToHide.gameObject.SetActive(true);

        }
    }
}
