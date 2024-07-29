using Microsoft.AspNetCore.Components;
using SportTracker.Server.Models.SportEvents;

namespace SportTracker.Server.Components.Pages
{
    public partial class Home
    {
        [Inject]
        private ISportEventRepository SportEventRepository { get; set; } = null!;

        private string? errorMessage;
        private bool submitted = false;

        [SupplyParameterFromForm]
        public SportEventInput EventInput { get; set; } =
            new() { EventType = SportEventType.Swimming };

        private void OnSubmitEvent()
        {
            try
            {
                SportEventRepository.AddEvent(EventInput);
                submitted = true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
    }
}
