using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using LitJson;
using UnityEngine.SceneManagement;
using System.Net.Security;
using System.Security.Cryptography;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System;

public class Login : MonoBehaviour
{
    public Button register;
    public Button btn;
    public InputField uid;
    public InputField pwd;
    public Text usernotfound;
    public Text wrongpwd;

    public string token = "";
    public static bool canLog = false;

    // Use this for initialization
    void Start()
    {
        usernotfound.text = "";
        wrongpwd.text = "";

        btn.onClick.AddListener(() =>
        {

            WebClient wc = new WebClient();
        
            wc.Headers["Content-Type"] = "application/json";

            var data = new { name = uid.text, password = pwd.text};
            string json = JsonMapper.ToJson(data);
            byte[] _byte = System.Text.Encoding.ASCII.GetBytes(json);

            byte[] reply = wc.UploadData(SharedVariables.address + "/api/token", "POST", _byte);

            string r = System.Text.Encoding.ASCII.GetString(reply);
            Response res = JsonMapper.ToObject<Response>(r);

            canLog = res.success;

            if (!res.success)
            {
                if (res.message == "Authentication failed. User not found.")
                {
                    uid.GetComponent<Outline>().enabled = true;
                    usernotfound.text = "user not found";
                }
                else if (res.message == "Authentication failed. Wrong password.")
                {
                    uid.GetComponent<Outline>().enabled = false;
                    pwd.GetComponent<Outline>().enabled = true;
                    usernotfound.text = "";
                    wrongpwd.text = "wrong password";
                }
            }
            else
            {
                uid.GetComponent<Outline>().enabled = false;
                pwd.GetComponent<Outline>().enabled = false;

                usernotfound.text = "";
                wrongpwd.text = "";

                token = res.token;

                User _user = new User();
                _user._id = GetUserData(token)._id;
                _user.name = GetUserData(token).name;
                _user.password = GetUserData(token).password;
                _user.admin = GetUserData(token).admin;

                SharedVariables.user = _user;
                SharedVariables.token = token;
            }

        });

        register.onClick.AddListener(() =>
        {
            Application.OpenURL(SharedVariables.address);
        });
    }

    Doc GetUserData(string token)
    {
        var parts = token.Split('.');
        if (parts.Length > 2)
        {
            var decode = parts[1];
            var padLength = 4 - decode.Length % 4;
            if (padLength < 4)
            {
                decode += new string('=', padLength);
            }
            var bytes = System.Convert.FromBase64String(decode);
            var userInfo = System.Text.ASCIIEncoding.ASCII.GetString(bytes);
            Doc x = JsonMapper.ToObject<Doc>(userInfo);

            return x;
        }
        return null;
    }

    public string CalculateMD5Hash(string input)
    {

        // step 1, calculate MD5 hash from input

        MD5 md5 = System.Security.Cryptography.MD5.Create();

        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);

        byte[] hash = md5.ComputeHash(inputBytes);

        // step 2, convert byte array to hex string

        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < hash.Length; i++)
        {
            sb.Append(hash[i].ToString("x2"));
        }

        return sb.ToString();

    }

    public bool MyRemoteCertificateValidationCallback(System.Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        bool isOk = true;
        // If there are errors in the certificate chain, look at each error to determine the cause.
        if (sslPolicyErrors != SslPolicyErrors.None)
        {
            for (int i = 0; i < chain.ChainStatus.Length; i++)
            {
                if (chain.ChainStatus[i].Status != X509ChainStatusFlags.RevocationStatusUnknown)
                {
                    chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
                    chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
                    chain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan(0, 1, 0);
                    chain.ChainPolicy.VerificationFlags = X509VerificationFlags.AllFlags;
                    bool chainIsValid = chain.Build((X509Certificate2)certificate);
                    if (!chainIsValid)
                    {
                        isOk = false;
                    }
                }
            }
        }
        return isOk;
    }
}
