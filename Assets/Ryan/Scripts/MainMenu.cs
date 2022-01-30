using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.InputAction;

public class MainMenu : MonoBehaviour
{
    PlayerControls controls;
    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            controls = new PlayerControls();
            controls.Menu.PauseMenu.performed += PauseGame;
            controls.Menu.PauseMenu.Enable();
        }
    }
    public void PlayGame()
    {
        GameManager.instance.sceneLoad = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LevelSelect(int level)
    {
        GameManager.instance.sceneLoad = level;
        SceneManager.LoadScene("Level_Final");
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void PauseGame(CallbackContext ctx)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GetComponent<Canvas>().enabled = true;
        Time.timeScale = 0;
    }
    public void UnpauseGame()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GetComponent<Canvas>().enabled = false;
        Time.timeScale = 1;
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    private void OnDestroy()
    {
        if (controls != null)
            controls.Disable();
    }
}
