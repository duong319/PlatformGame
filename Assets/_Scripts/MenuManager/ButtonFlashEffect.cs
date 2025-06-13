using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonFlashEffect : MonoBehaviour, IPointerClickHandler
{
    public Image buttonImage;
    public Color flashColor = Color.white;
    public float flashDuration = 0.1f;
    public bool disableCursorOnClick = true;

    private Color originalColor;

    void Start()
    {
        if (buttonImage == null)
            buttonImage = GetComponent<Image>();

        originalColor = Color.white;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(FlashEffect());
        if (disableCursorOnClick)
        {
            DisableCursor();
        }

    }

    IEnumerator FlashEffect()
    {
        int count = 3;
        for (int i = 0; i < count; i++)
        {
            buttonImage.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            buttonImage.color = originalColor;
            yield return new WaitForSeconds(flashDuration);
        }
    }

    void DisableCursor()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Invoke("EnableCursor", 2);

    }

    public void EnableCursor()
    {

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
