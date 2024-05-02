using Firebase.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditPanel : MonoBehaviour
{
    [SerializeField] PanelController panelController;

    [SerializeField] TMP_InputField nameInputField;
    [SerializeField] TMP_InputField passInputField;
    [SerializeField] TMP_InputField confirmInputField;

    [SerializeField] Button nameApplyButton;
    [SerializeField] Button passApplyButton;
    [SerializeField] Button backButton;
    [SerializeField] Button deleteButton;

    private void Awake()
    {
        nameApplyButton.onClick.AddListener(NameApply);
        passApplyButton.onClick.AddListener(PassApply);
        backButton.onClick.AddListener(Back);
        deleteButton.onClick.AddListener(Delete);
    }

    private void NameApply()
    {
        
    }

    private void PassApply()
    {
        
    }

    private void Back()
    {
        
    }

    private void Delete()
    {
        
    }

    private void SetInteractable(bool interactable)
    {
        nameInputField.interactable = interactable;
        passInputField.interactable = interactable;
        confirmInputField.interactable = interactable;
        nameApplyButton.interactable = interactable;
        passApplyButton.interactable = interactable;
        backButton.interactable = interactable;
        deleteButton.interactable = interactable;
    }
}
