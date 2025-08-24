<%@ Page Title="Add Bills" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddBillsPage.aspx.cs" Inherits="Electricity_Billing_Project.WebForm1" %>

<asp:Content ID="addBillsPage" ContentPlaceHolderID="PrimaryContent" runat="server">
    <div style="text-align: center;">
        <h3>Add Electricity Bills</h3>
        <label for="txtBillCount">Enter Number of Bills to be added: </label>
        <asp:TextBox ID="txtBillCount" runat="server" TextMode="Number"></asp:TextBox>&nbsp;
            <br />
        <br />
        <asp:Button ID="addBtn" runat="server" TextMode="Add" Text="Add" BackColor="CornflowerBlue" ForeColor="White" ValidationGroup="Add" OnClick="addBtn_Click" BorderColor="CornflowerBlue"></asp:Button>
        <br />
        <br />
        <asp:PlaceHolder ID="bills" runat="server"></asp:PlaceHolder>
        <br />
        <br />
        <asp:Button ID="saveBtn" runat="server" TextMode="Save" Text="Save" Visible="false" BackColor="CornflowerBlue" ForeColor="White" OnClick="saveBtn_Click" BorderColor="CornflowerBlue"></asp:Button><br />
        <asp:Label ID="status" runat="server" ForeColor="Green"></asp:Label><br />
        <asp:Label ID="error" runat="server" ForeColor="Red"></asp:Label>
        <br />
        <br />
    </div>
</asp:Content>
