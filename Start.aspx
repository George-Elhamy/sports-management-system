<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Start.aspx.cs" Inherits="Database_Project.Start" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <br />
        <br />
         <div>
            <h2 style="margin-left: 400px">
                Please enter username and password</h2>
            <p style="margin-left: 400px">
                USERNAME
                <asp:TextBox ID="username" runat="server"></asp:TextBox>
            </p>
            <p style="margin-left: 400px">
                PASSWORD
                <asp:TextBox ID="password" type="password" runat="server"></asp:TextBox>
            </p>
             <p style="margin-left: 400px">
                 CHOOSE ONE
                 <asp:RadioButton ID="RadioButton1" runat="server" text="Sports Association Manager" GroupName="i"/>
                 <asp:RadioButton ID="RadioButton2" runat="server" text="Club Representative" GroupName="i"/>
                 <asp:RadioButton ID="RadioButton3" runat="server" text="Stadium Manager" GroupName="i"/>
                 <asp:RadioButton ID="RadioButton4" runat="server" text="Fan" GroupName="i"/> 
                <asp:Button ID="Button1" runat="server" Text="login" style="margin-left: 48px" Width="113px" OnClick="Button1_Click" />
            </p>
             <p style="margin-left: 400px">
                 &nbsp;</p>
            <h3 style="margin-left: 400px;color:tomato">
                Do not have an account?</h3>
            <p style="margin-left: 400px">
                <asp:Button ID="Button2" runat="server" Text="Register as sports association manager " Width="335px" OnClick="Button2_Click" />
                <br />
                <asp:Button ID="Button3" runat="server" Text="Register as club representative"  Width="335px" OnClick="Button3_Click"  />
                <br />
                <asp:Button ID="Button4" runat="server" Text="Register as stadium manager " Width="335px" OnClick="Button4_Click"/>
                <br />
                <asp:Button ID="Button5" runat="server" Text="Register as fan" Width="335px" OnClick="Button5_Click" />
             </p>
        </div>
        
    </form>
</body>
</html>
