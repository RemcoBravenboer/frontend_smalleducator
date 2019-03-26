using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class AskQuestionScript : MonoBehaviour
{
    public Dropdown dropdownSelect;
    public InputField inputQuestion;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void btnSubmitQuestionClick()
    {
        Debug.Log("btn clicked");
        if(inputQuestion.text.ToString() != "")
        {
            StartCoroutine(postQuestion(inputQuestion.text.ToString(), dropdownSelect.options[dropdownSelect.value].text));
        }
    }

    private void Init()
    {
        dropdownSelect = GameObject.FindGameObjectWithTag("dropdownSubject").GetComponent<Dropdown>();
        inputQuestion = GameObject.FindGameObjectWithTag("txtQuestion").GetComponent<InputField>();
    }

    IEnumerator postQuestion(string questionText, string dropdownSelect)
    {
        //Initiating the WebRequest
        var uwr = new UnityWebRequest("http://localhost:8080/question", "POST");

        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes("{\"lessonName\":\"" + dropdownSelect + "\", \"question\": \"" + questionText + "\"}");

        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.Log(uwr.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }

    }
}
