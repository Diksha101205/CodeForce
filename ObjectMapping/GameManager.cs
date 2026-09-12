using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject victoryPanel;

    public void PlayerWon()
    {
        Debug.Log("PLAYER WON!");
        if (victoryPanel != null) victoryPanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
