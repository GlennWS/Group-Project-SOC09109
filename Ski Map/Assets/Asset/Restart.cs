using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//Respawn Script for photo real ski must be attached to game object so button can run
//callum galbraith
//06.04.18
public class Restart : MonoBehaviour {
    public Button ButRestart;

	public void TaskOnClick() {
        SceneManager.LoadScene("TestScene");
    }
}
