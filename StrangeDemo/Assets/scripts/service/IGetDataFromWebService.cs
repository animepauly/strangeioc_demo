using System;
using UnityEngine;
using strange.extensions.dispatcher.eventdispatcher.api;

public interface IGetDataFromWebService
{
	GameObject contextView{get;set;}

	void RequestData(string url);
	ReturnWebDataSignal ReturnWebDataSig{get;set;}
}
