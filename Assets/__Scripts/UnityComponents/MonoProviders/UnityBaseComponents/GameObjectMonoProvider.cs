using Leopotam.Ecs;

public class GameObjectMonoProvider : MonoProvider<GameObjectProvider>
{
	public override void Provide(ref EcsEntity entity)
	{
		entity.Get<GameObjectProvider>() = new GameObjectProvider
		{
			Value = gameObject
		};
	}
}