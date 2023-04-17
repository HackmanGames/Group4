using UnityEngine;

public class GameColorManager : MonoBehaviour
{
    private Camera cam;
    private ColorScheme colorScheme;
    public SpriteRenderer[] lives;

    private void Awake()
    {
        cam = Camera.main;
        colorScheme = Values.activeColorSceme;

        cam.backgroundColor = colorScheme.backgroundColor;

        foreach(SpriteRenderer sprite in lives)
        {
            sprite.color = colorScheme.textHilightColor;
        }
    }

    public void LooseLife(int _lives)
    {
        int livesLost = 6 - _lives;
        for (int i = 0; i < livesLost; i++)
        {
            lives[i].color = colorScheme.textColor;
        }
    }
}
