using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Entity_turorial : ScriptableObject
{	
	public List<Param> param = new List<Param> ();

	[System.SerializableAttribute]
	public class Param
	{
		
		public int tutorialId;
		public string serihu;
	}
}