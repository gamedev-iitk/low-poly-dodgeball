using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;

    [SerializeField]
    private CanvasGroup EndUI;

    [SerializeField]
    private CanvasGroup HUD;

    void Start()
    {
        Cursor.visible = false;
    }

    public GameObject GetPlayer()
    {
        return Player;
    }

    public GameObject GetEndUIObject()
    {
        return EndUI.gameObject;
    }

    public GameObject GetHUDObject()
    {
        return HUD.gameObject;
    }
}
