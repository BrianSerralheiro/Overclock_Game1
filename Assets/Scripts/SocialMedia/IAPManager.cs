using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

namespace IAP
{
	public class IAPManager :MonoBehaviour, IStoreListener
	{
		private delegate void Dele(int i);
		private static Dele _premium;
		private static IStoreController storeController;
		private static IExtensionProvider storeExtensionProvider;
		
		private static string[] products={"stars100","stars300","stars1000","premium"};
		private static int[] values={100,300,1000};

		void Start()
		{
			if(!IsInitialized())
			{
				InitializePurchasing();
			}
		}
		public static void Denitialize()
		{
			storeController=null;
			storeExtensionProvider=null;
		}
		public static void Premium()
		{
			_premium(products.Length-1);
		}
		public void InitializePurchasing()
		{
			_premium=BuyProduct;
			if(IsInitialized())
			{
				return;
			}

			var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
			for(int i=0;i<4;i++){
				builder.AddProduct(products[i],i==3? ProductType.NonConsumable : ProductType.Consumable);
			}
			UnityPurchasing.Initialize(this,builder);
		}


		private bool IsInitialized()
		{
			return storeController != null && storeExtensionProvider != null;
		}
		
		public void BuyProduct(int id)
		{
			if(IsInitialized() && id>=0 && id<4)
			{
				
				Product product = storeController.products.WithID(products[id]);

				if(product != null && product.availableToPurchase)
				{
					Debug.Log(string.Format("Purchasing product asychronously: '{0}'",product.definition.id));
					storeController.InitiatePurchase(product);
				}
				else
				{
					Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
				}
			}
			else
			{
				Debug.Log("BuyProductID FAIL. Not initialized.");
			}
		}

		//  
		// --- IStoreListener
		//

		public void OnInitialized(IStoreController controller,IExtensionProvider extensions)
		{
			Debug.Log("OnInitialized: PASS");
			
			storeController = controller;
			storeExtensionProvider = extensions;
		}


		public void OnInitializeFailed(InitializationFailureReason error)
		{
			//TODO avisar player com um popup
			Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
		}


		public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
		{
			for(int i=0;i<4;i++){
				if(String.Equals(args.purchasedProduct.definition.id,products[i],StringComparison.Ordinal))
				{
					Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'",args.purchasedProduct.definition.id));
					if(i==3)
						Locks.UnlockAll();
					else 
						Cash.totalCash += values[i];
					return PurchaseProcessingResult.Complete;
				}
			}
			//esse erro só deve ocorrer se algum ID estiver errado
			Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'",args.purchasedProduct.definition.id));
			return PurchaseProcessingResult.Complete;
		}


		public void OnPurchaseFailed(Product product,PurchaseFailureReason failureReason)
		{
			//TODO avisar ao playr sobre a falhacom um popup
			Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}",product.definition.storeSpecificId,failureReason));
		}
	}
}