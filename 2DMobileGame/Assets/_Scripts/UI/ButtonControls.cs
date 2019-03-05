using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonControls : MonoBehaviour {

    public GameObject startMenu;
    public GameObject optionsMenu;
    public Dropdown myDropdown;
    public Text InputDropMultiplierText;

    readonly List<int> PongValues = new List<int>() { 3, 5, 7, 9, 15 };

    public void ExitGame()
    {
        Application.Quit();
    }
    public void LoadScene1()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadScene2()
    {
        SceneManager.LoadScene(2);
    }
    public void LoadScene3()
    {
        SceneManager.LoadScene(3);
    }
    public void LoadScene4()
    {
        SceneManager.LoadScene(4);
    }

    public void StartMenuActive()
    {
        startMenu.SetActive(!startMenu.activeSelf);
    }
    public void OptionsMenuActive()
    {
        optionsMenu.SetActive(!optionsMenu.activeSelf);
    }
    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void PongDropdown()//chooses best out of pong value
    {
        PlayerPrefs.SetInt("BestOutOfScore", PongValues[myDropdown.value]);
        Debug.Log(PongValues[myDropdown.value]);
    }
}