using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
  
   public void StartGame(string level)
    {
        SceneManager.LoadScene(level);

    }
    public void Options()
    {

    }
   public void QuitGame()
   {
        Application.Quit();
   }


}
