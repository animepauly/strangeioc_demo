/// Make your Mediator as thin as possible. Its function is to mediate
/// between view and app. Don't load it up with behavior that belongs in
/// the View (listening to/controlling interface), Commands (business logic),
/// Models (maintaining state) or Services (reaching out for data).

using System;
using UnityEngine;
using UnityEngine.UI;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;

public class DemoMediator : Mediator
{
	//This is how your Mediator knows about your View.
	[Inject]
	public DemoView view{ get; set;}

	[Inject]
	public GetFriendsListSignal GetFriendsListSig{get;set;}

	[Inject]
	public FulfillFriendsListSignal FulfillFriendsListSig{get;set;}
	
	
	public override void OnRegister()
	{
		Debug.Log("DemoMediator:: OnRegister");
		//Listen to the view for a Signal
		_addListeners();
		view.init();
	}
	
	private void onButtonClicked(String __btnNameStr, object __data)
	{
		Debug.Log("DemoMediator::2. onButtonClicked detected");
		Debug.Log(__btnNameStr);
		switch (__btnNameStr)
		{
			case "toggleOverlay":
				view.ToggleOverlay();
			break;

			case "showFriends":
				view.ClearFriendsList();
				GetFriendsListSig.Dispatch(10);
			break;
		}
		
	}
	
	private void _addListeners()
	{
		view.ViewButtonClickSignal.AddListener(onButtonClicked);
		FulfillFriendsListSig.AddListener(_fulfillFriendsListHandler);
	}

	private void _fulfillFriendsListHandler(string[] __friendStrArr)
	{
		view.UpdateFriendsList(__friendStrArr);
	}

	private void _removeListeners()
	{
		view.ViewButtonClickSignal.RemoveListener(onButtonClicked);
	}
	
	public override void OnRemove()
	{
		Debug.Log("Mediator OnRemove");
		//Clean up listeners just as you do with EventDispatcher
		_removeListeners();
	}
}
