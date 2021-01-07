using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public string title = "villager", description;

    private SpriteRenderer unitImage;
    private Dictionary<string, Sprite> sprites;             // Holds sprites for card front, card back, gem front, gem back, icon and unit image
    private string cardFrame, gemFront, gemBack, icon;      // Holds the address of resources

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Load sprites into a dictionary
        sprites = new Dictionary<string, Sprite>();
        sprites.Add("CardFront", Resources.Load<Sprite>("CardResources/cardsFront" + cardFrame));
        sprites.Add("CardBack", Resources.Load<Sprite>("CardResources/cardsBack" + cardFrame));
        sprites.Add("GemFront", Resources.Load<Sprite>("CardResources/gems/ovalShaped" + gemFront));
        sprites.Add("GemBack", Resources.Load<Sprite>("CardResources/gems/rarity" + gemBack));
        sprites.Add("Icon", Resources.Load<Sprite>("CardResources/icons" + icon));
        sprites.Add("UnitImage", Resources.Load<Sprite>("Isle of Lore 2 Strategy Figures/Assets/Figures/180x180/Framed Standard/_" + title));
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites["CardFront"];    // Set the card frame
        unitImage.sprite = sprites["UnitImage"];                                    // Set the unit image

    }

    // Update is called once per frame
    void Update()
    {

    }
}
