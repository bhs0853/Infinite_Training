<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewBillsPage.aspx.cs" Inherits="Electricity_Billing_Project.ViewBillsPage1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
    <br />
    &nbsp;&nbsp;&nbsp;
     Enter No of Bills:&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="nValue" runat="server" Text="0"></asp:TextBox>
    &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="GetDataBtn" runat="server" BackColor="CornflowerBlue" BorderColor="CornflowerBlue" ForeColor="White" OnClick="GetDataBtn_Click" Text="Get Data" />
    <br />
    <br />
    &nbsp;&nbsp;&nbsp;
        <asp:Label ID="error" runat="server" Visible="false" ForeColor="Red"></asp:Label>
    <br />
    <div style="text-align: center;">
        <br />
        <asp:Label ID="txt" runat="server" Visible="False" Font-Size="Large" Font-Bold="True" ForeColor="CornflowerBlue">Details of last 'N' bills:</asp:Label>
        <br />
        <br />
        <asp:Label ID="nbills" runat="server" Visible="false" ForeColor="Black"></asp:Label>
    </div>
</asp:Content>
