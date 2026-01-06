/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using Unity.Services.Core;
using Unity.Services.Core.Environments;

namespace DsoftGameKit
{
    public class IapManager : MonoBehaviour, IStoreListener
    {
        public string environment = "production";
        async void Start()
        {
            try
            {
                var options = new InitializationOptions()
                    .SetEnvironmentName(environment);

                await UnityServices.InitializeAsync(options);
                InitializePurchasing();
            }
            catch (Exception exception)
            {
                // An error occurred during services initialization.
            }
        }

        public static IapManager _Instance;
        private void Awake()
        {
            if(_Instance == null)
            {
                _Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }


        IStoreController m_StoreController; // The Unity Purchasing system.

        //Your products IDs. They should match the ids of your products in your store.
        public List<string> _Products = new List<string>();
        void InitializePurchasing()
        {
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            //Add products that will be purchasable and indicate its type.
            for (int i = 0; i < _Products.Count; i++)
            {
                builder.AddProduct(_Products[i], ProductType.Consumable);
            }

            UnityPurchasing.Initialize(this, builder);
        }

        public void Buy(string _product)
        {
            m_StoreController.InitiatePurchase(_product);
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            Debug.Log("In-App Purchasing successfully initialized");
            m_StoreController = controller;
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            Debug.Log($"In-App Purchasing initialize failed: {error}");
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
        {
            //Retrieve the purchased product
            var product = args.purchasedProduct;

            IapAgent[] _list = FindObjectsOfType(typeof(IapAgent)) as IapAgent[];

            for (int i = 0; i < _list.Length; i++)
            {
                //Add the purchased product to the players inventory
                if (product.definition.id == _list[i]._Product)
                {
                    _list[i].OnComplete();
                    Debug.Log($"Purchase Complete - Product: {product.definition.id}");
                }
            }


            //We return Complete, informing IAP that the processing on our side is done and the transaction can be closed.
            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            Debug.Log($"Purchase failed - Product: '{product.definition.id}', PurchaseFailureReason: {failureReason}");
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            throw new System.NotImplementedException();
        }

    }
}
*/