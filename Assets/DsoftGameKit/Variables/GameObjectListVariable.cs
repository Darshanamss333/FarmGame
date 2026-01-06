using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DsoftGameKit
{
	[CreateAssetMenu(fileName = "newGameObjectList", menuName = "DsoftGameKit/GameObjectList")]
	public class GameObjectListVariable : ScriptableObject
	{
		public List<GameObject> Value;
	}
}
