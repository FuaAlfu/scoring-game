using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameSession : MonoBehaviour
{
    public static GameSession Instance;

    [SerializeField]
    int levelIndex;

    [SerializeField]
    Text score;
    int scoring;

    [SerializeField]
    GameObject levelComplateTxt, levelFailed;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        score.text = scoring.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScoreToAdd(int scoreToAdd, int limit)
    {
        scoring += scoreToAdd;
        score.text = scoring.ToString();

        if (scoring >= limit)
            StartCoroutine(LevelComplateTxt());
            Invoke("LevelLoader", 1f);
    }

    public void LevelFailedLoader()
    {
        StartCoroutine(LevelFailed());
        Invoke("LevelLoader", 1f);
    }

    public void LevelLoader()
    {
        SceneManager.LoadScene(levelIndex);
    }


    IEnumerator LevelComplateTxt()
    {
        levelComplateTxt.SetActive(true);
        yield return new WaitForSeconds(1f);
        levelComplateTxt.SetActive(false);
    }

    IEnumerator LevelFailed()
    {
        levelFailed.SetActive(true);
        yield return new WaitForSeconds(1f);
        levelFailed.SetActive(false);
    }
}
