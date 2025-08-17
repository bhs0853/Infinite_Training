<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Question2.aspx.cs" Inherits="assignment1.Question2" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Question 2</title>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Select a Product:</h2>

        <asp:DropDownList ID="ddlProducts" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProducts_SelectedIndexChanged" Width="260px">
            <asp:ListItem Text="-- Select Product --" Value=""></asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />

        <asp:Image ID="imgProduct" runat="server" Width="258px" Height="300px" />
        <br />
        <br />

        <asp:Button ID="getPriceBtn" runat="server" Text="Get This Product Price" BackColor="#6395EE" BorderColor="#6395EE" BorderStyle="Solid" BorderWidth="2px" ForeColor="White" Width="257px" OnClick="getPriceBtn_Click" />
        <br />
        <br />

        <asp:Label ID="lblPrice" runat="server" Font-Bold="true" ForeColor="Purple" />
    </form>
</body>
</html>
