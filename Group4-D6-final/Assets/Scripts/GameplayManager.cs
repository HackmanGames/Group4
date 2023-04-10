using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public Die activeDie;

    public Sprite[] lives;

    private int currentScore;
    private int currentChain;
    private int currentHighestChain;
    private int currentLives;

    public void StartGame()
    {
        Score.gamesPlayed ++;
        currentScore = 0;
        currentChain = 0;
        currentHighestChain = 0;
    }

    public bool CompareDie(Die dieToCompare)
    {
        if(dieToCompare.dieColor == activeDie.dieColor || activeDie == null)
        {
            activeDie = dieToCompare;
            currentChain++;
            currentScore++;
            return true;
        }else if (dieToCompare.dieNumber > activeDie.dieNumber || dieToCompare.dieNumber == 6 && activeDie.dieNumber == 1)
        {
            activeDie = dieToCompare;
            currentChain++;
            currentScore++;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void LooseLife()
    {
        if (currentChain > currentHighestChain)
            currentHighestChain = currentChain;
        currentChain = 0;
        if(currentLives <= -1)
        {
            if(currentScore>=50)
                EndGame(true);
            else if (currentScore < 50)
                EndGame(false);
        }
    }

    public void EndGame(bool wonGame)
    {
        if (wonGame)
            Score.gamesWon++;
        else
            Score.gamesLost++;

        Score.winPercent = (int) Mathf.Round(Score.gamesLost / Score.gamesWon * 100);

        if (currentScore > Score.highScore)
            Score.highScore = currentScore;
        if (currentHighestChain > Score.bestChain)
            Score.bestChain = currentHighestChain;
    }
}
