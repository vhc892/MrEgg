using Hapiga.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;

public class Password : BaseUI
{
    List<string> inputFields = new List<string>();
    int currentInputIndex = 0;

    [SerializeField] string password;
    [SerializeField] TextMeshProUGUI[] txtPassword;
    [SerializeField] TextMeshProUGUI wrongPassword;
    // Start is called before the first frame update

    public void InputPassword(string input)
    {
        if (currentInputIndex >= txtPassword.Length) currentInputIndex = txtPassword.Length;
        txtPassword[currentInputIndex].text = input;
        inputFields.Add(input);
        currentInputIndex += 1;
        if (inputFields.Count == txtPassword.Length)
        {
            CheckPassword();
        }
    }

    void CheckPassword()
    {
        string inputPassword = "";
        foreach (string character in inputFields)
        {
            inputPassword = inputPassword + character;
        }
        Debug.Log(inputPassword);
        if (inputPassword == password)
        {
            Debug.Log("Correct password");
            Hide();
            GameEvents.UnlockDoor();
        }
        else
        {
            Debug.Log("Wrong password");
            currentInputIndex = 0;
            inputFields.Clear();
            foreach (var txt in txtPassword)
            {
                txt.text = "0";
            }
            TextMeshProUGUI wrongText = Instantiate(wrongPassword,transform);
            wrongText.text = "Wrong Password";
            Destroy(wrongText.gameObject, 1f);
        }
    }

    public void DeleteInput()
    {
        txtPassword[currentInputIndex-1].text = "0";
        inputFields.RemoveAt(currentInputIndex-1);
        currentInputIndex -= 1;
        if (currentInputIndex <= 0) currentInputIndex = 0;
    }
}
