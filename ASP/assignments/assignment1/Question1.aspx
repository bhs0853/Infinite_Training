<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Question1.aspx.cs" Inherits="assignment1.Question1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Question 1</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h4>Insert Your Details: </h4>
            <asp:Label ID="name" Text="Name: " runat="server"></asp:Label>
            &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp;  &nbsp;
            <asp:TextBox ID="nameField" runat="server"></asp:TextBox>

            &nbsp;
            <asp:RequiredFieldValidator ID="NameValidator" runat="server" ControlToValidate="nameField" ErrorMessage="Name cannot be empty" ForeColor="Red" Display="Dynamic" ValidationGroup="validateForm"></asp:RequiredFieldValidator>

            <br />
            <br />
            <asp:Label ID="familyName" Text="Family Name: " runat="server"></asp:Label>
            &nbsp; &nbsp;
            <asp:TextBox ID="fnameField" runat="server"></asp:TextBox>
            &nbsp;
            <asp:RequiredFieldValidator ID="FnameValidator" runat="server" ControlToValidate="fnameField" Display="Dynamic" ErrorMessage="Family Name cannot be empty" ForeColor="Red" ValidationGroup="validateForm"></asp:RequiredFieldValidator>
            &nbsp;<asp:CompareValidator ID="FnameValidator1" runat="server" ControlToCompare="nameField" ControlToValidate="fnameField" Display="Dynamic" ErrorMessage="Family Name must be different from name" ForeColor="Red" Operator="NotEqual" ValidationGroup="validateForm"></asp:CompareValidator>
            <br />
            <br />
            <asp:Label ID="address" Text="Address: " runat="server"></asp:Label>
            &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="addressField" runat="server"></asp:TextBox>
            &nbsp;
            <asp:RequiredFieldValidator ID="AddressValidator" runat="server" ControlToValidate="addressField" Display="Dynamic" ErrorMessage="Address Cannot be empty" ForeColor="Red" ValidationGroup="validateForm"></asp:RequiredFieldValidator>
            &nbsp;<asp:RegularExpressionValidator ID="AddressValidator1" runat="server" ControlToValidate="addressField" Display="Dynamic" ErrorMessage="at least 2 chars" ForeColor="Red" ValidationExpression="[a-zA-Z]{2,}" ValidationGroup="validateForm"></asp:RegularExpressionValidator>
            <br />
            <br />
            <asp:Label ID="city" Text="City: " runat="server"></asp:Label>
            &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="cityField" runat="server"></asp:TextBox>
            &nbsp;
            <asp:RequiredFieldValidator ID="cityValidator" runat="server" ControlToValidate="cityField" Display="Dynamic" ErrorMessage="City Cannot be empty" ForeColor="Red" ValidationGroup="validateForm"></asp:RequiredFieldValidator>
            &nbsp;<asp:RegularExpressionValidator ID="cityValidator1" runat="server" ControlToValidate="cityField" Display="Dynamic" ErrorMessage="at least 2 chars" ForeColor="Red" ValidationExpression="[a-zA-Z]{2,}" ValidationGroup="validateForm"></asp:RegularExpressionValidator>
            <br />
            <br />
            <asp:Label ID="zipCode" Text="Zip Code: " runat="server"></asp:Label>
            &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
            <asp:TextBox ID="zipCodeField" runat="server"></asp:TextBox>
            &nbsp;
            <asp:RequiredFieldValidator ID="zipCodeValidator" runat="server" ControlToValidate="zipCodeField" Display="Dynamic" ErrorMessage="Zip Code Cannot be empty" ForeColor="Red" ValidationGroup="validateForm"></asp:RequiredFieldValidator>
            &nbsp;<asp:RangeValidator ID="zipCodeValidator1" runat="server" ControlToValidate="zipCodeField" Display="Dynamic" ErrorMessage="(xxxxx)" ForeColor="Red" MaximumValue="99999" MinimumValue="10000" Type="Integer" ValidationGroup="validateForm"></asp:RangeValidator>
            <br />
            <br />
            <asp:Label ID="phone" Text="Phone: " runat="server"></asp:Label>
            &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="phoneField" runat="server"></asp:TextBox>
            &nbsp;
                        <asp:RequiredFieldValidator ID="phoneValidator" runat="server" ControlToValidate="phoneField" Display="Dynamic" ErrorMessage="Phone Cannot be empty" ForeColor="Red" ValidationGroup="validateForm"></asp:RequiredFieldValidator>
            &nbsp;<asp:RegularExpressionValidator ID="phoneValidator1" runat="server" ControlToValidate="phoneField" ErrorMessage="(xx-xxxxxxx / xxx-xxxxxxx)" ForeColor="Red" ValidationExpression="((\d{2}-)|(\d{3}-)){1}\d{7}" ValidationGroup="validateForm"></asp:RegularExpressionValidator>
            <br />
            <br />
            <asp:Label ID="email" Text="E-Mail: " runat="server"></asp:Label>
            &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
            <asp:TextBox ID="emailField" runat="server"></asp:TextBox>

            &nbsp;
            <asp:RequiredFieldValidator ID="emailValidator" runat="server" ControlToValidate="emailField" Display="Dynamic" ErrorMessage="Email cannot be empty" ForeColor="Red" ValidationGroup="validateForm"></asp:RequiredFieldValidator>
            &nbsp;<asp:RegularExpressionValidator ID="emailValidator1" runat="server" ControlToValidate="emailField" ErrorMessage="(example@example.com)" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="validateForm"></asp:RegularExpressionValidator>

            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="checkBtn" runat="server" Text="check" OnClick="checkBtn_Click" ValidationGroup="validateForm" />

            <br />
            <br />

            <asp:Label ID="lblMessage" runat="server"></asp:Label>
            <asp:ValidationSummary ID="ValidationSummary" runat="server" ForeColor="Red" ValidationGroup="validateForm" />
        </div>
    </form>
</body>
</html>
