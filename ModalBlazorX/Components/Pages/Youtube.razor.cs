﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace ModalBlazorX.Components.Pages;

public partial class YouTube : ComponentBase
{
    [Parameter]
    public string VideoId { get; set; }

    [Inject]
    private IJSRuntime JsRuntime { get; set; }

    private Timer _timer;
    private bool playerReady;
    private bool apiReady;
    private bool playerCreated;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {

            await JsRuntime.InvokeVoidAsync("initializeYouTubePlayer");

            // Periodically check to see if the api is ready and the player is created
            // When everything is ready dispose of the timer
            _timer = new Timer(async (e) =>
            {
                playerReady = await JsRuntime.InvokeAsync<bool>("checkPlayer");
                apiReady = await JsRuntime.InvokeAsync<bool>("checkAPIReady");

                if (apiReady && !playerCreated)
                {
                    await JsRuntime.InvokeVoidAsync("createPlayer");
                    playerCreated = true;
                }

                if (playerReady)
                {
                    try
                    {
                        await JsRuntime.InvokeAsync<string>("playVideo", VideoId);
                        _timer.Dispose();
                        _timer = null;
                    }
                    catch
                    {
                        _timer.Dispose();
                        _timer = null;
                    }
                }
            }, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(500));
        }
    }
}
