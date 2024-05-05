using Firebase.Database;
using Firebase.Extensions;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UserPanel : MonoBehaviour
{
    [SerializeField] DataObject dataObject;

    [SerializeField] TMP_Text nickNameText;
    [SerializeField] TMP_InputField nickNameInputField;
    [SerializeField] Button nickNameApplyButton;

    [SerializeField] TMP_Text levelText;
    [SerializeField] Button levelUpButton;
    [SerializeField] Button levelDownButton;

    [SerializeField] TMP_Text characterText;
    [SerializeField] TMP_Dropdown characterDropDown;

    [SerializeField] Button logoutButton;

    private string UserDataRef => $"UserData/{FirebaseManager.Auth.CurrentUser.UserId}";
    private UserData userData = null;

    private void Awake()
    {
        nickNameApplyButton.onClick.AddListener(NickNameApply);
        levelUpButton.onClick.AddListener(LevelUp);
        levelDownButton.onClick.AddListener(LevelDown);
        characterDropDown.AddOptions(typeof(CharacterType).GetEnumNames().ToList());
        characterDropDown.onValueChanged.AddListener(CharacterChange);
        logoutButton.onClick.AddListener(Logout);
    }

    private void Start()
    {
        FirebaseManager.DB
            .GetReference(UserDataRef)
            .GetValueAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.Log("Get userdata canceled");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.Log($"Get userdata failed : {task.Exception.Message}");
                    return;
                }

                DataSnapshot snapShot = task.Result;
                if (snapShot.Exists)
                {
                    string json = snapShot.GetRawJsonValue();
                    userData = JsonUtility.FromJson<UserData>(json);
                }
                else
                {
                    userData = new UserData();
                }
                UpdateUserData();
            });
    }

    private void UpdateUserData()
    {
        nickNameText.text = userData.nickName;
        levelText.text = userData.level.ToString();
        characterText.text = userData.type.ToString();
    }

    private void NickNameApply()
    {
        FirebaseManager.DB
            .GetReference(UserDataRef)
            .Child("nickName")
            .SetValueAsync(nickNameInputField.text)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.Log("NickNameApply canceled");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.Log($"NickNameApply failed : {task.Exception.Message}");
                    return;
                }

                userData.nickName = nickNameInputField.text;
                UpdateUserData();
            });
    }

    private void LevelUp()
    {
        FirebaseManager.DB
            .GetReference(UserDataRef)
            .Child("level")
            .SetValueAsync(userData.level + 1)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.Log("NickNameApply canceled");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.Log($"NickNameApply failed : {task.Exception.Message}");
                    return;
                }

                userData.level++;
                UpdateUserData();
            });
    }

    private void LevelDown()
    {
        FirebaseManager.DB
            .GetReference(UserDataRef)
            .Child("level")
            .SetValueAsync(userData.level - 1)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.Log("NickNameApply canceled");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.Log($"NickNameApply failed : {task.Exception.Message}");
                    return;
                }

                userData.level--;
                UpdateUserData();
            });
    }

    private void CharacterChange(int index)
    {
        FirebaseManager.DB
            .GetReference(UserDataRef)
            .Child("type")
            .SetValueAsync(index)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.Log("NickNameApply canceled");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.Log($"NickNameApply failed : {task.Exception.Message}");
                    return;
                }

                userData.type = (CharacterType)index;
                UpdateUserData();
            });
    }

    private void Logout()
    {
        FirebaseManager.Auth.SignOut();
        SceneManager.LoadScene("AuthScene");
    }
}