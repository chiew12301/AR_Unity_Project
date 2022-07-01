using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerHandler : MonoBehaviour
{
    private static SceneManagerHandler _INSTANCE;
    private Coroutine m_changeSceneCO = null;

    private void Awake()
    {
        if(null == _INSTANCE)
        {
            _INSTANCE = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public SceneManagerHandler GetInstance()
    {
        return _INSTANCE;
    }

    public void ChangeScene(string sceneName)
    {
        if(null != this.m_changeSceneCO)
        {
            this.StopCoroutine(this.m_changeSceneCO);
            this.m_changeSceneCO = null;
        }
        this.m_changeSceneCO = this.StartCoroutine(this.ChangeSceneCO(sceneName));
    }

    private IEnumerator ChangeSceneCO(string sceneName)
    {
        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(sceneName);
    }
}
