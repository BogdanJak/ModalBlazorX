using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ModalBlazorX.Models;

namespace ModalBlazorX.Components.Pages;

public partial class VideoShow : ComponentBase
{
    [Inject] private IJSRuntime JsRuntime { get; set; }
    private Video SelectedVideo { get; set; }



    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await JsRuntime.InvokeVoidAsync("setModalCloseStopVideo", "ytModal");
        }
    }

    public async Task ShowVideoHandle()
    {
        SelectedVideo = new Video()
        {
            Title = "Test",
            YTId = "vNHMiTt7gns"
        };

        await InvokeAsync(() => StateHasChanged());
    }

    public void OnVideoDismissed()
    {
        SelectedVideo = null;
    }
}
