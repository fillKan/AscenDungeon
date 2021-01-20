﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Battlehub.Dispatcher;
using UnityEngine.SceneManagement;
public class LoginUI : MonoBehaviour
{
    private static LoginUI instance;

   
    public GameObject loginObject;
  
  
    public GameObject errorObject;
    public GameObject nicknameObject;

  
    private InputField nicknameField;
    private Text errorText;
    private GameObject loadingObject;
    // private FadeAnimation fadeObject;

    private const byte ID_INDEX = 0;
    private const byte PW_INDEX = 1;
    private const string VERSION_STR = "Ver {0}";
    // Start is called before the first frame update

    void Awake()
    {
        instance = this;
    }

    public static LoginUI GetInstance()
    {
        if (instance == null)
        {
            Debug.LogError("LoginUI 인스턴스가 존재하지 않습니다.");
            return null;
        }
        return instance;
    }

    void Start()
    {
       
        nicknameField = nicknameObject.GetComponentInChildren<InputField>();
        errorText = errorObject.GetComponentInChildren<Text>();

        //loadingObject = GameObject.FindGameObjectWithTag("Loading");
       // loadingObject.SetActive(false);

      //  var fade = GameObject.FindGameObjectWithTag("Fade");
        //if (fade != null)
       // {
            //   fadeObject = fade.GetComponent<FadeAnimation>();
       // }

        var google = loginObject.transform.GetChild(0).gameObject;

#if UNITY_ANDROID
        google.SetActive(true);

#endif
    }


    public void TouchStart()
    {
       
        BackEndServerManager.GetInstance().BackendTokenLogin((bool result, string error) =>
        {
            Dispatcher.Current.BeginInvoke(() =>
            {
                if (result)
                {
                    ChangeLobbyScene();
                    return;
                }
                else
                if (!error.Equals(string.Empty))
                {
                    errorText.text = "유저 정보 불러오기 실패\n\n" + error;
                    errorObject.SetActive(true);
                    return;
                }
            
                loginObject.SetActive(true);
            });
        });

    }


   
   
   

    public void UpdateNickName()
    {
        if (errorObject.activeSelf)
        {
            return;
        }
        string nickname = nicknameField.text;
        if (nickname.Equals(string.Empty))
        {
            errorText.text = "닉네임을 먼저 입력해주세요";
            errorObject.SetActive(true);
            return;
        }
       // loadingObject.SetActive(true);
        BackEndServerManager.GetInstance().UpdateNickname(nickname, (bool result, string error) =>
        {
            Dispatcher.Current.BeginInvoke(() =>
            {
                if (!result)
                {
                    loadingObject.SetActive(false);
                    errorText.text = "닉네임 생성 오류\n\n" + error;
                    errorObject.SetActive(true);
                    return;
                }else
                    Debug.Log("씬");
                ChangeLobbyScene();
            });
        });
    }
    public void GoogleFederation()
    {
        if (errorObject.activeSelf)
        {
            return;
        }

       // loadingObject.SetActive(true);
        BackEndServerManager.GetInstance().GoogleAuthorizeFederation((bool result, string error) =>
        {
            Dispatcher.Current.BeginInvoke(() =>
            {
                if (!result)
                {
                    loadingObject.SetActive(false);
                    errorText.text = "로그인 에러\n\n" + error;
                    errorObject.SetActive(true);
                    return;
                }
                ChangeLobbyScene();
            });
        });
    }

    public void GuestLogin()
    {
        if (errorObject.activeSelf)
        {
            return;
        }

       // loadingObject.SetActive(true);
        BackEndServerManager.GetInstance().GuestLogin((bool result, string error) =>
        {
            Dispatcher.Current.BeginInvoke(() =>
            {
                if (!result)
                {
                    loadingObject.SetActive(false);
                    errorText.text = "로그인 에러\n\n" + error;
                    errorObject.SetActive(true);
                    return;
                }
                Debug.Log("씬");
                ChangeLobbyScene();
            });
        });
    }
    void ChangeLobbyScene()
    {
       
            GameManager.GetInstance().ChangeState(GameManager.GameState.Town, (bool isDone) =>
            {
                
            });
       
      

      

        {
            GameManager.GetInstance().ChangeState(GameManager.GameState.Town);
        }

    }
}
