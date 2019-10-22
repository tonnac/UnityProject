using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject menuPage;
    public Text buttonText;

    public void OpenPage() {
        menuPage.SetActive(true);
        buttonText.text = "Close";
    }
}
