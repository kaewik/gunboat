using System.Collections;
using UnityEngine;

public class OnMouseOverBrightness : MonoBehaviour
{
    Color m_ColorMultiplier = new Vector4(1.3f, 1.3f, 1.3f, 1f);
    Color m_OriginalColor;
    SpriteRenderer m_Renderer;

    void Start()
    {
        m_Renderer = GetComponent<SpriteRenderer>();
        m_OriginalColor = m_Renderer.material.color;
        Debug.Log($"OriginalColor: {m_OriginalColor}");
    }

    void OnMouseOver()
    {
        m_Renderer.material.color = m_OriginalColor * m_ColorMultiplier;
    }

    void OnMouseExit()
    {
        m_Renderer.material.color = m_OriginalColor;
    }
}
