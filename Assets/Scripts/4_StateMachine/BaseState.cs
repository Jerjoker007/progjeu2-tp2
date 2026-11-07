using UnityEngine;

public class BaseState : MonoBehaviour
{
    protected Character character;
    protected CharacterStateMachine stateManager;
    protected CharacterBlackboard blackboard;
    protected CharacterVitals vitals;


    void Awake()
    {
        character = GetComponent<Character>();
        stateManager = GetComponent<CharacterStateMachine>();
        blackboard = GetComponent<CharacterBlackboard>();
        vitals = GetComponent<CharacterVitals>();
    }

    void Update()
    {
        UpdateCharacter();
        ManageStateChange();
    }

    protected virtual void UpdateCharacter() {}
    protected virtual void ManageStateChange() {}
    
    protected void SetNewDestination()
    {
        if (vitals.IsLonelinessAboveThreshold) blackboard.LastDestination = ArrayExtensions.Random(blackboard.SocialBuildings);
        else if (vitals.IsHungerAboveThreshold) blackboard.LastDestination = ArrayExtensions.Random(blackboard.FoodBuildings);
        else if (vitals.IsSleepinessAboveThreshold) blackboard.LastDestination = blackboard.House;
        else blackboard.LastDestination = blackboard.Workplace;
    }
}
