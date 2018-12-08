using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : View
{

    [SerializeField] Button pause_button;
    // Use this for initialization

    void Start()
    {
        this.pause_button.GetComponent<Button>().interactable = false;
        EventBroadcaster.Instance.AddObserver(EventNames.ON_TIMER_DONE, MakeMeClickable);
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.ON_TIMER_DONE);
    }

    private void MakeMeClickable()
    {
        this.pause_button.GetComponent<Button>().interactable = true;
    }
    public void OnPauseButtonClicked()
    {
        EventBroadcaster.Instance.PostEvent(EventNames.ON_PAUSE_NAME);
        ViewHandler.Instance.Show(ViewNames.PAUSE_SCREEN, true);
    }
}
