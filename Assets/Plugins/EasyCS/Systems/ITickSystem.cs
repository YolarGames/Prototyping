namespace EasyCS.Systems
{
	public interface ITickSystem : ISystem
	{
		public void Tick(in float deltaTime);
	}
}