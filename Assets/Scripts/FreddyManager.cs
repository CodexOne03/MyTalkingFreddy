using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Video;

public class FreddyManager : MonoBehaviour
{
    public static FreddyManager Instance;
    public Button Phone;
    public bool PhonePressed;
    public Button Talk;
    public bool TalkPressed;

    public Dictionary<State, string> videos;

    private State m_currentState;

    public State CurrentState
    {
        get
        {
            return m_currentState;
        }
        set
        {
            m_currentState = value;
            VideoPlayerScript.Instance.PlayVideo(GetVideoPath(value));
            Debug.Log("Freddy: " + value.ToString());
        }
    }

    void Start()
    {
        Instance = this;
        InitVideos();
        CurrentState = State.Idle;
        Debug.Log("Player: idle");
        Phone.onClick.AddListener(new UnityAction(() => { this.PhonePressed = true; }));
        Talk.onClick.AddListener(new UnityAction(() => { this.TalkPressed = true; }));
        StartCoroutine("Coroutine");
    }

    private void InitVideos()
    {
        this.videos = new Dictionary<State, string>();
        foreach (State state in Enum.GetValues(typeof(State)))
        {
            var path = Enum.GetName(typeof(State), state) + ".png";
            Debug.LogWarning($"Trying to load file \"{path}\"");
            this.videos.Add(state, @"C:\Users\ferra\TalkingFreddy\Assets\Resources\Videos\" + Enum.GetName(typeof(State), state) + ".mp4");

        }
    }

    private string GetVideoPath(State state)
    {
        return this.videos[state];
    }

    private IEnumerator Coroutine()
    {
        int merda = 0;
        while (true)
        {
            merda++;
            if (PhonePressed)
            {
                PhonePressed = false;
                Debug.Log("Player: Calling...");
                this.CurrentState = State.IgnoringPhone;
                do
                {
                    if (UnityEngine.Random.Range(0, 100 / 25) == 0)
                    {
                        this.CurrentState = State.TakingPhone;
                    }
                    else
                    {
                        Debug.Log("Player: Nope...");
                    }
                    yield return new WaitForSeconds(0.5f);
                }
                while (this.CurrentState != State.TakingPhone);
                Debug.Log("Player: Waiting to talk");
            }
            if (TalkPressed)
            {
                TalkPressed = false;
                if (this.CurrentState == State.Idle)
                {
                    this.CurrentState = State.Mocking;
                    //yield return new WaitForSeconds(1f);
                    this.CurrentState = State.Idle;
                }
                else if (this.CurrentState == State.TakingPhone)
                {
                    this.CurrentState = State.Listening;
                    //yield return new WaitForSeconds(1f);
                    if (UnityEngine.Random.Range(0, 100 / 25) == 0)
                    {
                        this.CurrentState = State.DroppingPhone;
                        //yield return new WaitForSeconds(1f);
                        this.CurrentState = State.Idle;
                    }
                    else
                    {
                        if (UnityEngine.Random.Range(0, 5) == 0)
                        {
                            Debug.Log("Player: Laugh");
                            this.CurrentState = State.AnsweringLaugh;
                            //yield return new WaitForSeconds(1f);
                        }
                        else if (UnityEngine.Random.Range(0, 5) == 0)
                        {
                            Debug.Log("Player: No");
                            this.CurrentState = State.AnsweringNo;
                            //yield return new WaitForSeconds(1f);
                        }
                        else if (UnityEngine.Random.Range(0, 5) == 0)
                        {
                            Debug.Log("Player: Mock");
                            this.CurrentState = State.AnsweringEw;
                            //yield return new WaitForSeconds(1f);
                        }
                        else if (UnityEngine.Random.Range(0, 5) == 0)
                        {
                            Debug.Log("Player: Drop");
                            this.CurrentState = State.DroppingPhone;
                            //yield return new WaitForSeconds(1f);
                        }
                        else
                        {
                            Debug.Log("Player: Yes");
                            this.CurrentState = State.AnsweringYes;
                            //yield return new WaitForSeconds(1f);
                        }
                        this.CurrentState = State.Idle;
                    }
                }
            }
            else if (this.CurrentState == State.TakingPhone && UnityEngine.Random.Range(0, 100 / 1) == 0)
            {
                Debug.Log("Player: Drop");
                this.CurrentState = State.DroppingPhone;
                //yield return new WaitForSeconds(1f);
            }
            //Input.GetKeyDown(KeyCode.S);
            //yield return new WaitForEndOfFrame();
        }
    }

    public enum State
    {
        Idle,
        Mocking,
        IgnoringPhone,
        TakingPhone,
        Listening,
        AnsweringYes,
        AnsweringNo,
        AnsweringEw,
        AnsweringLaugh,
        DroppingPhone
    }

    public class VideoInfo
    {
        string path;
        State linkedState;
        float duration;

        public VideoInfo(string path, State linkedState)
        {
            
        }
    }
}
