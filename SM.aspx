<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SM.aspx.cs" Inherits="Database_Project.SM" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div style="margin-left: 400px">
            <h2>Please Register!</h2>
           
            Name<br />
            <asp:TextBox ID="name" runat="server"></asp:TextBox>
            <br />
            Username<br />
            <asp:TextBox ID="username" runat="server"></asp:TextBox>
            <br />
            Password
                <br />

        <asp:TextBox ID="password" type="password" runat="server"></asp:TextBox>
        <br />
         stadium Name
         <br />
        <asp:TextBox ID="stadium" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="loginbutton" runat="server" Text="Register" OnClick="loginbutton_Click" />
        <br />
        </div >
                </div>
    </form>
</body>

</html>
