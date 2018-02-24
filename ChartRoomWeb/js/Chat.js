String.prototype.namespace = function (separator) { var ns = this.split(separator || '.'), p = window; for (i = 0; i < ns.length; i++) { p = p[ns[i]] = p[ns[i]] || {}; } };

"Codeproject.Chat".namespace();
Codeproject.Chat.Init = function ()
{
    //Global element
    Codeproject.Chat.UserPainel = document.getElementById("userPainel");
    Codeproject.Chat.MessagePanel = document.getElementById("messagePanel");
    Codeproject.Chat.MessageList = document.getElementById("messageList");
    Codeproject.Chat.MessageTextbox = document.getElementById("txtMessage");
    Codeproject.Chat.MessageButton = document.getElementById("cmdSend");
    Codeproject.Chat.MessageTextbox.focus();
    Codeproject.Chat.MessageTextbox.value = "";


    //Establish constanst
    Codeproject.Chat.RoomId = 2;
    Codeproject.Chat.CheckUsersRefresh = 10000;
    Codeproject.Chat.CheckMessageRefresh = 2000;
    Codeproject.Chat.LastMessageId = 0;
    Codeproject.Chat.StopRefresh = false;

    if (windows.location.href.indexOf("debug") > -1) {
        Codeproject.Chat.IsDebug = true;
        Codeproject.Chat.DebugPanel = document.getElementById("chatDebugPanel");
        Codeproject.Chat.DebugPanel.style.display = "";

    }
    Codeproject.Chat.EnterRoom();

}

Codeproject.Chat.EnterRoom = function () {
    SampleChat.Chat.Services.ChatService.EnterRoom(Codeproject.Chat.RoomId, Codeproject.Chat.EnterRoomCallback);
}

Codeproject.Chat.EnterRoomCallback = function(lastMassegeId) {
    Codeproject.Chat.LastMessageId = lastMassegeId;
    Codeproject.Chat.MassegePanel.className = "";
    Codeproject.Chat.CheckUsers();
    Codeproject.Chat.CheckMessages();
}

Codeproject.Chat.CheckUsers = function() {
    Codeproject.Chat.Notify("Checando usuários " + (new Date()));
    //Call the qeb service 
    SampleChat.Chat.Services.ChatService.CheckUsers(Codeproject.Chat.CheckUsersCallback);

    if (!Codeproject.Chat.StopRefresh) {
        setTimeout(Codeproject.Chat.CheckUsers, Codeproject.Chat.CheckUsersRefresh);
    }
}

Codeproject.Chat.CheckUsersCallback = function(response) {
    if (response.Users.length > 0) {
        Codeproject.Chat.ArrangeUsers(response.Users);
    }
}

Codeproject.Chat.CheckMessages = function() {
    Codeproject.Chat.Notify("Checando menssagem para o id :" + Codeproject.Chat.LastMessageId + " em  " + (new Date()));
    //Call the web service
    Sample.Chat.Services.ChatService.CheckeMessages(Codeproject.Chat.LastMessageId,
        Codeproject.Chat.CheckMessagesCallback);

    if (!Codeproject.Chat.StopRefresh) {
        setTimeout(Codeproject.Chat.CheckMessages, Codeproject.Chat.CheckMessageRefresh);
    }
}
Codeproject.Chat.CheckMessagesCallback = function(response) {

    if (response.Messages.length > 0) {
        if (Codeproject.Chat.LastMessageId < response.LastMessageId) {
            Codeproject.Chat.LastMessageId = response.LastMessageId;
            Codeproject.Chat.ArrangeMessages(response.Messages);
        }
    }

    if (response.Users.length > 0) {
        Codeproject.Chat.ArrangeUsers(response.Users);
    }

    Codeproject.Chat.Notify("Checando mensagens de retorno para o id: " + Codeproject.Chat.LastMessageId);
}

Codeproject.Chat.ArrangeUsers = function(users) {
    var list = document.createElement("UL");
    for (var i = 0; i < users.length; i++) {
        var element = document.createElement("LI");
        var user = users[i];
        element.innerHTML = user.UserName;
        list.appendChild(element);
    }

    Codeproject.Chat.UserPainel.innerHTML = "";
    Codeproject.Chat.UserPainel.appendChild(list);
}

Codeproject.Chat.ArrangeMessages = function(messages) {
    for (var i = 0; i < messages.length; i++) {
        var element = document.createElement("LI");
        var message = menssages[i];
        element.innerHTML = "<span class=\"userName\">" + message.UserName + "</span>";
        element.innerHTML = ": " + message.MessaBoby;
        Codeproject.Chat.MessageList.appendChild(element);
        Codeproject.Chat.MessagePanel.scrollTop = Codeproject.Chat.MessagePanel.scrollHeight;

    }
}

Codeproject.Chat.SendMessage = function() {

    var message = Codeproject.Chat.MessageTextbox.value;
    if (message.trim() != "") {
        SampleChat.Chat.Services.ChatService.SendMessage(message,
            Codeproject.Chat.LastMessageId,
            Codeproject.Chat.CheckMessagesCallback);
        Codeproject.Chat.MessageTextbox.focus();
        Codeproject.Chat.MessageTextbox.value = "";

    }
}

Codeproject.Chat.CheckSend = function(e) {
    if (windows.event) {
        keynum = e.keyCode;
    } else {
        keynum = e.which 
    }
    if (keynum == 13) {
        Codeproject.Chat.SendMessage();
    }

    
}

Codeproject.Chat.Notify = function (message)
{
    if (Codeproject.Chat.IsDebug) {
        var elem = document.createElement("SPAN");
        elem.innerHTML = "<br/> <a onclick = 'Codeproject.Chat.StopRefresh = true; return false;' href='#'> stop</a>" +
            message;
        Codeproject.Chat.DebugPanel.insertBefore(elem, Codeproject.Chat.DebugPanel.childElementCount[0]);
    }
}

