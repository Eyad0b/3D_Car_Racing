using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CarSelection : MonoBehaviour
{
    [Header("Buttons and Canvas")]
    public Button nextButton;
    public Button previousButton;

    [Header("Cameras")]
    public GameObject cam1;
    public GameObject cam2;


    [Header("Buttons and Canvas")]
    public GameObject SelectionCanvas;
    public GameObject skipButton;
    public GameObject PlayButton;
    public GameObject SettingsButton;
    public GameObject GameModeButton;
    public GameObject SteeringControlButton;
    public GameObject TouchControlButton;

    [Header("Settings")]
    public GameObject SettingsUI;
    // public GameObject playerUI;
    public GameObject NextUI;
    public GameObject PreviousUI;
    // public PlayerCarController playerCarController;
    public GameObject Break;
    public GameObject ArrowsVertical;
    public GameObject SteeringWheel;

    public bool isSteeringModeEnabled;
    public bool isTouchModeEnabled;

   


    private int currentCar;

    private GameObject[] carList;
    private void Awake()
    {
        SelectionCanvas.SetActive(false);
        PlayButton.SetActive(false);
        SettingsButton.SetActive(false);
        cam2.SetActive(false);
        chooseCar(0);
    }
    private void Start()
    {

        currentCar = PlayerPrefs.GetInt("CarSelected");
        carList =  new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
            carList[i] = transform.GetChild(i).gameObject;
        foreach (GameObject go in carList)
            go.SetActive(false);
        if (carList[currentCar])
            carList[currentCar].SetActive(true);
    }
    private void chooseCar(int index)
    {
        previousButton.interactable = (currentCar != 0);
        nextButton.interactable= (currentCar != transform.childCount -1);

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == index);
        }
    }

    public void switchCar(int switchCars)
    {
        currentCar += switchCars;
        chooseCar(currentCar);
    }

    public void playGame()
    {
        PlayerPrefs.SetInt("CarSelected", currentCar);
        SceneManager.LoadScene("scene_overcast");
        Time.timeScale = 1f;
        Menu.GameIsStopped = false;
        PlayerCarController.habed = isTouchModeEnabled;
        PlayerCarController.habed2 = isSteeringModeEnabled;

    }



    public void SkipButton()
    {
        SelectionCanvas.SetActive(true);
        PlayButton.SetActive(true);
        SettingsButton.SetActive(true);
        skipButton.SetActive(false);
        cam1.SetActive(false);
        cam2.SetActive(true);
    }

    public void GameMode()
    {
        TouchControlButton.SetActive(true);
        SteeringControlButton.SetActive(true);
        GameModeButton.SetActive(false);
    }

    public void SteeringControl()
    {
        SettingsUI.SetActive(false);
        SettingsButton.SetActive(true);
        PlayButton.SetActive(true);
        // SettingsUI.SetActive(true);
        // playerUI.SetActive(false);
        NextUI.SetActive(true);
        PreviousUI.SetActive(true);
        isSteeringModeEnabled = true;
        isTouchModeEnabled = false;

        GameModeButton.SetActive(true);

        // Break.SetActive(true);
        // ArrowsVertical.SetActive(true);
        // SteeringWheel.SetActive(true);

    }

    public void TouchControl()
    {
        SettingsUI.SetActive(false);
        SettingsButton.SetActive(true);
        PlayButton.SetActive(true);
        // SettingsUI.SetActive(true);
        // playerUI.SetActive(false);
        NextUI.SetActive(true);
        PreviousUI.SetActive(true);
        isSteeringModeEnabled = false;
        isTouchModeEnabled = true;

        GameModeButton.SetActive(true);



        // Break.SetActive(false);
        // ArrowsVertical.SetActive(false);
        // SteeringWheel.SetActive(false);
    }

    public void goToSettings()
    {
        SettingsButton.SetActive(false);
        PlayButton.SetActive(false);
        SettingsUI.SetActive(true);
        // playerUI.SetActive(false);
        NextUI.SetActive(false);
        PreviousUI.SetActive(false);
        Time.timeScale = 0f;
    }
}
