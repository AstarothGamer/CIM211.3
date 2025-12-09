using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !pauseMenuPanel.activeSelf)
        {
            pauseMenuPanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && pauseMenuPanel.activeSelf)
        {
            pauseMenuPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void Continue()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void QuitMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
