<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Admin_Page_Web.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
   
    <style>
        .chart-container {
            width: 400px;
            height: 400px;
            margin: 20px;
            float: left;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Maincontent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-6">
                <div class="chart-container">
                    <h5 class="text-center">Users Chart</h5>
                    <canvas id="usersChart" width="400" height="400"></canvas>
                </div>
            </div>
            <div class="col-md-6">
                <div class="chart-container">
                    <h5 class="text-center">Comments Chart</h5>
                    <canvas id="commentsChart" width="400" height="400"></canvas>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <div class="chart-container">
                    <h5 class="text-center">Posts Chart</h5>
                    <canvas id="postsChart" width="400" height="400"></canvas>
                </div>
            </div>
            <div class="col-md-6">
                <div class="chart-container">
                    <h5 class="text-center">Bans Chart</h5>
                    <canvas id="bansChart" width="400" height="400"></canvas>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
