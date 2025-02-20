using UnityEngine;

[CreateAssetMenu(fileName = "QuestInfoSO", menuName = "ScriptableObjects/QuestInfoSO", order = 1)]
public class QuestInfoSO : ScriptableObject
{
    [field: SerializeField] public string id {  get; private set; }

	public string displayName;
	public string displayRoom;

	public GameObject[] questStepPrefabs;

	public int score;
	public int timeToComplete;


	//Set ID of the quest to the name of the scriptable object
	private void OnValidate()
	{
		#if UNITY_EDITOR
			id = this.name;
			UnityEditor.EditorUtility.SetDirty(this);
		#endif
	}
}
