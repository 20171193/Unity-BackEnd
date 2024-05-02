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

    private void SendResetMail()
    {
        
    }

    private void Cancel()
    {
        
    }

    private void SetInteractable(bool interactable)
    {
        emailInputField.interactable = interactable;
        sendButton.interactable = interactable;
        cancelButton.interactable = interactable;
    }
}
