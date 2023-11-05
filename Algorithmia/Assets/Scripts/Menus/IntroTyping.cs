using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroTyping : MonoBehaviour
{
    public float typingSpeed = 0.05f;

    [SerializeField] private TextMesh gameName;
    
    private string fullText;
    private string currentText = "";
    private float timer = 0;

    private float currentTime;

    private void Start()
    {
        fullText = gameName.text;
        gameName.text = "";
    }

    private void Update()
    {
        ReWrite();
    }

    private void ReWrite()
    {
        if (currentText.Length < fullText.Length)
        {
            timer += Time.deltaTime;
            if (timer >= typingSpeed)
            {
                currentText = fullText.Substring(0, currentText.Length + 1);
                gameName.text = currentText;
                timer = 0;
            }
        }

        if (currentText.Length == fullText.Length)
        {
            currentTime += Time.deltaTime;
            if (currentTime > 5f)
            {
                gameName.text = "";
                currentText = "";
                ReWrite();
                currentTime = 0;
            }
            
        }
        
        
    }
}
