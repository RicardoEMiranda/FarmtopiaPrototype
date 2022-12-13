using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoginManager : MonoBehaviour  {

    [SerializeField] public GameObject signupPanel;
    [SerializeField] public GameObject errorPanel;
    [SerializeField] public TextMeshProUGUI emailInput;
    public string email;
    private string userName;

    private void GetUserName()  {

    }

    private void OnLogin() {

        //Check that string entered is a proper email string

    }

    public void OnSignUpLink () {

        //Activate the SignupPanel
        signupPanel.SetActive(true);
    }

    public void OnSignUpButton() {

        email = emailInput.text;

        if (email.Contains('@'))  {
            Debug.Log("Yes");
            errorPanel.SetActive(false);
            //Check that the email entered is properly formatted
            string[] emailParts = email.Split('@');
            string beforeAt = emailParts[0];
            string afterAt = emailParts[1];
        }
        if(!email.Contains('@'))  {
            errorPanel.SetActive(true);
            //emailInput.text = "";
            Debug.Log("Improper email format");
        }

        
    }

    public void OnGoBackToLogin()  {
        signupPanel.SetActive(false);
    }

    
}
