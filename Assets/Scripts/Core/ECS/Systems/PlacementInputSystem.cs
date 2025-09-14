using Core.ECS.Components;
using Scellecs.Morpeh;

namespace Core.ECS.Systems
{
    public class PlacementInputSystem : ISystem
    {
        public World World { get; set; }

        private Filter filter;
        private Stash<CellComponent> healthStash;

        public void OnAwake()
        {
            this.filter = this.World.Filter.With<CellComponent>().Build();
            this.healthStash = this.World.GetStash<CellComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in this.filter)
            {
                ref var healthComponent = ref healthStash.Get(entity);
                //healthComponent.GridPosition += 1;
            }
        }

        public void Dispose()
        {
        }
    }

    public class EcsRunner
    {
        public EcsRunner()
        {
            var newWorld = World.Create();
            var placementInputSystem = new PlacementInputSystem();

            var systemsGroup = newWorld.CreateSystemsGroup();
            systemsGroup.AddSystem(placementInputSystem);
            
            newWorld.AddSystemsGroup(0, systemsGroup);
        }
    }
}