using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
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
            Cursor.lockState = CursorLockMode.None;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && pauseMenuPanel.activeSelf)
        {
            pauseMenuPanel.SetActive(false);
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void Continue()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void QuitMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
