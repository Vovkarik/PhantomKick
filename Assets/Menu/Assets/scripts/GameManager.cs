using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject ChoosePanel;
    public GameObject LevelsPanel;
    public GameObject GamePanel;
    public GameObject SettingsPanel;
    public GameObject PausePanel;
    public GameObject BackButton;
    public GameObject PauseButton;
    public GameObject ResumeButton;
    public GameObject QuitButton;
    public GameObject SettingsButton;
    public GameObject RestartButton;
    public Button Level1Button;
    public ScrollRect Level1Scroll;
    public ScrollRect Level2Scroll;
    public ScrollRect Level3Scroll;

    private bool inGame = false;
    
    void Start ()
    { 
        ChoosePanel.SetActive(true);
        LevelsPanel.SetActive(false);
        GamePanel.SetActive(false);
        SettingsPanel.SetActive(false);
        PausePanel.SetActive(false); 
        inGame = false;
        Level1Scroll.gameObject.SetActive(false);
        Level2Scroll.gameObject.SetActive(false);
        Level3Scroll.gameObject.SetActive(false);
    }
	
    public void Menu()
    {
        ChoosePanel.SetActive(true);
        LevelsPanel.SetActive(false);
        SettingsPanel.SetActive(false);
    }

    public void ChooseLevel()
    {
        ChoosePanel.SetActive(false);
        LevelsPanel.SetActive(true);
    }

    public void BackMenu()
    {
        if (!inGame)
        {
            Menu();
        }
        else
        {
            Pause();
        }
        
    }

    public void SettingsMenu()
    {
        ChoosePanel.SetActive(false);
        PausePanel.SetActive(false);
        SettingsPanel.SetActive(true);
    }

    public void Game()
    {
        LevelsPanel.SetActive(false);
        ChoosePanel.SetActive(false);
        PausePanel.SetActive(false);
        GamePanel.SetActive(true);
        inGame = true;
    }

    public void Pause()
    {
        GamePanel.SetActive(false);
        SettingsPanel.SetActive(false);
        PausePanel.SetActive(true);
    }

    public void Quit()
    {
        inGame = false;
        PausePanel.SetActive(false);
        GamePanel.SetActive(false);
        ChoosePanel.SetActive(true);
    }

    public void Level1Hover()
    {
        Level1Scroll.gameObject.SetActive(true);
    }

    public void Level1UnHover()
    {
        Level1Scroll.gameObject.SetActive(false);
    }

    public void Level2Hover()
    {
        Level2Scroll.gameObject.SetActive(true);
    }

    public void Level2UnHover()
    {
        Level2Scroll.gameObject.SetActive(false);
    }

    public void Level3Hover()
    {
        Level3Scroll.gameObject.SetActive(true);
    }

    public void Level3UnHover()
    {
        Level3Scroll.gameObject.SetActive(false);
    }
}
