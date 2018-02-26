using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager : MonoBehaviour {

    public GameObject gameOverScreen;

    public void LoadGameOverScreen()
    {
        gameOverScreen.SetActive(true);
    }
}
