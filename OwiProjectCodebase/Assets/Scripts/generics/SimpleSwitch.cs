using System;
using UnityEngine;
using UnityEngine.Events;


public class SimpleSwitch : Actionable
{
    [SerializeField]
    private int minCollidersToTrigger = 0;

    [SerializeField]
    private UnityEvent onStart;

    [SerializeField]
    private UnityEvent onTriggerEnter;

    [SerializeField]
    private UnityEvent onTriggerExit;


    [SerializeField]
    private UnityEvent onAct;


    private int items = 0;

    private void Start(){
        onStart.Invoke();
    }

    public override void Act()
    {
        if(items >= minCollidersToTrigger){
            onAct.Invoke();
        }
    }

    private void OnTriggerEnter2D()
    {
        items++; 
        if(items >= minCollidersToTrigger){
            onTriggerEnter.Invoke();
        }
    }

    private void OnTriggerEnter()
    {
        items++; 
        if(items >= minCollidersToTrigger){
            onTriggerEnter.Invoke();
        }
    }

    private void OnTriggerExit2D()
    {
        items--;
        if(items <= minCollidersToTrigger){
            onTriggerExit.Invoke();
        }
    }

    private void OnTriggerExit()
    {
        items--;
        if(items <= minCollidersToTrigger){
            onTriggerExit.Invoke();
        }
    }
}
