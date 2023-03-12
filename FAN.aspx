<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FAN.aspx.cs" Inherits="Database_Project.FAN" %>

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
         National ID 
         <br />
        <asp:TextBox ID="NatID" runat="server"></asp:TextBox>
                  <br />
         phone number 
         <br />
        <asp:TextBox ID="phone" type="number" runat="server"></asp:TextBox>
                  <br />
         birth date
         <br />
        <asp:TextBox ID="birth" type="date" runat="server"></asp:TextBox>
         <br />
         address
         <br />
        <asp:TextBox ID="address" runat="server"></asp:TextBox>
                <br />
        <asp:Button ID="loginbutton" runat="server" Text="Register" OnClick="loginbutton_Click" />
        <br />
        <div />
                </div>
    </form>
</body>
</html>
