using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnloadLevel : MonoBehaviour {

    public string nextLevel;
	// Use this for initialization
	void Start () {
       
	}

  public  void changingLevel()
    {
        SceneManager.LoadSceneAsync(nextLevel, LoadSceneMode.Single);
        //SteamVR_LoadLevel.Begin(sceneToLoad, LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
