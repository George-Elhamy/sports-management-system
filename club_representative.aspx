<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="club_representative.aspx.cs" Inherits="Database_Project.club_representative" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
         <center>
           <div >    
                <h1 style="color:steelblue"> Club Representative </h1>
        </div>
            </center>
        <hr /> <div>
            <asp:Panel ID="Panel1" runat="server"></asp:Panel>
        </div>
       
         <left>
         <div >
             <h2>Send Host Request</h2> 
        </div>

        <div > 
            Stadium 
            name
            <asp:TextBox ID="name1" runat="server"></asp:TextBox>
            &nbsp;&nbsp;&nbsp; Starting time
            <asp:TextBox ID="start" type="datetime-local" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
            <asp:Button ID="B1" runat="server" Text="Send " OnClick="B1_Click"  />
            <br />
        </div>
             <div>
             <h2>Upcoming matches&nbsp;&nbsp;&nbsp; 

            <asp:Button ID="B3" runat="server" Text="Search " Height="26px" Width="119px" OnClick="B3_Click" />
            </h2> 
        </div>

        <div > 
            &nbsp;
             <h2>Available Stadiums</h2> 
        </div>

        <div > 
            Enter a Date
            <asp:TextBox ID="name3" type="datetime-local" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp

            <asp:Button ID="B4" runat="server" Text="View " OnClick="B4_Click"  />
            </div>
            </left>
        <div>
            <asp:Panel ID="Panel2" runat="server"></asp:Panel>
        </div>
    </form>

</body>
</html>
