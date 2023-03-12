<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="stadium_manager.aspx.cs" Inherits="Database_Project.stadium_manager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
         <center>
           <div >    
                <h1 style="color:steelblue"> Stadium Manager </h1>
        </div>
            </center>
        <hr /> <div>
            <asp:Panel ID="Panel1" runat="server"></asp:Panel>
        </div>
          <div>
             <h2>My pending requests&nbsp;&nbsp;&nbsp; 

            <asp:Button ID="reuest" runat="server" Text="View" Height="26px" Width="119px" OnClick="reuest_Click" />
            </h2>
             <h2>Accept/Reject a request</h2> 
        </div>

        <div > 
            &nbsp;&nbsp; Host Name&nbsp;
            <asp:TextBox ID="host" runat="server"></asp:TextBox>
            &nbsp;&nbsp;&nbsp; Guest Name
            <asp:TextBox ID="guest" runat="server" ></asp:TextBox>
            &nbsp;&nbsp; Starting Time&nbsp;
            <asp:TextBox ID="start"  type="datetime-local" runat="server" ></asp:TextBox>
            &nbsp;&nbsp;
&nbsp;<asp:Button ID="accept" runat="server" Text="accept"  style="height: 29px" OnClick="accept_Click" />
            &nbsp;<asp:Button ID="Button3" runat="server" Text="reject"  style="height: 29px" OnClick="Button3_Click" />
              </div>
        <br />
         <div>
            <asp:Panel ID="Panel2" runat="server"></asp:Panel>
        </div>
    </form>
</body>
</html>
