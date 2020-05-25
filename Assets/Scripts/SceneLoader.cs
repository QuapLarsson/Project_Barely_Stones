using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 0.5f;

    public void Load(string fileName)
    {
        StartCoroutine(LoadScene(fileName));
    }

    IEnumerator LoadScene(string fileName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(fileName);
    }
}
