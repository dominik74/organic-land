using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour {

    public void ToggleFullsreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

	public void Quit()
    {
        Application.Quit();
        Debug.Log("Quitting application...");
    }

}
