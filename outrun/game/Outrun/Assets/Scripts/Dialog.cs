using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Dialog : MonoBehaviour
{
	private UnityAction dialogListener;
	private Text _textComponent;
	public string[] DialogueStrings;
	public float SecondsBetweenCharacters = 0.15f;
	public float CharacterRateMultiplier = 0.5f;
	public KeyCode DialogueInput = KeyCode.Return;
	private bool _isStringBeingRevealed = false;
	private bool _isDialoguePlaying = false;
	private bool _isEndOfDialogue = false;
	public GameObject ContinueIcon;
	public GameObject StopIcon;


	void Start ()
	{
	    _textComponent = GetComponent<Text>();
	    _textComponent.text = "";

        HideIcons();
	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Return))
		{
			if (!_isDialoguePlaying)
			{
				_isDialoguePlaying = true;
				StartCoroutine(StartDialogue());
			}

		}
	}

	private IEnumerator StartDialogue()
	{
		int dialogueLength = DialogueStrings.Length;
		int currentDialogueIndex = 0;

		while (currentDialogueIndex < dialogueLength || !_isStringBeingRevealed)
		{
			if (!_isStringBeingRevealed)
			{
				_isStringBeingRevealed = true;
				StartCoroutine(DisplayString(DialogueStrings[currentDialogueIndex++]));

				if (currentDialogueIndex >= dialogueLength)
				{
					_isEndOfDialogue = true;
				}
			}

			yield return 0;
		}

		while (true)
		{
			if (Input.GetKeyDown(DialogueInput))
			{
				break;
			}

			yield return 0;
		}

		HideIcons();
		_isEndOfDialogue = false;
		_isDialoguePlaying = false;
	}

	private IEnumerator DisplayString(string stringToDisplay)
	{
		int stringLength = stringToDisplay.Length;
		int currentCharacterIndex = 0;

		HideIcons();

		_textComponent.text = "";

		while (currentCharacterIndex < stringLength)
		{
			_textComponent.text += stringToDisplay[currentCharacterIndex];
			currentCharacterIndex++;

			if (currentCharacterIndex < stringLength)
			{
				if (Input.GetKey(DialogueInput))
				{
					yield return new WaitForSeconds(SecondsBetweenCharacters*CharacterRateMultiplier);
				}
				else
				{
					yield return new WaitForSeconds(SecondsBetweenCharacters);
				}
			}
			else
			{
				break;
			}
		}

		ShowIcon();

		while (true)
		{
			if (Input.GetKeyDown(DialogueInput))
			{
				break;
			}

			yield return 0;
		}

		HideIcons();

		_isStringBeingRevealed = false;
		_textComponent.text = "";
	}

	private void HideIcons()
	{
		ContinueIcon.SetActive(false);
		StopIcon.SetActive(false);
	}

	private void ShowIcon()
	{
		if (_isEndOfDialogue)
		{
			Debug.Log("end dialog");
			StopIcon.SetActive(true);
			EventManager.TriggerEvent ("endDialog");
			return;
			
		}
		Debug.Log("continue icon");
		ContinueIcon.SetActive(true);
	}
}