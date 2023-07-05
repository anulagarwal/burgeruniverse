using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class ExamplesManager : MonoBehaviour {

    [Space]
    public bool ThisIsBuildVersion = false;
    public bool ThisIsIntroScene = false;
    [Space]
    public Text UI_SceneTitle;
    public Text UI_SceneDescription;
    [Multiline(5)]
    public string SceneDescription;

    public bool ShowControls = true;
    public bool ThisIsCarScene = true;
    public bool ThisIsSmartTrackScene = false;
    public Rigidbody ball;
    public GameObject[] Cars;

    public MouseOrbit CameraOrbit;


    Vector3 StartPos = Vector3.zero;

    private string Controls = "Use 1, 2, 3, 4 to change vehicle.\nPress Q to restart vehicle.\nUse WASD & Space to Drive.";
    private string Controls2 = "Use WASD and LShift to Move.";
    private string Controls3 = "Use WASD to Move.";

#if UNITY_EDITOR
    void Awake()
    {
        if (!ThisIsIntroScene)
            return;
        GenerateScenesToBuild();
    }


    public static void GenerateScenesToBuild()
    {
        try
        {
            UnityEditor.EditorBuildSettings.scenes = new UnityEditor.EditorBuildSettingsScene[0];
            string[] tempPaths = Directory.GetFiles(Application.dataPath + "/MeshTracker_Package/Examples/Scenes/", "*.unity");
            List<UnityEditor.EditorBuildSettingsScene> sceneAr = new List<UnityEditor.EditorBuildSettingsScene>();

            for (int i = 0; i < tempPaths.Length; i++)
            {
                string path = tempPaths[i].Substring(Application.dataPath.Length - "Assets".Length);
                path = path.Replace('\\', '/');

                sceneAr.Add(new UnityEditor.EditorBuildSettingsScene(path, true));
            }
            UnityEditor.EditorBuildSettings.scenes = sceneAr.ToArray();
        }
        catch (IOException e)
        {
            Debug.Log("UNABLE TO LOAD EXAMPLE SCENES! Exception: " + e.Message);
        }

    }
#endif


    private void Start ()
    {
        if (ThisIsIntroScene)
            return;
        if (ThisIsCarScene)
        {
            StartPos = Cars[0].transform.position;
            ActivateCar(0);
        }

        UI_SceneTitle.text = SceneManager.GetActiveScene().name;
        UI_SceneDescription.text = SceneDescription;

        if (!ShowControls)
        {
            if(!ThisIsBuildVersion)
                UI_SceneDescription.text += "\n\nR = Restart Scene, Esc = Back To Intro";
            return;
        }

        if (ThisIsCarScene)
            UI_SceneDescription.text += "\n" + Controls;
        else if(!ThisIsSmartTrackScene)
            UI_SceneDescription.text += "\n" + Controls2;
        else
            UI_SceneDescription.text += "\n" + Controls3;

        if (!ThisIsBuildVersion)
            UI_SceneDescription.text += "\n\nR = Restart Scene, Esc = Back To Intro";
        else
            UI_SceneDescription.text += "\n\nR = Restart Scene, Esc = Quit";
    }

    public void SwitchBalls(Rigidbody b)
    {
        ball = b;
    }

    private void Update()
    {
        if (!ThisIsBuildVersion)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                UnityEngine.SceneManagement.SceneManager.LoadScene("Introduction");
            if (Input.GetKeyDown(KeyCode.R))
                UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
            if (Input.GetKeyDown(KeyCode.R))
                UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }

        if(ThisIsSmartTrackScene)
        {
            Vector3 dir = new Vector3(Input.GetAxis("Horizontal") * 10, 0, Input.GetAxis("Vertical") * 10);
            dir = Camera.main.transform.TransformDirection(dir);
            ball.AddForce(dir, ForceMode.Acceleration);
        }

        if(Input.GetKeyDown(KeyCode.F1))
        {
            if (UI_SceneTitle.transform.root.gameObject.activeInHierarchy)
                UI_SceneTitle.transform.root.gameObject.SetActive(false);
            else
                UI_SceneTitle.transform.root.gameObject.SetActive(true);
        }

        if (ThisIsIntroScene)
            return;
        if (!ThisIsCarScene)
            return;

        if(Input.GetKeyDown(KeyCode.Alpha1))
            ActivateCar(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            ActivateCar(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            ActivateCar(2);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            ActivateCar(3);

        if(Input.GetKeyDown(KeyCode.Q))
        {
            foreach (GameObject car in Cars)
            {
                if (car.activeInHierarchy)
                {
                    car.transform.position = StartPos;
                    car.transform.rotation = Quaternion.identity;
                }
            }
        }
    }

    public void ActivateCar(int Index)
    {
        foreach (GameObject car in Cars)
            car.SetActive(false);

        try
        {
            Cars[Index].SetActive(true);
            CameraOrbit.target = Cars[Index].transform;
        }
        catch
        { }
    }

    public void _LoadLevel(string LVL_Name)
    {
        if (SceneManager.sceneCountInBuildSettings > 1)
        {
            try
            {
                SceneManager.LoadScene(LVL_Name);
            }
            catch
            {
                Debug.LogError("Can't load level. Please press stop and press play again to refresh Build Settings. If still nothing happens, check if the path exists : "+ Application.dataPath + "/MeshTracker_Package/Examples/Scenes/");

            }
        }
        else Debug.Log("Can't load level. Please press stop and press play again to refresh Build Settings.");
    }
    public void OpenDocumentation()
    {
        try
        {
            System.Diagnostics.Process.Start(Application.dataPath + "/MeshTracker_Package/MeshTracker_Documentation.pdf");
        }
        catch
        {
            Debug.LogError("Documentation could not be found! Required path: " + Application.dataPath + "/MeshTracker_Package/MeshTracker_Documentation.pdf");
        }
    }
    public void OpenPortfolio()
    {
        Application.OpenURL("https://matejvanco.com");
    }
    public void OpenDocs()
    {
        Application.OpenURL("https://www.youtube.com/watch?v=EtQ99k2em38");
    }
    public void OpenDiscord()
    {
        Application.OpenURL("https://discord.gg/Ztr8ghQKqC");
    }

    public void OpenUrl(string url)
    {
        Application.OpenURL(url);
    }
}
