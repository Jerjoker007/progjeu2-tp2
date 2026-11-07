using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Mémoire du personnage.
//
// Vous aurez à compléter cette classe en lui ajoutant toute valeur que vous considérez importante à conserver sur le
// long terme. Si la valeur est modifiable, créez une propriété avec un "get" et un "set" directement, sans validation.
//
// Le blackboard contient aussi des informations comme la liste de toutes les épiceries et de tous les cafés. Il s'agit
// des mêmes informations contenues dans le GameManager.
//
// Votre blackboard devrait faire la maintenance des informations qu'il contient. Par exemple, si un déchet ayant été
// aperçu récemment est ramassé par un autre personnage, le blackboard devrait le retirer de sa mémoire.
//
// Vous avez à modifier cette classe pour le travail.
public class CharacterBlackboard : MonoBehaviour
{
    [Header("Knowledge")]
    [field:SerializeField] public Building House { get; private set; }
    [field:SerializeField] public Building Workplace { get; private set; }
    [field:SerializeField] public Character[] Friends { get; private set; } = { };
    public Building[] HouseBuildings => gameManager.CityObjects.HouseBuildings;
    public Building[] WorkplaceBuildings => gameManager.CityObjects.WorkplaceBuildings;
    public Building[] FoodBuildings => gameManager.CityObjects.FoodBuildings;
    public Building[] SocialBuildings => gameManager.CityObjects.SocialBuildings;
    
    [Header("Memory")]
    [field:SerializeField] public bool ShouldThrowTrash { get; set; }
    [field:SerializeField] public Character LastFriendSeen { get; set; }
    [field:SerializeField] public List<Trash> LastTrashSeen { get; set; }
    [field:SerializeField] public Building LastDestination { get; set; }

    
    // TODO : Ajouter ici les autres éléments à conserver en mémoire.
    //        Notez que la syntaxe est légèrement différente de ce que vous avez été habitué : il y a un "field:" devant
    //        le nom de l'annotation [SerializeField]. C'est pour gérer correctement les propriétés C# avec Unity.
    
    private GameManager gameManager;
    
    // Initialization.
    private void Awake()
    {
        gameManager = Finder.GameManager;
        LastTrashSeen = new List<Trash>();
    }
    
    // Blackboard maintenance.
    private void Update()
    {
        
    }

    public bool IsFriend(Character character)
    {
        return Friends.Contains(character);
    }
}