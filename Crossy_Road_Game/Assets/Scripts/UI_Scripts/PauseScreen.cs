using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : View
{

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            this.SendResumeToEveryone();
        }
    }

    

    public void OnResumeClicked()
    {
        this.SendResumeToEveryone();
        ViewHandler.Instance.HideCurrentView();
    }

    private void SendResumeToEveryone()
    {
        EventBroadcaster.Instance.PostEvent(EventNames.ON_RESUME_NAME);
    }

    public void OnMainMenuClicked()
    {
        if (ViewHandler.Instance.IsViewActive("pauseScreen"))
        {
            TwoChoiceDialog exitDialog = (TwoChoiceDialog)DialogBuilder.Create(DialogBuilder.DialogType.CHOICE_DIALOG);
            exitDialog.SetMessage("Are you sure? All progress will be lost.");
            exitDialog.SetConfirmText("Confirm");
            exitDialog.SetCancelText("Cancel");

            exitDialog.SetOnConfirmListener(() =>
            {
                LoadManager.Instance.LoadScene(SceneNames.MAIN_MENU_SCENE);
            });
        }
    }
}
