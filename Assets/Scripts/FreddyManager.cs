using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FreddyManager : MonoBehaviour
{
    public static FreddyManager Instance;
    public Button Phone;
    public bool PhonePressed;
    public Button Talk;
    public bool TalkPressed;

    private SpriteRenderer spriteRenderer;

    public List<Sprite> sprites;

    private State m_currentState;

    private bool waiting;

    public State CurrentState
    {
        get
        {
            return m_currentState;
        }
        set
        {
            m_currentState = value;
            spriteRenderer.sprite = GetSprite(value);
            Debug.Log("Freddy: " + value.ToString());
        }
    }

    void Start()
    {
        Instance = this;
        spriteRenderer = GetComponent<SpriteRenderer>();
        //LoadSprites();
        CurrentState = State.Idle;
        Debug.Log("Player: idle");
        Phone.onClick.AddListener(new UnityAction(() => { this.PhonePressed = true; }));
        Talk.onClick.AddListener(new UnityAction(() => { this.TalkPressed = true; }));
        StartCoroutine("Coroutine");
    }

    private void LoadSprites()
    {
        this.sprites = new List<Sprite>();
        foreach (State state in Enum.GetValues(typeof(State)))
        {
            var path = Enum.GetName(typeof(State), state) + ".png";
            Debug.LogWarning($"Trying to load file \"{path}\"");
            this.sprites.Add(Resources.Load<Sprite>(path));
        }
    }

    private Sprite GetSprite(State state)
    {
        return this.sprites[(int)state];
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
                    yield return new WaitForSeconds(1f);
                    this.CurrentState = State.Idle;
                }
                else if (this.CurrentState == State.TakingPhone)
                {
                    this.CurrentState = State.Listening;
                    yield return new WaitForSeconds(1f);
                    if (UnityEngine.Random.Range(0, 100 / 25) == 0)
                    {
                        this.CurrentState = State.DroppingPhone;
                        yield return new WaitForSeconds(1f);
                        this.CurrentState = State.Idle;
                    }
                    else
                    {
                        if (UnityEngine.Random.Range(0, 5) == 0)
                        {
                            Debug.Log("Player: Laugh");
                            this.CurrentState = State.AnsweringLaugh;
                            yield return new WaitForSeconds(1f);
                        }
                        else if (UnityEngine.Random.Range(0, 5) == 0)
                        {
                            Debug.Log("Player: No");
                            this.CurrentState = State.AnsweringNo;
                            yield return new WaitForSeconds(1f);
                        }
                        else if (UnityEngine.Random.Range(0, 5) == 0)
                        {
                            Debug.Log("Player: Mock");
                            this.CurrentState = State.AnsweringEw;
                            yield return new WaitForSeconds(1f);
                        }
                        else if (UnityEngine.Random.Range(0, 5) == 0)
                        {
                            Debug.Log("Player: Drop");
                            this.CurrentState = State.DroppingPhone;
                            yield return new WaitForSeconds(1f);
                        }
                        else
                        {
                            Debug.Log("Player: Yes");
                            this.CurrentState = State.AnsweringYes;
                            yield return new WaitForSeconds(1f);
                        }
                        this.CurrentState = State.Idle;
                    }
                }
            }
            else if (this.CurrentState == State.TakingPhone && UnityEngine.Random.Range(0, 100 / 1) == 0)
            {
                Debug.Log("Player: Drop");
                this.CurrentState = State.DroppingPhone;
                yield return new WaitForSeconds(1f);
            }
            //Input.GetKeyDown(KeyCode.S);
            yield return new WaitForEndOfFrame();
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
}
