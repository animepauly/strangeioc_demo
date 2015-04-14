/// An example Command
/// ============================
/// This Command puts a new ExampleView into the scene.
/// Note how the ContextView (i.e., the GameObject our Root was attached to)
/// is injected for use.
/// 
/// All Commands must override the Execute method. The Command is automatically
/// cleaned up when Execute has completed, unless Retain is called (more on that
/// in the OpenWebPageCommand).

using System;
using UnityEngine;
using strange.extensions.command.impl;
using strange.extensions.mediation.impl;
using strange.extensions.dispatcher.eventdispatcher.impl;

public class GetFriendsListCommand : Command
{
	[Inject]
	public int ListLimitInt{get;set;}

	[Inject]
	public FulfillFriendsListSignal FulfillFriendsListSig{get;set;}

	[Inject]
	public IGetDataFromWebService GetDataFromWebServ{get;set;}
	
	public override void Execute()
	{
		Debug.Log ("GetFriendsListCommand::Execute called...");
		Retain();
		GetDataFromWebServ.ReturnWebDataSig.AddListener(_returnWebDataHandler);
		GetDataFromWebServ.RequestData("http:://www.someservice.com/friendslist");
	}

	private void _returnWebDataHandler(object __dataObj)
	{
		string[] __friendsStrArr = __dataObj as string[];
		string[] __newFriendStrArr = new string[ListLimitInt];

		for (int __i = 0; __i < ListLimitInt; __i++)
		{
			__newFriendStrArr[__i] = __friendsStrArr[__i];
		}

		FulfillFriendsListSig.Dispatch(__newFriendStrArr);
		Release();
	}
}
