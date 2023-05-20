using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Change_To_Underground : MonoBehaviour
{
    private Dialogue_academiallamas instance;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = GetComponent<Dialogue_academiallamas>();
        
        StartCoroutine(LoadYourAsyncScene());
    }

    IEnumerator LoadYourAsyncScene()
{
    yield return null;

    AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Scene_16_1");

    asyncOperation.allowSceneActivation = false;

    while (!asyncOperation.isDone)
    {
        if (asyncOperation.progress >= 0.9f)
        {
            if (instance.conversationEnded)
            {
                yield return new WaitForSeconds(1.5f); // Add a delay of 2 seconds
                asyncOperation.allowSceneActivation = true;
            }
        }

        yield return null;
    }
}
}
