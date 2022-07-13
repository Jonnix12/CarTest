using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LevelSelectionManager : MonoBehaviour
{
    [Header("Level Selection")]
    [SerializeField] private GameObject _levelSelectionPanel;
    [SerializeField] private GameObject _firstLevelButton;
    private int _levelChoice;

   [Header("Car Selection")]
    [SerializeField] private GameObject _carSelectionPanel;
    [SerializeField] private GameObject _carSelectionEnvironment;
    [SerializeField] private GameObject _firstCarSelectionButton;
    private int _carChoice;

    public void Start()
    {
        _levelChoice = 0;
        _carChoice = 0;
        MoveToLevelSelection();
    }

    public void MoveBackToMainMenu()
    {
        //Load Scene
    }

    public void MoveToLevelSelection()
    {
        _levelSelectionPanel.gameObject.SetActive(true);
        _carSelectionPanel.gameObject.SetActive(false);
        _carSelectionEnvironment.gameObject.SetActive(false);
        UpdateFirstButton(_firstLevelButton);
    }

    public void MoveToCarChoice()
    {
        if(_levelChoice==0)
        {
            Debug.LogError("I didnt chose any level");
        }
        _levelSelectionPanel.gameObject.SetActive(false);
        _carSelectionPanel.gameObject.SetActive(true);
        _carSelectionEnvironment.gameObject.SetActive(true);
        UpdateFirstButton(_firstCarSelectionButton);
    }

    public void ChooseLevel(int levelIndex)
    {
        _levelChoice = levelIndex;
    }

    public void ChooseCar(int carIndex)
    {
        _carChoice = carIndex;
    }

    public void LoadLevel()
    {
        if (_carChoice == 0)
        {
            Debug.LogError("I didnt chose any car");
        }

        //if the player chose the batmobile- is this scene the yellow car will be setactvie false
        //both of the cars need to be in the scene but only one of them will be active
        //We can use _carChoice to know the car index
        if (_levelChoice == 1)
        {
            //load farm
        }

        if (_levelChoice == 2)
        {
            //load desert
        }

        if (_levelChoice == 3)
        {
            //load mountaints
        }
    }

    private void UpdateFirstButton(GameObject firstButton)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButton);
    }
}