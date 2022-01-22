using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
public class ScreenshotTaker : MonoBehaviour
{
    public static int screenshotId = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            string sceneName = SceneManager.GetActiveScene().name;
            string resolution = Screen.currentResolution.width + "x" + Screen.currentResolution.height;
            string folder = "Screenshots";
            if(!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string fileName = folder + "/" + sceneName + " " + resolution + " " + screenshotId + ".png";
            ScreenCapture.CaptureScreenshot(fileName);
            print("Captured " + fileName);
        }
    }
}
