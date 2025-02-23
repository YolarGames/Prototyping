namespace EasyCS.Systems
{
	public interface ITickSystem : ISystem
	{
		public void Tick(float deltaTime);
	}
}