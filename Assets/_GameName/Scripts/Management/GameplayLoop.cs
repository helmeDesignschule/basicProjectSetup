using UnityEngine;
using UnityEngine.SceneManagement;

//this is a dummy-implementation of the game loop.
//in this implementation, pressing any button sends the player back to the main menu.
//Any actual gameplay code would go here.
public class GameplayLoop : MonoBehaviour
{
    private void Start()
    {
        SceneManager.SetActiveScene(gameObject.scene);
    }

    private void Update()
    {
        if (Input.anyKey)
            ReturnToMainMenu();
    }

    public void ReturnToMainMenu()
    {
        GameStateManager.GoToMainMenu();
    }
}
