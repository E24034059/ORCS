﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sendMessageBySignalR.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>sendMessageBySignalRInORCS</title>
</head>
<body>
    <!--Script references. -->
    <!--Reference the jQuery library. -->
    <script src="Scripts/jquery-1.6.4.min.js" "></script>
    <!--Reference the SignalR library. -->
    <script src="Scripts/jquery.signalR-2.2.0.min.js"></script>
    <!--Reference the autogenerated SignalR hub script. -->
    <script src='<%: ResolveClientUrl("~/signalr/hubs") %>'></script>
    <!--Add script to update the page and send messages.-->
    <script type="text/javascript">
        $(function () {
            var userID = $('#hUserID').val();
            var groupID = $('#hTempGroupID').val();
            // 建立與 Server 端的 Hub 的物件，注意 Hub 的開頭字母一定要為小寫
            // Declare a proxy to reference the hub.
            var chat = $.connection.chatHub;
            // Create a function that the hub can call to broadcast messages.
            chat.client.broadcastMessage = function (name, message) {
                var messageArray = message.split("#");
                switch (messageArray[0]) {
                    //通知小組成員開啟考卷
                    case "OpenWindows":
                        //推播給同組成員開啟考卷 不包含自己
                        if (userID != name)
                            window.open(messageArray[1]);
                        break;
                    case "OutOfIframeToRedirect":
                        //推播給同組成員開啟考卷 不包含自己
                        if (userID != name)
                            window.open(messageArray[1]);
                    default:
                        break;
                }
            };
            // 將連線打開
            // Start the connection.
            $.connection.hub.start().done(function () {
                //連線設定GroupID
                chat.server.group(groupID);
                // 呼叫 Server 端的 sendMessage 方法，並傳送使用者姓名及訊息內容給 Server
                $('#btSendmessage').click(function () {
                    // Call the Send method on the hub.
                    chat.server.send(userID, $('#txtMessage').val());
                    // Clear text box and reset focus for next comment.
                    $('#message').val('');
                });
            });
        });
    </script>
    <input type="text" id="txtMessage" runat="server"/>
    <input type="button" id="btSendmessage" value="Send" />
    <input type="hidden" id="hTempGroupID" value="" runat="server" />
    <input type="hidden" id="hUserID" value="" runat="server" />
    <input id="Button1" type="button" value="button" onclick="window.open('https://www.google.com.tw','resizable=yes, width=700px, height=620px')" />
</body>
</html>