using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHoverTextChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    TMP_Text textMesh;
    Color32 defaultColor = Color.white;
    Color32 mouseOverColor = new Color32(106, 218, 252, 255);
    Color32 mouseDownColor = new Color32(106, 145, 252, 255);
    bool mouseOver = true;
    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<Button>().transform.Find("Text (TMP)").gameObject.GetComponent<TMP_Text>();
        if (textMesh == null)
            Debug.Log("Error - TMP is null");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;
        textMesh.color = mouseOverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
        textMesh.color = defaultColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        textMesh.color = mouseDownColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (mouseOver)
            textMesh.color = mouseOverColor;
        else
            textMesh.color = defaultColor;
    }
}
