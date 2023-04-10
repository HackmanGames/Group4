using UnityEngine;
using UnityEngine.UI;

public class ColorsManager : MonoBehaviour
{
    //color scemes
    public ColorScheme[] colorScemes;
    private int currentColorScheme;

    //affeted Refrences
    private Camera cam;
    private Sprite[] lives;

    public Sprite Logo;
    public Sprite progressBar;
    public Sprite diceSlot;
    public Image[] buttonImages;

    private void Awake()
    {
        cam = Camera.main;
        lives = GetComponent<GameplayManager>().lives;
        SetRandomColorSceme();
    }

    void SetRandomColorSceme()
    {
        currentColorScheme = Mathf.FloorToInt(Random.Range(0, colorScemes.Length));

        UpdateColorScheme();
    }

    void NextColorSceme()
    {
        currentColorScheme++;
        if (currentColorScheme >= colorScemes.Length)
            currentColorScheme = 0;

        UpdateColorScheme();
    }

    void UpdateColorScheme()
    {
        cam.backgroundColor = colorScemes[currentColorScheme].backgroundColor;

        for (int i = 0; i < 5; i++)
        {
            buttonImages[i].color = GetCurrentColorScheme().dieColors[i];
        }
    }

    public ColorScheme GetCurrentColorScheme()
    {
        return colorScemes[currentColorScheme];
    }
}
