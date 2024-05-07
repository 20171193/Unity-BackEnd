using Firebase.Database;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePanel : MonoBehaviour
{
    private void Start()
    {
        FirebaseManager.DB
            .GetReference("ScoreBoard")
            .OrderByChild("score")  // 정렬된 값으로 snapshot
            .LimitToFirst(3)
            .ValueChanged += ScoreBoardChanged;

            //.ContinueWithOnMainThread(task =>
            //{
            //    if(task.IsCanceled)
            //    {
            //        Debug.Log("ScoreBoard get value async is canceled");
            //        return;
            //    }
            //    else if(task.IsFaulted)
            //    {
            //        Debug.Log($"ScoreBoard get value async is faulted : {task.Exception.Message}");
            //        return;
            //    }

            //    DataSnapshot snapshot = task.Result;

            //    foreach(DataSnapshot child in snapshot.Children)
            //    {
            //        ScoreData scoreData = JsonUtility.FromJson<ScoreData>(child.GetRawJsonValue());
            //    }
            //});
    }

    private void OnEnable()
    {
        FirebaseManager.DB
            .GetReference("ScoreBoard")
            .OrderByChild("score")  // 정렬된 값으로 snapshot
            .LimitToLast(3)
            .ValueChanged += ScoreBoardChanged;
    }

    private void OnDisable()
    {
        FirebaseManager.DB
            .GetReference("ScoreBoard")
            .OrderByChild("score")  // 정렬된 값으로 snapshot
            .LimitToLast(3)
            .ValueChanged -= ScoreBoardChanged;
    }

    private void ScoreBoardChanged(object sender, ValueChangedEventArgs args)
    {
        DataSnapshot snapshot = args.Snapshot;

        List<DataSnapshot> sorted = new List<DataSnapshot>(snapshot.Children);
        // 기본 정렬은 오름차순
        sorted.Reverse();

        foreach(DataSnapshot child in sorted)
        {
            ScoreData scoreData = JsonUtility.FromJson<ScoreData>(child.GetRawJsonValue());
        }
    }
}

[SerializeField]
public class ScoreData
{
    public string nickName;
    public int score;
}
