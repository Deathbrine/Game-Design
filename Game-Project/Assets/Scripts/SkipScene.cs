using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipScene : MonoBehaviour
{
    public void SkipCredits()
    {
        SceneManager.LoadScene(0);
    }
}
