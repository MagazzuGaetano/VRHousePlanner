using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{

    private bool loadScene = false;

    [SerializeField]
    private int scene;
    [SerializeField]
    private Text loadingText;
    [SerializeField]
    private GameObject panel;

    // Updates once per frame
    void Update()
    {
        //quando sono nella scena dei progetti e la varibile canLoad di LoadAllProjects è = true allora mostro il caricamento
        if (SceneManager.GetActiveScene().buildIndex == 1 && LoadAllProjects.canLoad)
        {
            CheckAndStartLoadScene("Loading...");
        }
        //quando sono nella pagina di login e la variabile canLog è true carico la scena
        else if (SceneManager.GetActiveScene().buildIndex == 0 && Login.canLog) {
            Login.canLog = false;
            CheckAndStartLoadScene("Logging...");
        }
        //se sono nella scena del planning e canSave di SaveProject è true mostro lo screen 
        else if (SceneManager.GetActiveScene().buildIndex == 2 && SaveProject.canSave)
        {
            //una volta salvato reinposto la variabile
            SaveProject.canSave = false;
            CheckAndStartLoadScene("Saving...");
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1 && SetProjectName.canCreate)
        {
            SetProjectName.canCreate = false;
            CheckAndStartLoadScene("Generating...");
        }

            // If the new scene has started loading...
            if (loadScene == true)
        {
            panel.SetActive(true);
            // ...then pulse the transparency of the loading text to let the player know that the computer is still working.
            loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
        }
        else
        {
            panel.SetActive(false);
        }

    }


    // The coroutine runs on its own at the same time as Update() and takes an integer indicating which scene to load.
    IEnumerator LoadNewScene()
    {

        // This line waits for 3 seconds before executing the next line in the coroutine.
        // This line is only necessary for this demo. The scenes are so simple that they load too fast to read the "Loading..." text.
        yield return new WaitForSeconds(3);

        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        AsyncOperation async = Application.LoadLevelAsync(scene);

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!async.isDone)
        {
            yield return null;
        }

    }

    void CheckAndStartLoadScene(string str)
    {
        // If the player has pressed the button  and a new scene is not loading yet...
        if (loadScene == false)
        {
            // ...set the loadScene boolean to true to prevent loading a new scene more than once...
            loadScene = true;

            // ...change the instruction text to read "Loading..."
            loadingText.text = str;

            // ...and start a coroutine that will load the desired scene.
            StartCoroutine(LoadNewScene());

        }
    }
}