using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Change_Scene3 : MonoBehaviour
{
    private Dialogue_Profesor2 instance;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = GetComponent<Dialogue_Profesor2>();
        
        StartCoroutine(LoadYourAsyncScene());
    }

    IEnumerator LoadYourAsyncScene()
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Scene_3");
        
        asyncOperation.allowSceneActivation = false;
        
        while (!asyncOperation.isDone)
        {

            if (asyncOperation.progress >= 0.9f)
            {
                if (instance.conversationEnded)

                    asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
