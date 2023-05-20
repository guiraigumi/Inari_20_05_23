using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnCollision : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StartCoroutine(LoadSceneAsyncCoroutine());
        }
    }

    IEnumerator LoadSceneAsyncCoroutine()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Scene_14");
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.9f)
            {
                // Add your own condition for allowing the scene to activate here
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}