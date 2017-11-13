using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NCMB;
using System;
using System.Reflection;

public class NCMBTestSettings
{
	public static readonly string APP_KEY = "d55786e0bd4263018ca04ac9af7cbad153d757879d8cd296a9c3075a482ad845";
	public static readonly string CLIENT_KEY = "ba82f3ca775edf2c39b53bc7ecaa40eb2a7dd35f4724f489b067ec93895e21de";
	public static readonly string DOMAIN_URL = "http://localhost:3000";
	//public static readonly string DOMAIN_URL = "";
	public static readonly string API_VERSION = "2013-09-01";
	private static bool _callbackFlag = false;
	private static readonly int REQUEST_TIME_OUT = 10;

	public static bool CallbackFlag {
		get {
			return _callbackFlag;
		}
		set {
			_callbackFlag = value;
		}
	}

	// 初期化
	public static void Initialize ()
	{
		GameObject manager = new GameObject ();
		manager.name = "NCMBManager";
		manager.AddComponent<NCMBManager> ();

		GameObject settings = new GameObject ();
		settings.name = "NCMBSettings";
		settings.AddComponent<NCMBSettings> ();

		NCMBSettings.Initialize (
			APP_KEY,
			CLIENT_KEY,
			DOMAIN_URL,
			API_VERSION
		);
		CallbackFlag = false;

		GameObject o = new GameObject ("settings");
		System.Object obj = o.AddComponent<NCMBSettings> ();
		FieldInfo field = obj.GetType ().GetField ("filePath", BindingFlags.Static | BindingFlags.NonPublic);
		field.SetValue (obj, Application.persistentDataPath);

		NCMBUser.LogOutAsync ();

		MockServer.startMock ();
	}

	public static IEnumerator AwaitAsync ()
	{
		Debug.Log ("確認3");
		float elapsedTime = 0.0f;
		while (NCMBTestSettings.CallbackFlag == false) {
			Debug.Log ("確認4:" + elapsedTime);
			elapsedTime += Time.deltaTime;
			if (elapsedTime >= REQUEST_TIME_OUT) { 
				Debug.Log ("確認　Break！！");
				yield break;
			}
			//yield return new WaitForEndOfFrame ();
			yield return new WaitForSeconds (0.5f); 
		}
		Debug.Log ("確認5");
		yield break;
	}
}