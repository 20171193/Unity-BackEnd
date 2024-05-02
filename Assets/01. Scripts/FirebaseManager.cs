using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using System.Net.Mail;
using UnityEngine;

public class FirebaseManager : MonoBehaviour
{
    public enum Panel { Loading, Login, SignUp, Main, Edit }

    private static FirebaseManager instance;
    public static FirebaseManager Instance { get { return instance; } }

    private FirebaseApp app;
    public static FirebaseApp App { get { return instance?.app; } }

    private FirebaseAuth auth;
    public static FirebaseAuth Auth { get { return instance?.auth; } }

    //[SerializeField] InfoPanel infoPanel;
    //[SerializeField] LoadingPanel loadingPanel;
    //[SerializeField] LoginPanel loginPanel;
    //[SerializeField] SignUpPanel signUpPanel;
    //[SerializeField] MainPanel mainPanel;
    //[SerializeField] EditPanel editPanel;

    private void Awake()
    {
        CreateInstance();
        CheckDependency();
    }

    private void Start()
    {
        //SetActivePanel(Panel.Loading);
    }

    private void CreateInstance()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void CheckDependency()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            DependencyStatus dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                app = FirebaseApp.DefaultInstance;
                auth = FirebaseAuth.DefaultInstance;

                // Set a flag here to indicate whether Firebase is ready to use by your app.
                //SetActivePanel(Panel.Login);
            }
            else
            {
                // Firebase Unity SDK is not safe to use here.
                Debug.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}");
            }
        });
    }

    //public void SetActivePanel(Panel panel)
    //{
    //    loadingPanel.gameObject.SetActive(panel == Panel.Loading);
    //    loginPanel.gameObject.SetActive(panel == Panel.Login);
    //    signUpPanel.gameObject.SetActive(panel == Panel.SignUp);
    //    mainPanel.gameObject.SetActive(panel == Panel.Main);
    //    editPanel.gameObject.SetActive(panel == Panel.Edit);
    //}

    //public void ShowInfo(string message)
    //{
    //    infoPanel.ShowInfo(message);
    //}
}