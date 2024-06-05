using AutoFixture;

namespace EShop.UnitTest.Application
{
    public class CustomFixture : Fixture
    {
        public CustomFixture()
        {
            this.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => this.Behaviors.Remove(b));
            this.Behaviors.Add(new OmitOnRecursionBehavior());
        }
    }
}
