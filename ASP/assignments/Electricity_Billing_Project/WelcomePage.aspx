<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WelcomePage.aspx.cs" Inherits="Electricity_Billing_Project.WelcomePage1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
    <div>
        <h3 style="margin-left: 40px">Welcome Admin !!!</h3>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="AddBillsBtn" runat="server" Text="Add Bills" OnClick="AddBillsBtn_Click" BackColor="CornflowerBlue" ForeColor="White" BorderColor="CornflowerBlue" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="ViewBillsBtn" runat="server" Text="View Bills" OnClick="ViewBillsBtn_Click" BackColor="CornflowerBlue" ForeColor="White" BorderColor="CornflowerBlue" />
    </div>
</asp:Content>
