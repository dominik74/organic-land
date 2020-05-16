using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

	public GameObject loadingScreen;

	public void LoadScene(string sceneName)
	{
		loadingScreen.SetActive(true);
		SceneManager.LoadScene(sceneName);
	}

}
