using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouseOver2 : MonoBehaviour {
    Color m_ColorMultiplier = new Vector4(0.3f, 0.3f, 0.3f, 1f);
    Texture2D m_OriginalTexture;
    SpriteRenderer m_Renderer;
    PolygonCollider2D m_Collider;

    void Start() {
        m_Renderer = GetComponent<SpriteRenderer>();
        m_Collider = GetComponent<PolygonCollider2D>();
        m_OriginalTexture = new Texture2D(m_Renderer.sprite.texture.width, m_Renderer.sprite.texture.height);
        m_OriginalTexture.SetPixels(m_Renderer.sprite.texture.GetPixels());
        m_OriginalTexture.Apply();
    }

    void OnMouseEnter() {
        var sprite = m_Renderer.sprite;
        var spriteTexture = sprite.texture;
        var spriteRect = new IntRect(sprite.rect);
        var offsetX = spriteRect.x;
        var offsetY = spriteRect.y;
        var pixelsPerUnit = sprite.pixelsPerUnit;
        var localPosition = new Vector2(m_Renderer.transform.localPosition.x, m_Renderer.transform.localPosition.y);
        for (var x = offsetX; x < offsetX + spriteRect.width; x++) {
            localPosition.x = (x - offsetX) / pixelsPerUnit;
            for (var y = offsetY; y < offsetY + spriteRect.height; y++) {
                localPosition.y = (y - offsetY) / pixelsPerUnit;
                if (m_Collider.OverlapPoint(localPosition)) {
                    var highlightColor = ClampColor(spriteTexture.GetPixel(x, y) * m_ColorMultiplier);
                    spriteTexture.SetPixel(x, y, Color.red);
                }
            }
        }
        spriteTexture.Apply();
    }

    void OnMouseExit() {
        m_Renderer.sprite.texture.SetPixels(m_OriginalTexture.GetPixels());
        m_Renderer.sprite.texture.Apply();
    }

    private Color ClampColor(Vector4 rgbaColor) {
        var minColor = new Vector4(1f, 1f, 1f, 1f);
        return Vector4.Min(minColor, rgbaColor);
    }

    private struct IntRect {
        public IntRect(Rect rect) {
            x = (int) rect.x;
            y = (int) rect.y;
            width = (int) rect.width;
            height = (int) rect.height;
        }
        public int x { get; set; }
        public int y { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }
}
