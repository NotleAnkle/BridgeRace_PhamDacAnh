using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorType
{
    None = 0,
    Gray = 1, 
    Red = 2,
    Blue = 3, 
    Green = 4, 
    Yellow = 5, 
    Orange = 6, 
    Brown  = 7, 
    Violet = 8,
}
public class ColorObject : GameUnit
{
    public ColorType colorType;

    [SerializeField] private ColorData colorData;
    [SerializeField] private Renderer render;
    
    public void ChangeColor(ColorType colorType)
    {
        this.colorType = colorType; 
        render.material = colorData.GetColorMaterial(colorType);
    }
}
