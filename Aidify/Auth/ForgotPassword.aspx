<%@ Page Title="Forgot Password" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="Aidify.Auth.ForgotPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <section class="auth-section">
        <div class="container">
            <div class="row justify-content-center">
                <div class="col-md-5 auth-right">

                    <h2>Forgot Password?</h2>
                    <p>Enter your email address and we'll send you a reset link.</p>

                    <asp:Label ID="lblMessage" runat="server" CssClass="auth-error" Visible="false"></asp:Label>

                    <div class="mb-3">
                        <label>Email Address</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" placeholder="example@email.com"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                            ErrorMessage="Email is required" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>

                    <asp:Button ID="btnSubmit" runat="server" Text="Send Reset Link" CssClass="btn btn-aidify w-100" OnClick="btnSubmit_Click" />

                    <p class="auth-bottom-text mt-3">
                        <a href="Login.aspx">Back to Login</a>
                    </p>

                </div>
            </div>
        </div>
    </section>

</asp:Content>
