using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowBehaviour 
{
    public GameObject gameObject { get; set; }
    public Transform transform { get; set; }
    public Canvas canvas { get; set; }
    public string name { get; set; }
    public bool isActive { get; set; }

    public virtual void Init() { }
    public virtual void ShowMe() { }
    public virtual void OnUpdate() { }
    public virtual void HideMe() { }
    public virtual void OnDestroy() { }
    public virtual void SetActive() { }



}
