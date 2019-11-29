using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public static bool paused = false;

    public GameObject pausedPanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        paused = true;

        Time.timeScale = 0;

        pausedPanel.SetActive(true);
    }

    public void Resume()
    {
        pausedPanel.SetActive(false);

        Time.timeScale = 1;

        paused = false;
    }

    public void Restart()
    {
        if (paused)
            Resume();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        if (paused)
            Resume();

        SceneManager.LoadScene(0);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Debug.Log("Quitting!");

        Application.Quit();
    }

}
