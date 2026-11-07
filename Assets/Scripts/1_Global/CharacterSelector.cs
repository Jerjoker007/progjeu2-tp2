using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour
{
    private static readonly Color RED_COLOR = new Color32(255, 0, 0, 255);
    private static readonly Color YELLOW_COLOR = new Color32(255, 185, 0, 255);
    private static readonly Color GREEN_COLOR = new Color32(146, 206, 80, 255);


    [Header("UI Character Information")]
    [SerializeField] private Image characterImage;
    [SerializeField] private TMP_Text characterNameText;

    [Header("UI Needs Scrollbar")]
    [SerializeField] private Scrollbar hungerBar;
    [SerializeField] private Scrollbar sleepBar;
    [SerializeField] private Scrollbar lonelinessBar;

    private InputAction nextCharacterAction;
    private InputAction previousCharacterAction;

    private Character[] characters;
    private Character selectedCharacter;
    private int selectedCharacterIndex = 0;


    void Start()
    {
        nextCharacterAction = InputSystem.actions.FindAction("NextCharacter");
        previousCharacterAction = InputSystem.actions.FindAction("PreviousCharacter");
        characters = Finder.GameManager.CityObjects.Characters;
        SelectCharacter(selectedCharacterIndex);
    }

    void Update()
    {
        ManageInput();
        ManageUpdate();
    }

    private void ManageInput()
    {
        if (nextCharacterAction.WasPressedThisFrame())
        {
            NextCharacter();
        }
        else if (previousCharacterAction.WasPressedThisFrame())
        {
            PreviousCharacter();
        }
    }

    private void ManageUpdate()
    {
        characterNameText.text = $"{selectedCharacter.FullName}\n({selectedCharacter.StateMachine.CurrentStateName})";

        hungerBar.size = selectedCharacter.Vitals.Hunger / CharacterVitals.MAX_VITAL_VALUE;
        sleepBar.size = selectedCharacter.Vitals.Sleepiness / CharacterVitals.MAX_VITAL_VALUE;
        lonelinessBar.size = selectedCharacter.Vitals.Loneliness / CharacterVitals.MAX_VITAL_VALUE;

        hungerBar.targetGraphic.color = GetVitalColor(selectedCharacter.Vitals.Hunger, selectedCharacter.Vitals.IsHungerAboveThreshold);
        sleepBar.targetGraphic.color = GetVitalColor(selectedCharacter.Vitals.Sleepiness, selectedCharacter.Vitals.IsSleepinessAboveThreshold);
        lonelinessBar.targetGraphic.color = GetVitalColor(selectedCharacter.Vitals.Loneliness, selectedCharacter.Vitals.IsLonelinessAboveThreshold);
    }

    private void NextCharacter()
    {
        int nextIndex = (selectedCharacterIndex + 1) % characters.Length;
        SelectCharacter(nextIndex);
    }

    private void PreviousCharacter()
    {
        int previousIndex = (selectedCharacterIndex - 1 + characters.Length) % characters.Length;
        SelectCharacter(previousIndex);
    }

    private void SelectCharacter(int index)
    {
        if (selectedCharacter != null) selectedCharacter.HidePointer();

        selectedCharacterIndex = index;
        selectedCharacter = characters[selectedCharacterIndex];

        selectedCharacter.ShowPointer();

        characterImage.sprite = selectedCharacter.Avatar;
        characterNameText.text = $"{selectedCharacter.FullName}\n({selectedCharacter.StateMachine.CurrentStateName})";
    }

    private Color GetVitalColor(float value, bool isAboveThreshold)
    {
        if (value >= CharacterVitals.MAX_VITAL_VALUE * 0.9f) return RED_COLOR;
        else if (isAboveThreshold) return YELLOW_COLOR;

        return GREEN_COLOR;
    }
}
