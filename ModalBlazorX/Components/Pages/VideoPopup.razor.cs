using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ModalBlazorX.Models;

namespace ModalBlazorX.Components.Pages;

public partial class VideoPopup : ComponentBase, IDisposable
{
    [Inject] IJSRuntime JSRuntime { get; set; }
    [Parameter] public Video SelectedVideo { get; set; }
    [Parameter]     public EventCallback OnPopupClosed { get; set; }


    private bool AddedToFavorites;

    private DotNetObjectReference<VideoPopup> dotNetObjectReference;

    protected override async Task OnInitializedAsync()
    {
        dotNetObjectReference = DotNetObjectReference.Create(this);

    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await OpenModal();
        }
    }

    private async Task OpenModal()
    {
        await JSRuntime.InvokeVoidAsync("openModal", "ytModal", dotNetObjectReference);
    }


    [JSInvokable("OnModalCloseClicked")]
    public async Task OnModalCloseClicked()
    {
        await JSRuntime.InvokeVoidAsync("closeModal", "ytModal");
        await OnPopupClosed.InvokeAsync(EventCallback.Empty);
    }

    public void Dispose() => dotNetObjectReference?.Dispose();
}
