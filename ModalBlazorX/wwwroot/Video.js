window.setTitle = function (title) {
    document.title = title;
}

window.dotNetObjectRef = null;
window.ytAPIReady = false;
window.playerReady = false;
window.player = null;

window.initializeYouTubePlayer = function () {
    window.player = null;

    var tag = document.createElement('script');
    tag.src = 'https://www.youtube.com/iframe_api';
    var firstScriptTag = document.getElementsByTagName('script')[0];
    firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);
}

window.createPlayer = function () {
    window.player = new YT.Player("player", {
        videoId: '',
        width: "100%",
        height: "100%",
        playerVars: {
            autoplay: 1,
            iv_load_policy: 3
        },
        events: {
            'onReady': onPlayerReady,
            'onStateChange': onPlayerStateChange
        }
    });
}

window.playVideo = function (videoId) {
    if (window.player) {
        window.player.setVolume(30);
        window.player.loadVideoById(videoId);
    }
}

window.onPlayerStateChange = function (event) {
    if (event.data === YT.PlayerState.ENDED) {
        if (window.dotNetObjectRef) {
            window.dotNetObjectRef.invokeMethodAsync('OnModalCloseClicked');
        }
    }
}

window.onYouTubeIframeAPIReady = function () {
    window.ytAPIReady = true;
}

window.onPlayerReady = function (event) {
    window.playerReady = true;
}

window.checkAPIReady = function () {
    if (window.ytAPIReady) {
        return true;
    }

    return false;
}

window.checkPlayer = function () {
    if (player && playerReady) {
        return true;
    } else {
        return false;
    }
}

window.closeModal = function (id) {
    $("#" + id).modal("hide");
    $("body").css("overflow-y", "auto");
    toggleMainUI();
}

window.openModal = function (id, dotNetObjectRef) {
    window.dotNetObjectRef = dotNetObjectRef;
    $("#" + id).modal({ backdrop: "static", keyboard: false });
    $("body").css("overflow-y", "hidden");
    toggleMainUI();

    if (player) {
        player.playVideo();
    }
}

window.setModalCloseStopVideo = function (id) {
    $("#" + id).on("hidden.bs.modal", function () {
        if (player) {
            player.stopVideo();
        }
    });
}

window.toggleMainUI = function () {
    $("#main-ui").toggle();
}