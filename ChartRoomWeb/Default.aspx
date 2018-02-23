<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>ChatRoom - Login </title>
    <link rel="stylesheet" href="css/reset.css">
    <link rel="stylesheet" href="css/style.css" media="screen" type="text/css" />
    <script src="js/index.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        
        <div class="wrap">
            <div><h3 style= "text-align: center; color: white ">ChatRoom Login</h3></div>
            <div class="avatar">
                <img  src="/img/avatar.png"/>
            </div>
            <input type="text" placeholder="username" required runat="server"/>
            <div class="bar">
                <i></i>
            </div>
            <input type="password" placeholder="password" required runat="server"/>
            <a href="" class="forgot_link">esqueceu ?</a>
            <button>Sign in</button>
        </div>

        

    </form>
</body>
</html>

