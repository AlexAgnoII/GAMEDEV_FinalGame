using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : View
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPauseButtonClicked()
    {
        Parameters updateLineParams = new Parameters();
        updateLineParams.PutExtra(EventNames.ON_PAUSE_NAME, 0);
        EventBroadcaster.Instance.PostEvent(EventNames.ON_PAUSE_NAME, updateLineParams);

        Parameters updateLineParams2 = new Parameters();
        updateLineParams.PutExtra(EventNames.ON_RESUME_NAME, 0);
        EventBroadcaster.Instance.PostEvent(EventNames.ON_RESUME_NAME, updateLineParams2);

        ViewHandler.Instance.Show(ViewNames.PAUSE_SCREEN, true);
    }
}
