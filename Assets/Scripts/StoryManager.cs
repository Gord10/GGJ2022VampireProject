using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    public string storyFileName;
    public string nextSceneName;

    private string[] storyLines;
    private Text text;
    private int textCounter = 0;
    private void Awake()
    {
        TextAsset mytxtData = (TextAsset)Resources.Load(storyFileName);
        storyLines = mytxtData.ToString().Split('\n');

        text = FindObjectOfType<Text>();
        text.text = storyLines[textCounter];
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown && Time.timeSinceLevelLoad > 0.4f)
        {
            textCounter++;

            if(textCounter >= storyLines.Length)
            {
                SceneManager.LoadScene(nextSceneName);
                return;
            }

            text.text = storyLines[textCounter];
        }
    }
}
