/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DsoftGameKit
{
    [RequireComponent(typeof(Button))]
    public class BuyIap : MonoBehaviour, IStoreListener
    {
        IStoreController m_StoreController; // The Unity Purchasing system.

        //Your products IDs. They should match the ids of your products in your store.
        public string IapId;
        public UnityEvent _OnCompleteEvent;


        void Start()
        {
            InitializePurchasing();
        }

        private void OnEnable()
        {
            GetComponent<Button>().onClick.AddListener(Buy);
        }

        private void OnDisable()
        {
            GetComponent<Button>().onClick.RemoveListener(Buy);
        }

        void InitializePurchasing()
        {
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            //Add products that will be purchasable and indicate its type.
            builder.AddProduct(IapId, ProductType.Consumable);

            UnityPurchasing.Initialize(this, builder);
        }

        public void Buy()
        {
            m_StoreController.InitiatePurchase(IapId);
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            Debug.Log("In-App Purchasing successfully initialized");
            DebugScreen._instance?.Debug("In-App Purchasing successfully initialized");
            m_StoreController = controller;
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            Debug.Log($"In-App Purchasing initialize failed: {error}");
            DebugScreen._instance?.Debug($"In-App Purchasing initialize failed: {error}");

        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
        {
            //Retrieve the purchased product
            var product = args.purchasedProduct;

            //Add the purchased product to the players inventory
            if (product.definition.id == IapId)
            {
                _OnCompleteEvent?.Invoke();
            }

            Debug.Log($"Purchase Complete - Product: {product.definition.id}");
            DebugScreen._instance?.Debug($"Purchase Complete - Product: {product.definition.id}");


            //We return Complete, informing IAP that the processing on our side is done and the transaction can be closed.
            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            Debug.Log($"Purchase failed - Product: '{product.definition.id}', PurchaseFailureReason: {failureReason}");
            DebugScreen._instance?.Debug($"Purchase failed - Product: '{product.definition.id}', PurchaseFailureReason: {failureReason}");

        }
    
    }
  

}
*/