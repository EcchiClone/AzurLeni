using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnStartGame : MonoBehaviour
{
    public void OnStartGameBtnClicked()
    {
        SceneManager.LoadScene("Lobby");
    }
}
