using Firebase.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResetPanel : MonoBehaviour
{
    [SerializeField] PanelController panelController;

    [SerializeField] TMP_InputField emailInputField;

    [SerializeField] Button sendButton;
    [SerializeField] Button cancelButton;

    private void Awake()
    {
        sendButton.onClick.AddListener(SendResetMail);
        cancelButton.onClick.AddListener(Cancel);
    }

    // 비밀번호 재설정 메일 보내기
    private void SendResetMail()
    {
        SetInteractable(false);

        string email = emailInputField.text;

        FirebaseManager.Auth.SendPasswordResetEmailAsync(email).ContinueWithOnMainThread(task =>
        {
            if(task.IsCanceled)
            {
                panelController.ShowInfo("SendPasswordResetEmailAsync canceled");
                SetInteractable(true);
                return;
            }
            else if(task.IsFaulted)
            {
                panelController.ShowInfo($"SendPasswordResetEmailAsync failed : {task.Exception.Message}");
                SetInteractable(true);
                return;
            }

            panelController.ShowInfo("SendPasswordResetEmailAsync success");
            panelController.SetActivePanel(PanelController.Panel.Login);
        });
    }

    private void Cancel()
    {
        panelController.SetActivePanel(PanelController.Panel.Login);
    }

    private void SetInteractable(bool interactable)
    {
        emailInputField.interactable = interactable;
        sendButton.interactable = interactable;
        cancelButton.interactable = interactable;
    }
}
