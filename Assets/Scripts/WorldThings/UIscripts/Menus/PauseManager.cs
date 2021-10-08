using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private bool IsPaused;
    public GameObject PausePanel;
    public GameObject OnGameCanvas;
    public GameObject InventoryPanel;
    
    
    // Start is called before the first frame update
    void Start()
    {
        IsPaused = false;
        PausePanel.SetActive(false);
        InventoryPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Enter"))
        {
            Resume();
        }
    }
    public void Resume()
    {
        IsPaused = !IsPaused;
        if (IsPaused)
        {
            PausePanel.SetActive(true);
            InventoryPanel.SetActive(false);
            OnGameCanvas.SetActive(false);
            Time.timeScale = 0f;
        }
        else
        {
            PausePanel.SetActive(false);
            InventoryPanel.SetActive(false);
            OnGameCanvas.SetActive(true);
            Time.timeScale = 1f;
        }
    }
    public void QuitToMenu()
    {
        SceneManager.LoadScene("StartMenu");
        Time.timeScale = 1f;
    }

    public void SwitchPanels()
    {
        PausePanel.SetActive(!PausePanel.activeInHierarchy);
        InventoryPanel.SetActive(!InventoryPanel.activeInHierarchy);
        if (InventoryPanel.activeInHierarchy)
        {
            OnGameCanvas.SetActive(true);
        }
        else
        {
            OnGameCanvas.SetActive(false);
        }
    }
}
