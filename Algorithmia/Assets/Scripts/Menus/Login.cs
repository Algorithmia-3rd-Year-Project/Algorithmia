using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Login : MonoBehaviour
{

    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private Button loginButton;
    
    //private string loginEndPoint = "https://algorithmia-server.onrender.com/api/user/login";
    private string loginEndPoint = "localhost:4000/api/user/login";

    [SerializeField] private GameObject loginInterface;

    public string currentUsername;
    [SerializeField] private TMP_Text loggedUsernameText;
    
    public void OnLoginClick()
    {
        Debug.Log("singing in");
        loginButton.interactable = false;
        StartCoroutine(TryLogin());
    }

    private IEnumerator TryLogin()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;

        WWWForm form = new WWWForm();
        form.AddField("email", username);
        form.AddField("password", password);

        UnityWebRequest request = UnityWebRequest.Post(loginEndPoint, form);
        var handler = request.SendWebRequest();
        
        float startTime = 0.0f;
        while (!handler.isDone)
        {
            startTime += Time.deltaTime;

            if (startTime > 10.0f)
            {
                break;
            }

            yield return null;
        }
        
        if (request.result == UnityWebRequest.Result.Success)
        {
            PlayerAccount returnedPlayer = JsonUtility.FromJson<PlayerAccount>(request.downloadHandler.text);
            loginInterface.SetActive(false);
            Debug.Log(request.downloadHandler.text + " from db" + returnedPlayer._id + " " + returnedPlayer.email);

            currentUsername = returnedPlayer.email;
            loggedUsernameText.text = currentUsername;
            PlayerPrefs.SetString("PlayerID", returnedPlayer._id);
            PlayerPrefs.SetString("PlayerName", returnedPlayer.email);
            PlayerPrefs.Save();
            
        } else if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(loginEndPoint);
            Debug.Log("Error connecting to the server with yasintha");
            loginButton.interactable = true;
        }
        else
        {
            Debug.Log("Failure" + request.downloadHandler.text);
            loginButton.interactable = true;
        }

        
    }
}