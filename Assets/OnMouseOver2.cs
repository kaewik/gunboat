using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouseOver2 : MonoBehaviour
{
    Color m_ColorMultiplier = new Vector4(0.3f, 0.3f, 0.3f, 1f);
    Texture2D m_OriginalTexture;
    SpriteRenderer m_Renderer;
    PolygonCollider2D m_Collider;

    void Start()
    {
        m_Renderer = GetComponent<SpriteRenderer>();
        m_Collider = GetComponent<PolygonCollider2D>();
        m_OriginalTexture = new Texture2D(m_Renderer.sprite.texture.width, m_Renderer.sprite.texture.height);
        m_OriginalTexture.SetPixels(m_Renderer.sprite.texture.GetPixels());
        m_OriginalTexture.Apply();
    }

    void OnMouseEnter()
    {
        var spriteTexture = m_Renderer.sprite.texture;
        var spriteX = m_Renderer.sprite.texture.width;
        var spriteY = m_Renderer.sprite.texture.height;
        for (var x = 0; x < spriteX; x++) {
            for (var y = 0; y < spriteY; y++) {
                var localPosition2D = new Vector2(m_Renderer.transform.localPosition.x, m_Renderer.transform.localPosition.y);
                var localPosition = localPosition2D + new Vector2(x / m_Renderer.sprite.pixelsPerUnit, y / m_Renderer.sprite.pixelsPerUnit);
                var closestPoint = m_Collider.ClosestPoint(localPosition);
                if (closestPoint == localPosition) {
                    var highlightColor = ClampColor(spriteTexture.GetPixel(x, y) * m_ColorMultiplier);
                    spriteTexture.SetPixel(x, y, Color.red);
                    Debug.Log($"spriteTexture.SetPixel({x}, {y}, {highlightColor})");
                }
            }
        }
        spriteTexture.Apply();
    }

    void OnMouseExit()
    {
        m_Renderer.sprite.texture.SetPixels(m_OriginalTexture.GetPixels());
        m_Renderer.sprite.texture.Apply();
    }

    private Color ClampColor(Vector4 rgbaColor) {
        var minColor = new Vector4(1f, 1f, 1f, 1f);
        return Vector4.Min(minColor, rgbaColor);
    }
}
