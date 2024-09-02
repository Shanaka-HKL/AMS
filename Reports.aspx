<%@ Page Title="Reports & Analytics" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="AMS._Reports" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="position: relative; width: 100%; height: auto; background-image: url('Images/report.jpg'); background-repeat: no-repeat; background-size: cover; background-position: center; overflow: hidden;"
        class="blurred-background">
        <link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css">
        <br />
        <asp:HiddenField ID="Idn" runat="server" Value="InitialValue" />
        <asp:UpdateProgress ID="UpdateProgress9" runat="server" AssociatedUpdatePanelID="UpdatePanel9">
            <ProgressTemplate>
                <div style="position: fixed; left: 0%; top: 0%; z-index: 999; height: 100%; width: 100%; border-style: none; background-color: Black; filter: alpha(opacity=60); opacity: 0.3; -moz-opacity: 0.5;">
                    <asp:Image ID="ImageLodinggif" Style="position: fixed; left: 48%; top: 48%; z-index: 1000;" runat="server" ImageUrl="~/Images/loading.gif" Width="86px" Height="86px"></asp:Image>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
            <ContentTemplate>
                <div class="panel-header">Reports & Analytics</div>
                <div class="dashboard-section">
                    <div class="dashboard-item">
                        <h4>Generate Report</h4>
                        <asp:Panel ID="Panel1" runat="server">
                            <asp:Label ID="lblReportType" runat="server" Text="Select Report Type:" />
                            <asp:DropDownList ID="ddlReportType" runat="server" CssClass="form-control">
                                <asp:ListItem Text="-Select-" Value="0" />
                                <asp:ListItem Text="Campaign Performance" Value="CampaignPerformance" />
                                <asp:ListItem Text="Banner Performance" Value="BannerPerformance" />
                                <asp:ListItem Text="Website Performance" Value="WebsitePerformance" />
                                <asp:ListItem Text="Zone Performance" Value="ZonePerformance" />
                                <asp:ListItem Text="Custom Report" Value="CustomReport" />
                            </asp:DropDownList>
                            <br />
                            <asp:Label ID="Label10" runat="server" Text="Advertiser:" />
                            <asp:DropDownList ID="AdDDL" runat="server" CssClass="form-control">
                                <asp:ListItem Text="-Select All-" Value="0" />
                                <asp:ListItem Text="Advertiser 1" Value="1" />
                                <asp:ListItem Text="Advertiser 2" Value="2" />
                                <asp:ListItem Text="Advertiser 3" Value="3" />
                                <asp:ListItem Text="Advertiser 4" Value="4" />
                                <asp:ListItem Text="Advertiser 5" Value="5" />
                            </asp:DropDownList>
                            <br />
                            <asp:Label ID="Label9" runat="server" Text="Campaign:" />
                            <asp:DropDownList ID="CampaignDDL" runat="server" CssClass="form-control">
                                <asp:ListItem Text="-Select All-" Value="0" />
                                <asp:ListItem Text="Campaign A" Value="1" />
                                <asp:ListItem Text="Campaign B" Value="2" />
                                <asp:ListItem Text="Campaign C" Value="3" />
                                <asp:ListItem Text="Campaign D" Value="4" />
                                <asp:ListItem Text="Campaign E" Value="5" />
                            </asp:DropDownList>
                            <br />
                            <asp:Label ID="lblZoneType" runat="server" Text="Website:" />
                            <asp:DropDownList ID="WebsiteDDL" runat="server" CssClass="form-control">
                                <asp:ListItem Text="-Select All-" Selected="True" Value="0" />
                                <asp:ListItem Text="Website A" Value="1" />
                                <asp:ListItem Text="Website B" Value="2" />
                                <asp:ListItem Text="Website C" Value="3" />
                                <asp:ListItem Text="Website D" Value="4" />
                                <asp:ListItem Text="Website E" Value="5" />
                            </asp:DropDownList>
                            <br />
                        </asp:Panel>
                    </div>
                    <div class="dashboard-item">
                        <asp:Panel ID="Panel2" runat="server">
                            <asp:Label ID="Label7" runat="server" Text="Created From Date:" />
                            <asp:TextBox ID="txtFromDate" runat="server" TextMode="Date" CssClass="form-control" Placeholder="YYYY-MM-DD" />
                            <br />
                            <asp:Label ID="Label8" runat="server" Text="Created To Date:" />
                            <asp:TextBox ID="txtToDate" runat="server" TextMode="Date" CssClass="form-control" Placeholder="YYYY-MM-DD" />
                            <br />
                            <asp:Label ID="lblFilters" runat="server" Text="Additional Filters:" />
                            <asp:TextBox ID="txtFilter" runat="server" CssClass="form-control" Placeholder="Enter filters (optional)" />
                            <br />
                            <div class="form-group text-center">
                                <asp:Label ID="ErrLbl" runat="server" Height="15px" BackColor="Transparent" Text="" ForeColor="Red" Font-Size="Smaller"></asp:Label>
                            </div>
                            <asp:Button ID="GenerateReportButton" runat="server" CssClass="btn-primary" Text="Generate Report" OnClick="GenerateReportButton_Click" />
                        </asp:Panel>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div class="dashboard-section">
            <div class="dashboard-item">
                <h4>Registered Zones</h4>
                <div>
                    <rsweb:reportviewer id="ReportViewer1" runat="server" width="100%" height="100%">
                    </rsweb:reportviewer>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
