using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour {
  
  private Queue<string> sentences = new Queue<string>();
  [SerializeField] private TMP_Text dialogue;
  [SerializeField] private GameObject continueButton;
  [SerializeField] private GameObject marshmallowPile0;
  [SerializeField] private GameObject marshmallowPile1;
  private bool isWriting = false;

  private void Awake() {
    continueButton.SetActive(false);
    marshmallowPile0.SetActive(false);
    marshmallowPile1.SetActive(false);
  }

  public void StartDialogue(Dialogue dialogue) {
    Debug.Log("Starting conversation with " + dialogue.name);

    this.sentences.Clear();

    foreach (string sentence in dialogue.sentences) {
      sentences.Enqueue(sentence);
    }

    DisplayNextSentence();
  }

  public void DisplayNextSentence() {
    if (sentences.Count == 0) {
      EndDialogue();
      return;
    } else if (sentences.Count == 1) {
      marshmallowPile1.SetActive(true);
    } else if (sentences.Count == 2) {
      marshmallowPile0.SetActive(true);
    }

    if (!isWriting) {
      continueButton.SetActive(false);
      string sentence = sentences.Dequeue();
      StopAllCoroutines();
      StartCoroutine(TypeSentence(sentence));
      isWriting = true;
    }

  }

  IEnumerator TypeSentence(string sentence) {
    dialogue.text = "";
    foreach (char letter in sentence.ToCharArray()) {
      dialogue.text += letter;
      yield return new WaitForSeconds(.1f);
    }
    Debug.Log("end typing sentence");
    isWriting = false;
    continueButton.SetActive(true);
  }

  private void EndDialogue() {
    SceneManager.LoadScene("Main", LoadSceneMode.Single);
  }

}
