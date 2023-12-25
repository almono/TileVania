using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    public static ScenePersist instance;

    private void Awake()
    {
        // this way of singleton can be used for persistance because
        // the changes in it might give differences between old and new instance
        if(FindObjectsOfType<ScenePersist>().Length > 1)
        {
            Destroy(gameObject);
        } else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetScenePersist()
    {
        // reset scene objects that should persist
        Destroy(gameObject);
    }
}
