<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Fanpage.aspx.cs" Inherits="Database_Project.Try" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <center>
        
        <div >    
                <h1 style="color:red;">Fanpage </h1>
        </div>
            </center>
        <hr />
        <left>
        <div >
        <h2>Display available matches</h2> 
        </div>

        <div > 
            Enter date
            <asp:TextBox ID="startdate" type="datetime-local" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button1"  runat="server" Text="search" OnClick="Button1_Click" />
            <br />
        </div>
            
        
            <div>
                <h2> Purchase a ticket</h2>
            </div>
        <div > 
            Enter Host Name
            <asp:TextBox ID="host" runat="server" style="margin-left: 43px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
            
            <br />
        </div>
            <div > 
            Enter Guest Name
            <asp:TextBox ID="guest" runat="server" style="margin-left: 35px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
            
            <br />
        </div>
            <div > 
            Enter Starting Time
            <asp:TextBox ID="start" type="datetime-local" runat="server" style="margin-left: 10px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
            
                <asp:Button ID="Button2" runat="server" Text="Purchase" OnClick="Button2_Click" />
            
                
        </div>
            </left>
        
        <div>
            <asp:Panel ID="Panel1" runat="server"></asp:Panel>
        </div>
    </form>
</body>
</html>
