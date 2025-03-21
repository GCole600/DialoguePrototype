using System;
using TMPro;
using UnityEngine;
using Yarn.Unity;

namespace Dialogue.Scripts
{
    public class DialogueBubble : DialogueViewBase
    {
        [SerializeField] private RectTransform playerContainer;
        [SerializeField] private RectTransform soContainer;
        [SerializeField] private TMP_Text playerText;
        [SerializeField] private TMP_Text soText;
        
        private Action advanceHandler;

        public override void RunLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
        {
            if (dialogueLine.CharacterName == "Player")
            {
                playerContainer.gameObject.SetActive(true);
                soContainer.gameObject.SetActive(false);
                playerText.text = dialogueLine.TextWithoutCharacterName.Text;
            }
            else if (dialogueLine.CharacterName == "SO")
            {
                soContainer.gameObject.SetActive(true);
                playerContainer.gameObject.SetActive(false);
                soText.text = dialogueLine.TextWithoutCharacterName.Text;
            }
            
            advanceHandler = requestInterrupt;
        }

        public override void DismissLine(Action onDismissalComplete)
        {
            playerContainer.gameObject.SetActive(false);
            soContainer.gameObject.SetActive(false);
            onDismissalComplete();
        }

        public override void UserRequestedViewAdvancement()
        {
            if (playerContainer.gameObject.activeSelf || soContainer.gameObject.activeSelf)
                advanceHandler?.Invoke();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                UserRequestedViewAdvancement();
        }
    }
}