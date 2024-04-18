using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private AudioClip changescenesound;
    public void ChangeScene()
    {
        AudioManager.InstanceAM.PlaySound(changescenesound);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

}
