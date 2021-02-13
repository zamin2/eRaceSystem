<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="eRaceProject._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

<div class="jumbotron">
        <h1>eRace Project By Team B</h1>
    <br />
        <div>
            <img style="width:100%" id="racing-track" src="images/eRace-track.jpg" />
            <br />
            <label id="race-caption" for="racing-track"><b>Image Credits:</b> Go-Kart race track Image by Alex Pearson from Pixabay, Clip-Art Image by OpenClipart-Vectors from Pixabay</label>

        </div>
    <br />
        <div>
            <img id="teamb-logo" src="images/Team_B.jpg" /> 
            <br />
            <label id="logo-caption" for="teamb-logo">Team B eRace Project Logo</label>
        </div>
      

    </div>

    <div class="row">
     
        <div class="col-md-4">
            <h2>Team Member</h2>
            <ul>
                <li>Baihao Guan</li>
                 <li>Robert Kelly</li>
                 <li>Zahid Bin Amin</li>
                 <li>Tylan Broomfield</li>

            </ul>
        </div>
        <div class="col-md-4">
            <h2>Responsibility</h2>
            <ul>
                <li>Baihao Guan: Sales</li>
                 <li>Robert Kelly - Receiving</li>
                 <li>Zahid Bin Amin - Purchasing</li>
                 <li>Tylan Broomfield - Racing</li>
            </ul>
        </div>

        <div class="col-md-4">
            <h2>Known bugs</h2>
            <p>None</p>
        </div>


    </div>


</asp:Content>
