using UnityEngine;

public class Hook : MonoBehaviour
{
   [SerializeField] private int _capacity;
   [SerializeField] private bool _isBuyed;
   [SerializeField] private int _price;
   [SerializeField] private string _name;
   [SerializeField] private Sprite _icon;

   private SpriteRenderer _renderer;

   public string Name => _name;
   public int Price => _price;
   public bool IsBuyed => _isBuyed;
   public Sprite Icon => _icon;

   public void Buy()
   {
      _isBuyed = true;
   }

   public void SetCapacity(Bag bag)
   {
      bag.UpdateFishesBag(_capacity);
   }
}