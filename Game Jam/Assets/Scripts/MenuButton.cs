using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButton : Button
{
    protected override void Awake()
    {
        m_Image = gameObject.GetComponent< Image >();
        m_Rect = gameObject.GetComponent< RectTransform >();
        m_OriginalSize = m_Rect.sizeDelta;
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        m_Rect.sizeDelta = m_OriginalSize * 1.3f;
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        m_Rect.sizeDelta = m_OriginalSize;
    }

    private Image m_Image;
    private RectTransform m_Rect;
    private Vector2 m_OriginalSize;
}
