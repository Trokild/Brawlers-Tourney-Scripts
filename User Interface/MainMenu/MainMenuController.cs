using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    public GameObject PlayGameMenu;
    public TextMeshProUGUI textHeader;

    public void TurnOnPlayGame()
    {
        PlayGameMenu.SetActive(true);
        textHeader.SetText("Game Options");
    }

    public void TurnOffPlayGame()
    {
        PlayGameMenu.SetActive(false);
        textHeader.SetText("Main Menu");
    }
}
