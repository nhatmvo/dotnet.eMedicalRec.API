using System.Threading.Tasks;

namespace eMedicalRecords.API.Projections
{
    public interface IStateProjection
    {
        public Task SubscribeTemplateState();

        public void CreateSubscription();
    }
}