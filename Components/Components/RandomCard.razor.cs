using Microsoft.AspNetCore.Components;
using TrustyPortfolio.Repositories;

namespace TrustyPortfolio.Components.Components {
    public partial class RandomCard<T> {
        [Inject] IRandomRepository<T> RandomRepository { get; set; }
        T RandomItem { get; set; }

        protected async Task OnInitializedAsync() {
            await InitializeRandomItemAsync();
        }

        async Task InitializeRandomItemAsync() {
            RandomItem = await RandomRepository.GetRandomAsync();
        }
    }
}
