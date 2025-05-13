using UnityEngine;

namespace AssetContract
{
	public class AttributeTestBehaviour : MonoBehaviour
	{
		[SerializeField, TextureContract(512, 512, maxFileSizeKb: 350, extension:".png")] 
		private Texture2D _sprite;
	}
}