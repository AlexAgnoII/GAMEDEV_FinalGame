using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : View
{

    public void OnPlayClicked()
    {
       //LoadManager.Instance.LoadScene(SceneNames.LOAD_SCENE);
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }
}
