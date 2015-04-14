/// The only change in StartCommand is that we extend Command, not EventCommand

using System;
using UnityEngine;
using strange.extensions.context.api;
using strange.extensions.command.impl;
using strange.extensions.dispatcher.eventdispatcher.impl;

public class StartCommand : Command
{
	
	[Inject(ContextKeys.CONTEXT_VIEW)]
	public GameObject contextView{get;set;}

	private string _prefabPathStr = "prefabs/";

	private Canvas _rootCanvas;
	
	public override void Execute()
	{
		_rootCanvas = contextView.GetComponentInChildren<Canvas>();

		GameObject __go = UnityEngine.Object.Instantiate(Resources.Load(_prefabPathStr+"DemoViewPanel")) as GameObject;
		__go.name = "DemoView";
		__go.AddComponent<DemoView>();
		__go.transform.SetParent(_rootCanvas.transform, false);
	}
}

