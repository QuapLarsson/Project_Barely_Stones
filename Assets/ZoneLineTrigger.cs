using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneLineTrigger : MonoBehaviour
{
    [Header("Setup")]
    public SceneLoader sceneLoader;
    public string triggerTag;
    public string nextScene;
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered Trigger");
        if (other.gameObject.tag == triggerTag)
        {
            Debug.Log("Correct Tag");
            sceneLoader.Load(nextScene);
        }
    }
}
