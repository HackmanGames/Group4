using UnityEngine;

[CreateAssetMenu(fileName = "ColorScheme", menuName = "ScriptableObjects/ColorScheme", order = 1)]
public class ColorScheme : ScriptableObject
{

    public Color backgroundColor;
    public Color textColor;
    public Color textHilightColor;

    public Color[] dieColors = new Color[5];

}
