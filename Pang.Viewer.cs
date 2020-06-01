using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public partial class Pang : MonoBehaviour
{
    private Image image;
    public Tile tile;
    enum ColorType
    {
        Black,
        Blue,
        Green,
        Orange,
        Red,
        White,
        Yellow,
    };

    ColorType colorType;

    private void ViewInit()
    {
        image = GetComponent<Image>();
        randomizeColor();
        drawColor();
    }

    public void randomizeColor()
    {
        var allType = System.Enum.GetValues(typeof(ColorType));
        var rand = Random.Range(0, allType.Length - 1);
        colorType = (ColorType)allType.GetValue(rand);
    }

    public void drawColor()
    {
        switch (colorType)
        {
            case ColorType.Black:
                image.sprite = GameManager.sInstance.pangImages[0];
                break;
            case ColorType.Blue:
                image.sprite = GameManager.sInstance.pangImages[1];
                break;
            case ColorType.Green:
                image.sprite = GameManager.sInstance.pangImages[2];
                break;
            case ColorType.Orange:
                image.sprite = GameManager.sInstance.pangImages[3];
                break;
            case ColorType.Red:
                image.sprite = GameManager.sInstance.pangImages[4];
                break;
            case ColorType.White:
                image.sprite = GameManager.sInstance.pangImages[5];
                break;
            case ColorType.Yellow:
                image.sprite = GameManager.sInstance.pangImages[6];
                break;
        }
    }

    public bool ColorCmp(Pang other)
    {
        return colorType == other.colorType;
    }

    public IEnumerator PangFX(Tile tile)
    {
        var color = image.color;
        for (var count = 100 ; count >= 0; count--)
        {
            image.color = color;
            color.a -= 0.01f;
            yield return null;
        }
        var p = tile.pang;
        color.a = 1;
        image.color = color;
        tile.pang = null;
        if (p) PangPool.sInstance.Push(p);
        GameManager.sInstance.score += 10;
    }
    
}
