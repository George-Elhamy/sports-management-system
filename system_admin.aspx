<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="system_admin.aspx.cs" Inherits="Database_Project.system_admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <center>
        
        <div >    
                <h1 style="color:steelblue;">System admin </h1>
        </div>
            </center>
        <hr />
        <left>
        <div >
        <h2>Add Club</h2> 
        </div>

        <div > 
            name
            <asp:TextBox ID="name1"  runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp 
             location
            <asp:TextBox ID="location1" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button1" runat="server" Text="add " OnClick="Button1_Click" />
            <br />
        </div>
             <h2>Delete Club</h2> 
       

        <div > 
            name
            <asp:TextBox ID="name2" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp 

            <asp:Button ID="Button3" runat="server" Text="delete " OnClick="Button3_Click" />
            <br />
        </div>
             <h2>Add Stadium</h2> 
        </div>

        <div > 
            name
            <asp:TextBox ID="name3" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp
            location
            <asp:TextBox ID="location2" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp
            capacity
            <asp:TextBox ID="capacity" type="number" min="0" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp

            <asp:Button ID="Button4" runat="server" Text="add " OnClick="Button4_Click" />
            <br />
            </div>
             <h2>Delete stadium</h2> 
        </div>

        <div > 
            name
            <asp:TextBox ID="name4" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp 

            <asp:Button ID="Button5" runat="server" Text="delete " OnClick="Button5_Click" />
            <br />
        </div>
            <div>
                <h2> Block A Fan</h2>
            </div>
        <div > 
            Enter national ID
            <asp:TextBox ID="national" runat="server" style="margin-left: 43px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
            
                <asp:Button ID="Button2" runat="server" Text="Block" OnClick="Button2_Click"  />
            
            <br />
        </div>
         
            <br />
        </div>
            </left>
        
        <div>
            <asp:Panel ID="Panel1" runat="server"></asp:Panel>
        </div>
    </form>
</body>
</html>
