using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using strange.extensions.context.api;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;

public class AppView : View
{
	[Inject]
	public ButtonClickSignal ViewButtonClickSignal {get; set;}
	
	[Inject(ContextKeys.CONTEXT_VIEW)]
	public GameObject contextView{get;set;}
	
	protected List<Button> _btnList;
	
	//Consttructor
	public AppView()
	{
	}
	
	public virtual void init()
	{
		_btnList = new List<Button>();
	}
	
	public virtual void ButtonClick(String __btnNameStr, object __data)
	{
		Debug.Log ("AppView:: 1. "+__btnNameStr+" button clicked...");
		ViewButtonClickSignal.Dispatch(__btnNameStr, __data);
	}
	
	public virtual void AddButtonClickListener(Button __btn, string __btnParamStr, object __data)
	{
		Debug.Log("AddButtonClickListener::called..."+__btn.name);
		__btn.onClick.AddListener(() => { ButtonClick(__btnParamStr, __data); });
		__btn.interactable = true;
		_btnList.Add(__btn);
	}
	
	public virtual void RemoveButtonClickListener(Button __btn)
	{
		__btn.onClick.RemoveAllListeners();
		__btn.interactable = false;
		_btnList.Remove(__btn);
	}
	
	protected override void OnDestroy ()
	{
		Debug.Log("AppView::OnDestroy called...");
		//Clear up anything here
		if (_btnList != null)
		{
			int __lengthInt = _btnList.Count;
			Button __btn;
			
			for (int i = 0; i < __lengthInt; i++)
			{
				__btn = _btnList[i];
				__btn.onClick.RemoveAllListeners();
			}
			
			_btnList.Clear();
			_btnList = null;
		}
		
		base.OnDestroy();
	}
}
