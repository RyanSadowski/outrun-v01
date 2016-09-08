using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class EventTest : MonoBehaviour {

    private UnityAction dialogShowListener;
    private UnityAction dialogHideListener;

    void Awake ()
    {
        dialogShowListener = new UnityAction (ShowDialog);
        dialogHideListener = new UnityAction (HideDialog);
    }

    void OnEnable ()
    {
        EventManager.StartListening ("test", dialogShowListener);
        EventManager.StartListening ("endDialog", dialogHideListener);
    }

    void OnDisable ()
    {
        EventManager.StopListening ("test", dialogShowListener);
        EventManager.StopListening ("endDialog", dialogHideListener);
    }

    void ShowDialog ()
    {
        Debug.Log ("Some Function was called!");
        transform.GetChild(0).gameObject.SetActive(true);
    }
    void HideDialog()
    {
        Debug.Log("hide Dialog");
        transform.GetChild(0).gameObject.SetActive(false);
    }
}