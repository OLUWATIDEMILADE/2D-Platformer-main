using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

	public GameObject mainMenu;
	public GameObject restartGame;
    public GameObject QuitGame;

    //Restart Game
    public void Reset()
    {
        //set menu false and restart the game in gameplay scene
        mainMenu.SetActive(false);
        SceneManager.LoadScene("GameScene-ALU");
    }

    //Quit Game
    public void Quit()
    {
        //Quit the game
        Application.Quit();
    }



    //Re

    //   public void PlayGame() {
    //	SceneManager.LoadScene ("Gameplay");
    //}

}
