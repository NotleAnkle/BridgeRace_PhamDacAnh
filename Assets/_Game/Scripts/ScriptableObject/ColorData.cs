using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorData", menuName = "ScriptableObjects/ColorData", order = 1)]
public class ColorData : ScriptableObject
{
    [SerializeField] Material[] colorMaterials;

    public Material GetColorMaterial(ColorType colorType)
    {
        return colorMaterials[(int)colorType];
    }
}
