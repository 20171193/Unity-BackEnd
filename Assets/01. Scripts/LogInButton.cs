using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LogInButton : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField emailInputField;

    [SerializeField]
    private TMP_InputField passInputField;

    public void Login()
    {
        string id = emailInputField.text;
        string pass = passInputField.text;

        FirebaseManager.Auth.SignInWithEmailAndPasswordAsync(id, pass).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("Sign in was canceled");
                return;
            }
            if(task.IsFaulted)
            {
                Debug.LogError($"Sign in encountered an error: {task.Exception}");
                return;
            }

            Firebase.Auth.AuthResult result = task.Result;
            Debug.Log($"User signed in successfully: {result.User.DisplayName} ({result.User.UserId})");
            //if (result.User.IsEmailVerified)
            //{
            //    panelController.SetActivePanel(PanelController.Panel.Main);
            //}
            //else
            //{
            //    panelController.SetActivePanel(PanelController.Panel.Verify);
            //}
            //SetInteractable(true);
        });
    }

}
