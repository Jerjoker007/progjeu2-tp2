using UnityEngine;

// Il s'agit de la classe dans laquelle vous allez implémenter la machine à état. Vous aurez tout particulièrement à
// compléter la méthode "ChangeCharacterState" (Vous aurez pour cela besoin de créer une énumération des prochains états)
//
// Notez que c'est aussi ici que les personnages "malpropres" sont gérés. Pour les personnages "malpropre", cette classe
// contient une méthode qui, de temps en temps, met le booléen "shouldThrowTrash" du CharacterBlackboard à "true". Dans
// votre machine à état, cela signifie qu'il est temps de changer d'état pour lancer un déchet.
//
// Au cas où ce n'était pas encore clair, vous avez à modifier cette classe pour le travail.
public class CharacterStateMachine : MonoBehaviour
{
    [Header("Behaviour")]
    [SerializeField] private CityCharacterTrashBehaviour trashBehaviour = CityCharacterTrashBehaviour.Ignore;
    [SerializeField, Range(0, 100)] private float throwTrashChances = 5f;
    [SerializeField, Min(0)] private float throwTrashCheckDelay = 1f;

    private Character character;
    private float throwTrashCheckTimer;

    private BaseState currentState;
    private string currentStateName;
    public string CurrentStateName => currentStateName;
    public CityCharacterTrashBehaviour TrashBehaviour => trashBehaviour;

    private void Awake()
    {
        // Get dependencies.
        character = GetComponent<Character>();

        // Init timers.
        throwTrashCheckTimer = 0;

        ChangeCharacterState(CharacterNextState.Moving);
    }

    private void Update()
    {
        UpdateTimers();
    }

    private void UpdateTimers()
    {
        if (trashBehaviour == CityCharacterTrashBehaviour.Throw)
        {
            throwTrashCheckTimer += Time.deltaTime;
            if (throwTrashCheckTimer >= throwTrashCheckDelay)
            {
                character.Blackboard.ShouldThrowTrash = RandomUtils.Chance(throwTrashChances);
                throwTrashCheckTimer = 0f;
            }
        }
        else
        {
            throwTrashCheckTimer = 0f;
        }
    }

    public void ChangeCharacterState(CharacterNextState nextState)
    {
        if (currentState != null) currentState.enabled = false;
        switch (nextState)
        {
            case CharacterNextState.Moving:
                {
                    currentState = gameObject.GetComponent<MovingState>();
                    break;
                }
            case CharacterNextState.Working:
                {
                    currentState = gameObject.GetComponent<WorkingState>();
                    break;
                }
            case CharacterNextState.Eating:
                {
                    currentState = gameObject.GetComponent<EatingState>();
                    break;
                }
            case CharacterNextState.Socializing:
                {
                    currentState = gameObject.GetComponent<SocializingState>();
                    break;
                }
            case CharacterNextState.Sleeping:
                {
                    currentState = gameObject.GetComponent<SleepingState>();
                    break;
                }
            case CharacterNextState.PickingTrash:
                {
                    currentState = gameObject.GetComponent<PickingTrashState>();
                    break;
                }
            case CharacterNextState.ThrowingTrash:
                {
                    currentState = gameObject.GetComponent<ThrowingTrashState>();
                    break;
                }
            case CharacterNextState.GreetingFriend:
                {
                    currentState = gameObject.GetComponent<GreetingState>();
                    break;
                }
        }
        currentStateName = currentState.GetType().Name;
        currentState.enabled = true;
    }
}

public enum CityCharacterTrashBehaviour
{
    Ignore,
    PickUp,
    Throw
}

public enum CharacterNextState
{
    Moving,
    Working,
    Eating,
    Socializing,
    Sleeping,
    PickingTrash,
    ThrowingTrash,
    GreetingFriend
}