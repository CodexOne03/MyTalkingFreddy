using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreddyManager : MonoBehaviour
{
    public static FreddyManager Instance;

    public State CurrentState = State.Idle;
    public string PlayerState = "Idle";

    void Start()
    {
        Instance = this;
    }

    void Update()
    {
        
    }

    private void OnGUI()
    {
        GUILayout.BeginVertical();
        if (GUILayout.Button("Phone"))
        {
            CallPhone();
        }
        if (GUILayout.Button("Talk"))
        {
            PlayerTalk();
        }
        GUILayout.Button("Silence");
        GUILayout.Label($"Freddy state:{this.CurrentState}");
        GUILayout.Label($"Player state:{this.PlayerState}");
        GUILayout.EndVertical();
    }

    private void PlayerTalk()
    {
        if (this.CurrentState == State.Idle)
        {
            MockPlayer();
        }
        else if (this.CurrentState == State.TakingPhone)
        {
            this.CurrentState = State.Listening;
            //yield return new WaitForSeconds(1f);
            if (UnityEngine.Random.Range(0, 100 / 25) == 0)
            {
                DropPhone();
            }
            else
            {
                AnswerPlayer();
            }
        }
    }

    private void AnswerPlayer()
    {
        if (UnityEngine.Random.Range(0, 3) == 0)
        {
            AnswerYes();
        }
        else if (UnityEngine.Random.Range(0, 3) == 0)
        {
            AnswerNo();
        }
        else
        {
            AnswerMock();
        }
    }

    private void AnswerMock()
    {
        this.PlayerState = "Mock";
        //yield return new WaitForSeconds(1f);
    }

    private void AnswerNo()
    {
        this.PlayerState = "No";
        //yield return new WaitForSeconds(1f);
    }

    private void AnswerYes()
    {
        this.PlayerState = "Yes";
        //yield return new WaitForSeconds(1f);
    }

    private void DropPhone()
    {
        this.CurrentState = State.DroppingPhone;
        //yield return new WaitForSeconds(1f);
        this.CurrentState = State.Idle;
    }

    private void MockPlayer()
    {
        this.CurrentState = State.Mocking;
        //yield return new WaitForSeconds(1f);
        this.CurrentState = State.Idle;
    }

    private void CallPhone()
    {
        this.PlayerState = "Calling...";
        do
        {
            if (UnityEngine.Random.Range(0, 100 / 25) == 0)
            {
                this.CurrentState = State.TakingPhone;
            }
            else
            {
                this.PlayerState = "Nope...";
            }
            //yield return new WaitForSeconds(0.5f);
        }
        while (this.CurrentState != State.TakingPhone);
        this.PlayerState = "Waiting to talk";
    }

    public enum State
    {
        Idle,
        Mocking,
        TakingPhone,
        Listening,
        Answering,
        DroppingPhone
    }
}
