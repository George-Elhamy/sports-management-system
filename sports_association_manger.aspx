<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sports_association_manger.aspx.cs" Inherits="Database_Project.sports_association_manger" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
        <center>
        
        <div >    
                <h1 style="color:steelblue">Sports Association Manager</h1>
        </div>
            </center>
        <hr />
    <form id="form1" runat="server">
        <left>
        <div >
        <h2>Add match</h2> 
        </div>

        <div > 
            Host Name
            <asp:TextBox ID="host" runat="server"></asp:TextBox>
            &nbsp;&nbsp;&nbsp; Guest Name
            <asp:TextBox ID="guest" runat="server" ></asp:TextBox>
            &nbsp;&nbsp;StartTime
            <asp:TextBox ID="start" type="datetime-local" runat="server" ></asp:TextBox>
&nbsp;&nbsp; EndTime
            <asp:TextBox ID="end" type="datetime-local" runat="server" ></asp:TextBox>
&nbsp;<asp:Button ID="Button1" runat="server"   Text="add " OnClick="Button1_Click" />
            <br />
        </div>
             <h2>Delete match</h2> 
       

        <div > 
            Host Name
            <asp:TextBox ID="host0" runat="server"></asp:TextBox>
            &nbsp;&nbsp;&nbsp; Guest Name
            <asp:TextBox ID="guest0" runat="server" ></asp:TextBox>
            &nbsp;&nbsp;StartTime
            <asp:TextBox ID="start0" type="datetime-local" runat="server" ></asp:TextBox>
&nbsp;&nbsp; EndTime
            <asp:TextBox ID="end0" type="datetime-local" runat="server" ></asp:TextBox>
&nbsp;<asp:Button ID="Button2"  runat="server" Text="delete " OnClick="Button2_Click" style="height: 29px" />
            <br />
            <br />
      <center >
            
            <asp:Button  ID="Button3" runat="server" Text="upcoming matches " OnClick="Button3_Click" style="height: 29px" />
            &nbsp;<asp:Button ID="Button4" runat="server" Text="matches already played " OnClick="Button4_Click" style="height: 29px" />
        
          &nbsp;&nbsp;<asp:Button ID="Button5" runat="server" Text="clubs never played " OnClick="Button5_Click" style="height: 29px" />
          <center/> 

     
&nbsp;</div>
            </left>
    <div>
            <asp:Panel ID="Panel1" runat="server"></asp:Panel>
        </div>
    </form>
</body>
</html>
