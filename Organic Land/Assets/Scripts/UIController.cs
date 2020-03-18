using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

    public Transform screensParent;
    private GameObject[] screens;

    private InventorySystem inventorySystem;

    public static UIController instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        inventorySystem = InventorySystem.instance;

        // Initialize screens
        screens = new GameObject[screensParent.childCount];
        for (int i = 0; i < screensParent.childCount; i++)
        {
            screens[i] = screensParent.GetChild(i).gameObject;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            inventorySystem.SelectSlot(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            inventorySystem.SelectSlot(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            inventorySystem.SelectSlot(2);
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            inventorySystem.SelectSlot(3);
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            inventorySystem.SelectSlot(4);
        else if (Input.GetKeyDown(KeyCode.Alpha6))
            inventorySystem.SelectSlot(5);
        else if (Input.GetKeyDown(KeyCode.Alpha7))
            inventorySystem.SelectSlot(6);
        else if (Input.GetKeyDown(KeyCode.Alpha8))
            inventorySystem.SelectSlot(7);
        else if (Input.GetKeyDown(KeyCode.Alpha9))
            inventorySystem.SelectSlot(8);

        if (Input.GetKeyDown(KeyCode.Escape))
            Escape();
        else if (Input.GetKeyDown(KeyCode.T))
            SetScreen("pnlConsole", true);
        else if (Input.GetKeyDown(KeyCode.Tab))
            SetScreen("pnlInventory", true);
        else if (Input.GetKeyDown(KeyCode.Z))
            inventorySystem.RemoveSelectedItem();
        else if (Input.GetKeyDown(KeyCode.I))
            inventorySystem.DropSelectedItem();
    }

    public void SetScreenString(string input)
    {
        string[] words = input.Split(' ');
        string screenName = "";
        bool state;
        if (words.Length > 1)
        {
            screenName = words[0];
            state = words[1] == "true";
            SetScreen(screenName, state);
        }

    }

    public void SetScreen(string screenName, bool state)
    {
        for (int i = 0; i < screens.Length; i++)
        {
            if (screens[i].name == screenName)
            {
                if (screens[i].activeSelf && state == true)
                    screens[i].SetActive(false);
                else
                    screens[i].SetActive(state);
            }
        }

        UpdateTimeScale();
    }

    bool DetectActiveScreen()
    {
        for (int i = 0; i < screens.Length; i++)
        {
            if (screens[i].activeSelf)
                return true;
        }
        return false;
    }

    GameObject GetActiveScreen()
    {
        for (int i = 0; i < screens.Length; i++)
        {
            if (screens[i].activeSelf)
                return screens[i];
        }
        return null;
    }

    void Escape()
    {
        GameObject activeScreen = GetActiveScreen();

        if (activeScreen != null)
        {
            activeScreen.SetActive(false);
            UpdateTimeScale();
        }
        else
            SetScreen("pnlPause", true);

    }

    void UpdateTimeScale()
    {
        if (DetectActiveScreen())
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

}
