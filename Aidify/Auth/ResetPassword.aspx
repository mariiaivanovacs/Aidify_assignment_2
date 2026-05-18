<%@ Page Title="Reset Password" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="Aidify.Auth.ResetPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <section class="auth-section">
        <div class="container">
            <div class="row justify-content-center">
                <div class="col-md-5 auth-right">

                    <h2>Reset Your Password</h2>
                    <p>Enter and confirm your new password below.</p>

                    <asp:Label ID="lblMessage" runat="server" CssClass="auth-error" Visible="false"></asp:Label>

                    <div class="mb-3">
                        <label>New Password</label>
                        <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="New password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvNewPassword" runat="server" ControlToValidate="txtNewPassword"
                            ErrorMessage="Password is required" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>

                    <div class="mb-3">
                        <label>Confirm New Password</label>
                        <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Confirm password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword"
                            ErrorMessage="Please confirm password" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="cvPasswords" runat="server" ControlToValidate="txtConfirmPassword"
                            ControlToCompare="txtNewPassword" ErrorMessage="Passwords do not match"
                            CssClass="text-danger" Display="Dynamic"></asp:CompareValidator>
                    </div>

                    <asp:Button ID="btnReset" runat="server" Text="Reset Password" CssClass="btn btn-aidify w-100" OnClick="btnReset_Click" />

                    <p class="auth-bottom-text mt-3">
                        <a href="Login.aspx">Back to Login</a>
                    </p>

                </div>
            </div>
        </div>
    </section>

</asp:Content>
