using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Panel currentPanel = null;

    public Panel loadingScreen;
    public Slider slider;
    private List<Panel> panelHistory = new List<Panel>();

    // Start is called before the first frame update
    void Start()
    {
        SetupPanels();
    }

    private void SetupPanels()
    {
        Panel[] panels = GetComponentsInChildren<Panel>();
        foreach(Panel panel in panels)
        {
            panel.Setup(this);
        }
        currentPanel.Show();
    }

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
            GoToPrevious();
    }

    public void GoToPrevious()
    {
        if(panelHistory.Count == 0)
        {
            OVRManager.PlatformUIConfirmQuit();
            return;
        }

        int lastIndex = panelHistory.Count - 1;
        SetCurrent(panelHistory[lastIndex]);
        panelHistory.RemoveAt(lastIndex);
    }

    public void SetCurrentWithHistory(Panel newPanel)
    {
        panelHistory.Add(currentPanel);
        SetCurrent(newPanel);
    }

    public void SetCurrent(Panel newPanel)
    {
        currentPanel.Hide();
        currentPanel = newPanel;
        currentPanel.Show();
    }

    public void LoadScene(string name)
    {
        StartCoroutine(LoadAsyncScene(name));
    }

    IEnumerator LoadAsyncScene(string name)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(name);

        SetCurrent(loadingScreen);
        while (!op.isDone)
        {
            float progress = Mathf.Clamp01(op.progress / 0.9f);
            slider.value = progress;
            yield return null;
        }
    }

    public void OpenURL(string url)
    {
        Application.OpenURL(url);
    }
}
