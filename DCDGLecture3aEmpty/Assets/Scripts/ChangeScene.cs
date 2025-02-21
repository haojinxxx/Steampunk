using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour{
 public void MoveToSceneGrandpaLetter(){
    Debug.Log("method called");
    SceneManager.LoadScene("grandpaIntroLetter");
 }

}
