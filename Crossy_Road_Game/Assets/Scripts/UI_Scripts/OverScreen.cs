using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverScreen : View
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnReplayClicked()
    {

        Parameters updateLineParams = new Parameters();
        updateLineParams.PutExtra(EventNames.ON_REPLAY_NAME, 1);
        EventBroadcaster.Instance.PostEvent(EventNames.ON_RESUME_NAME, updateLineParams);
        Parameters updateLineParams2 = new Parameters();
        updateLineParams.PutExtra(EventNames.ON_PAUSE_NAME, 0);
        EventBroadcaster.Instance.PostEvent(EventNames.ON_PAUSE_NAME, updateLineParams2);
        ViewHandler.Instance.HideCurrentView();
        LoadManager.Instance.LoadScene(SceneNames.LOAD_SCENE);
    }

    public void OnMainMenuClicked()
    {

     LoadManager.Instance.LoadScene(SceneNames.MAIN_MENU_SCENE);

    }
}
