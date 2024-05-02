using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour
{
    [SerializeField] PanelController panelController;

    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text emailText;
    [SerializeField] TMP_Text idText;

    [SerializeField] Button logoutButton;
    [SerializeField] Button editButton;

    private void Awake()
    {
        logoutButton.onClick.AddListener(Logout);
        editButton.onClick.AddListener(Edit);
    }

    private void Logout()
    {
        
    }

    private void Edit()
    {
        
    }
}
