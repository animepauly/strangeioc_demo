using System;
using System.Collections;
using UnityEngine;
using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;

public class MockGetDataFromWebService : IGetDataFromWebService
{
	[Inject(ContextKeys.CONTEXT_VIEW)]
	public GameObject contextView{get;set;}
	
	//The interface demands this signal
	[Inject]
	public ReturnWebDataSignal ReturnWebDataSig{get;set;}
	
	private string _url;
	
	public MockGetDataFromWebService ()
	{
	}
	
	public void RequestData(string url)
	{
		Debug.Log("ExampleService::Request called with "+url);
		_url = url;
		
		MonoBehaviour root = contextView.GetComponent<AppRoot>();
		root.StartCoroutine(waitASecond());
	}

	private string[] _mockGetFriendsList()
	{
		return new string[]{"bobby", "brenna", "jaryd", "chris", "andy", "rachel", "bryan", "bridget", "nick", "willy", "kevin", "danny", "stephanie"};
	}
	
	private IEnumerator waitASecond()
	{
		Debug.Log("ExampleService::waitASecond");
		yield return new WaitForSeconds(1f);
		
		//Pass back some fake data via a Signal
		ReturnWebDataSig.Dispatch(_mockGetFriendsList());
	}
}

