using Leopotam.Ecs;

public class MonoEntity : MonoProviderBase
{
	private EcsEntity _entity;

	private MonoProviderBase[] _monoProviders;

	public MonoProvider<T> Get<T>() where T : struct
	{
		foreach (MonoProviderBase link in _monoProviders)
			if (link is MonoProvider<T> monoLink)
				return monoLink;

		return null;
	}

	public override void Provide(ref EcsEntity entity)
	{
		_entity = entity;

		_monoProviders = GetComponents<MonoProviderBase>();
		foreach (MonoProviderBase monoProvider in _monoProviders)
		{
			if (monoProvider is MonoEntity)
			{
				continue;
			}
			monoProvider.Provide(ref entity);
		}
	}
}
