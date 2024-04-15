using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using TMPro;


public class Menu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button easyPlayButton;
    [SerializeField] private Button mediumPlayButton;
    [SerializeField] private Button hardPlayButton;
    [SerializeField] private Button restartPlayButton;
    [SerializeField] private Button backButton2;
    [SerializeField] private Button backButton3;
    [SerializeField] private Button informationButton;

    [SerializeField] private GameObject Page1;
    [SerializeField] private GameObject Page2;
    [SerializeField] private GameObject Page3;
    [SerializeField] private GameObject Page4;

    [SerializeField] private TMP_Text UpperText;

    [SerializeField] private TMP_Text KillsHSTextLabel;
    [SerializeField] private TMP_Text KillsTextLabel;
    [SerializeField] private TMP_Text ComboTextLabel;

    private DataController dataController;

    private void Start()
    {
        dataController = GameObject.Find("DataController").GetComponent<DataController>();
        Subscribe();
        SetText();


        if(dataController.Defeat)
        {
            UpperText.text = "Game Over";
            dataController.Defeat = false;
            Page1.SetActive(false);
            Page2.SetActive(false);
            Page3.SetActive(true);
        }
        else
        {
            UpperText.text = "Nightmares";
            Page1.SetActive(true);
            Page2.SetActive(false);
            Page3.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        UnSubscribe();
    }

    private void SetText()
    {
        KillsHSTextLabel.text = "Kills High Score: " + dataController.KillsHighScore.ToString();
        KillsTextLabel.text = "Kills: " + dataController.Kills.ToString();
        ComboTextLabel.text = "Max Combo: " + dataController.Combo.ToString();
    }

    private void PlayButton()
    {
        Page1.SetActive(false);
        Page2.SetActive(true);
    }

    private void BackButton()
    {
        UpperText.text = "Nightmares";
        Page1.SetActive(true);
        Page2.SetActive(false);
        Page3.SetActive(false);
        Page4.SetActive(false);
    }

    ///

    private void EasyPlayButton()
    {
        ChangeDifficulty(0);
        StartGame();
    }

    private void MediumPlayButton()
    {
        ChangeDifficulty(1);
        StartGame();
    }

    private void HardPlayButton()
    {
        ChangeDifficulty(2);
        StartGame();
    }

    ///
    private void ChangeDifficulty(int diff)
    {
        dataController.DifficultyVal = diff;
    }

    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    private void InformationButton()
    {
        Page1.SetActive(false);
        Page4.SetActive(true);
    }


    private void Subscribe()
    {
        playButton.onClick.AddListener(PlayButton);
        exitButton.onClick.AddListener(Quit);
        backButton.onClick.AddListener(BackButton);
        easyPlayButton.onClick.AddListener(EasyPlayButton);
        mediumPlayButton.onClick.AddListener(MediumPlayButton);
        hardPlayButton.onClick.AddListener(HardPlayButton);
        restartPlayButton.onClick.AddListener(StartGame);
        backButton2.onClick.AddListener(BackButton);
        backButton3.onClick.AddListener(BackButton);
        informationButton.onClick.AddListener(InformationButton);
    }

    private void UnSubscribe()
    {
        playButton.onClick.RemoveListener(PlayButton);
        exitButton.onClick.RemoveListener(Quit);
        backButton.onClick.RemoveListener(BackButton);
        easyPlayButton.onClick.RemoveListener(EasyPlayButton);
        mediumPlayButton.onClick.RemoveListener(MediumPlayButton);
        hardPlayButton.onClick.RemoveListener(HardPlayButton);
        restartPlayButton.onClick.RemoveListener(StartGame);
        backButton2.onClick.RemoveListener(BackButton);
        backButton3.onClick.RemoveListener(BackButton);
        informationButton.onClick.RemoveListener(InformationButton);
    }

    private void Quit()
    {
        Application.Quit();
    }
}
