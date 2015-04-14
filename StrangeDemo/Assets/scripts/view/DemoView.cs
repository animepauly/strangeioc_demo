/// The view is where you program and configure the particulars of an item
/// in a scene. For example, if you have a GameObject with buttons and a
/// test readout, wire all that into this class.
/// 
/// By default, Views do not have access to the common Event bus. While you
/// could inject it, we STRONGLY recommend against doing this. Views are by
/// nature volatile, possibly the piece of your app most likely to change.
/// Mediation mapping allows you to automatically attach a 'Mediator' class
/// whose responsibility it is to connect the View to the rest of the app.
/// 
/// Building a view in code here. Ordinarily, you'd do this in the scene.
/// You could argue that this code is kind of messy...not ideal for a demo...
/// but that's kind of the point. View code is often highly volatile and
/// reactive. It gets messy. Let your view be what it needs to be while
/// insulating the rest of your app from this chaos.

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using strange.extensions.context.api;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;

public class DemoView : AppView
{
	private Transform _transform;
	private GameObject _overlayPanelGO;
	private Button _toggleOverlayBtn;
	private Button _showFriendsBtn;
	private Text _friendsListText;
	
	//Contstructor
	public DemoView()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	//where I initiate all my variables and UI elements
	override public void init()
	{
		Debug.Log("DemoView::init called...");
		_transform = transform;
		_overlayPanelGO = _transform.Find("OverlayPanel").gameObject;
		_toggleOverlayBtn = _transform.Find("ToggleOverlayBtn").GetComponent<Button>();
		_showFriendsBtn = _transform.Find("ShowFriendsBtn").GetComponent<Button>();
		_friendsListText = _transform.Find ("FriendsListText").GetComponent<Text>();

		base.init();
		_registerListeners();
	}

	public void ToggleOverlay()
	{
		Debug.Log ("ToggleOverlay called...");
		if (_overlayPanelGO.activeSelf)
		{
			_overlayPanelGO.SetActive(false);
		}
		else
		{
			_overlayPanelGO.SetActive(true);
		}
	}

	public void ClearFriendsList ()
	{
		_friendsListText.text = String.Empty;
	}

	public void UpdateFriendsList (string[] __friendStrArr)
	{
		int __lengthInt = __friendStrArr.Length;
		for (int __i = 0; __i < __lengthInt; __i++)
		{
			_friendsListText.text += __friendStrArr[__i] + "\n";
		}
	}
	
	private void _registerListeners ()
	{
		AddButtonClickListener(_toggleOverlayBtn, "toggleOverlay", null);
		AddButtonClickListener(_showFriendsBtn, "showFriends", null);
	}
}
