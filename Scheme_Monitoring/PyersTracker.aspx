<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="PyersTracker.aspx.cs" Inherits="PyersTracker" EnableEventValidation="false" ValidateRequest="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="assets/css/CalendarStyle.css" rel="stylesheet" />
    <div class="main-content">
        <div class="page-content">
            <div class="container-fluid">
                <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
                </cc1:ToolkitScriptManager>
                <asp:UpdatePanel ID="up" runat="server">
                    <ContentTemplate>
                        <div id="divCreate" runat="server">
                            <div class="row">
                                <div class="col-12 mb-3">
                                    <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                        <h4 class="mb-sm-0">Create / Update Crematorium Tracker</h4>
                                        <div class="page-title-right">
                                            <ol class="breadcrumb m-0">
                                                <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                                <li class="breadcrumb-item">Project Master</li>
                                                <li class="breadcrumb-item active">Crematorium Tracker</li>
                                            </ol>
                                        </div>
                                    </div>
                                </div>
                                <!--end col-->
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="card">
                                        <div class="card-header align-items-center d-flex">
                                            <h4 class="card-title mb-0 flex-grow-1">City Profile</h4>
                                        </div>
                                        <!-- end card header -->
                                        <div class="card-body">
                                            <div class="live-preview">
                                                <div class="row gy-4">
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div id="div1" runat="server">
                                                            <asp:Label ID="lblYear" runat="server" Text="Year*" CssClass="form-label"></asp:Label>
                                                            <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-select">
                                                                <asp:ListItem Value="0">-select year-</asp:ListItem>
                                                                <asp:ListItem Value="2024">2024</asp:ListItem>
                                                                <asp:ListItem Value="2025">2025</asp:ListItem>
                                                                <asp:ListItem Value="2026">2026</asp:ListItem>
                                                                <asp:ListItem Value="2027">2027</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-3 col-md-6">
                                                        <div id="div2" runat="server">
                                                            <asp:Label ID="lblMonth" runat="server" Text="Month*" CssClass="form-label"></asp:Label>
                                                            <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-select"></asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-3 col-md-6">
                                                        <div id="divZone" runat="server">
                                                            <asp:Label ID="lblZoneH" runat="server" Text="Zone*" CssClass="form-label"></asp:Label>
                                                            <asp:DropDownList ID="ddlZone" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-3 col-md-6">
                                                        <div id="divCircle" runat="server">
                                                            <asp:Label ID="lblCircleH" runat="server" Text="Circle*" CssClass="form-label"></asp:Label>
                                                            <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-3 col-md-6">
                                                        <div id="divDivision" runat="server">
                                                            <asp:Label ID="lblDivisionH" runat="server" Text="Division*" CssClass="form-label"></asp:Label>
                                                            <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-select"></asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-3 col-md-6">
                                                        <div id="divPopulation" runat="server">
                                                            <asp:Label ID="lblUrbanPopulation" runat="server" Text="Total urban population*" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtUrbanPopulation" runat="server" CssClass="form-control" AutoPostBack="true" onkeyup="isNumericVal(this);" OnTextChanged="txtUrbanPopulation_TextChanged" TextMode="Number"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-3 col-md-6">
                                                        <div id="divPopulationCreamtion80" runat="server">
                                                            <asp:Label ID="lblPopulationCreamtion80" runat="server" Text="Population likely to be cremated (80%)" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtPopulationCreamtion80"  ReadOnly="true" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);" Enabled="False" Font-Bold="True"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-3 col-md-6">
                                                        <div id="divDeathPer1000" runat="server">
                                                            <asp:Label ID="lblDeathPer1000" runat="server" Text="Death rate per 1000 per year*" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtDeathPer1000" runat="server" AutoPostBack="true" CssClass="form-control" onkeyup="isNumericVal(this);" OnTextChanged="txtDeathPer1000_TextChanged"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-3 col-md-6">
                                                        <div id="divEstDeath10Buffer" runat="server">
                                                            <asp:Label ID="lblEstDeath10Buffer" runat="server" Text="Estimated no. of deaths per day (incl 10% buffer)" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtEstDeath10Buffer" ReadOnly="true" Enabled="False" Font-Bold="True" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-3 col-md-6">
                                                        <div>
                                                            <%--<asp:Label ID="Label10" runat="server" Text="Upload Budget Sanctioned GO" CssClass="form-label"></asp:Label>--%>
                                                            <asp:FileUpload ID="flUploadGO" runat="server" Visible="False" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <!--end row-->
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="card">
                                        <div class="card-header align-items-center d-flex">
                                            <h4 class="card-title mb-0 flex-grow-1">No. of existing pyres (excluding under construction) as per city administration</h4>
                                        </div>
                                        <div class="card-body">
                                            <div class="live-preview">
                                                <div class="row gy-4">
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div id="div3" runat="server">
                                                            <asp:Label ID="lblConventional" runat="server" Text="Conventional (Mortal remian handling capacity per pyre per day=1)*" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtConventional" runat="server" CssClass="form-control" AutoPostBack="true" onkeyup="isNumericVal(this);" OnTextChanged="ExistingPyersHandlingCapacity" TextMode="Number"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-3 col-md-6">
                                                        <div id="div5" runat="server">
                                                            <asp:Label ID="lblImprovisedWood" runat="server" Text="Improvised Wood (Mortal remian handling capacity per pyre per day=2)*" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtImprovisedWood" runat="server" CssClass="form-control" AutoPostBack="true" onkeyup="isNumericVal(this);" OnTextChanged="ExistingPyersHandlingCapacity" TextMode="Number"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-3 col-md-6">
                                                        <div id="div4" runat="server">
                                                            <asp:Label ID="lblGas" runat="server" Text="Gas (Mortal remian handling capacity per pyre per day=4)*" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtGas" runat="server" CssClass="form-control" AutoPostBack="true" onkeyup="isNumericVal(this);" OnTextChanged="ExistingPyersHandlingCapacity" TextMode="Number"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-3 col-md-6">
                                                        <div id="div6" runat="server">
                                                            <asp:Label ID="lblElectric" runat="server" Text="Electric (Mortal remian handling capacity per pyre per day=4)*" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtElectric" runat="server" CssClass="form-control" AutoPostBack="true" onkeyup="isNumericVal(this);" OnTextChanged="ExistingPyersHandlingCapacity" TextMode="Number"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-3 col-md-6">
                                                        <div id="div7" runat="server">
                                                            <asp:Label ID="lblExistCapacity" runat="server" Text="Existing 'mortal remains' handling capacity" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtExistCapacity" runat="server"  Enabled="False" Font-Bold="True" CssClass="form-control" ReadOnly="true" onkeyup="isNumericVal(this);" TextMode="Number"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="card">
                                        <div class="card-header align-items-center d-flex">
                                            <h4 class="card-title mb-0 flex-grow-1">Decision for next step</h4>
                                        </div>
                                        <div class="card-body">
                                            <div class="live-preview">
                                                <div class="row gy-4">
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div id="div8" runat="server">
                                                            <asp:Label ID="lblUpgradeExisting" runat="server" Text="Decision : Upgrade existing or build new dependent on existing capacity" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtUpgradeExisting" ReadOnly="true" Enabled="False" Font-Bold="True" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    
                                                    <div class="col-xxl-4 col-md-6">
                                                        <div id="div9" runat="server">
                                                            <asp:Label ID="lblRemainingToBeHandled" runat="server" Text="Total estimated deaths per day - Existing 'mortal remains' handling capacity of EFCs = Remaining 'mortal remains' to be handled" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtRemainingToBeHandled" ReadOnly="true" Enabled="False" Font-Bold="True" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row" runat="server" id="divUpgradation">
                                <div class="col-lg-12">
                                    <div class="card">
                                        <div class="card-header align-items-center d-flex">
                                            <h4 class="card-title mb-0 flex-grow-1">Upgradation of existing conventional pyres</h4>
                                        </div>
                                        <div class="card-body">
                                            <div class="live-preview">
                                                <div class="row gy-4">
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div id="div10" runat="server">
                                                            <asp:Label ID="lblUpgradeImprovisedWood" runat="server" Text="Improvised Wood" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtUpgradeImprovisedWood" OnTextChanged="CalculateCheckOn" runat="server" CssClass="form-control" AutoPostBack="true" onkeyup="isNumericVal(this);"  TextMode="Number"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-3 col-md-6">
                                                        <div id="div11" runat="server">
                                                            <asp:Label ID="lblUpgradeGas" runat="server" Text="Gas" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtUpgradeGas" OnTextChanged="CalculateCheckOn" runat="server"  CssClass="form-control" AutoPostBack="true" onkeyup="isNumericVal(this);" TextMode="Number"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-3 col-md-6">
                                                        <div id="div12" runat="server">
                                                            <asp:Label ID="lblUpgradeElectric" runat="server" Text="Electric" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtUpgradeElectric" OnTextChanged="CalculateCheckOn" runat="server" CssClass="form-control" AutoPostBack="true" onkeyup="isNumericVal(this);" TextMode="Number"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <%--<div class="col-xxl-3 col-md-6">
                                                        <div id="div13" runat="server">
                                                            <asp:Label ID="lblCheckOn" runat="server" Text="Check on total existing conventional pyres" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtCheckOn" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="card">
                                        <div class="card-header align-items-center d-flex">
                                            <h4 class="card-title mb-0 flex-grow-1">Current status for installation of EFPs</h4>
                                        </div>
                                        <div class="card-body">
                                            <div class="live-preview">
                                                <div class="row gy-4">
                                                    <div class="col-xxl-3 col-md-6">
                                                        <div id="div13" runat="server">
                                                            <asp:Label ID="lblRemainingCapacity" runat="server" Text="Remaining capacity (ideally should be negative)*" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtRemainingCapacity" AutoPostBack="true" runat="server" CssClass="form-control" OnTextChanged="GetCommentOnCapacity" onkeyup="isNumericVal(this);" TextMode="Number"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    
                                                    <div class="col-xxl-4 col-md-6">
                                                        <div id="div14" runat="server">
                                                            <asp:Label ID="lblCommentOnCapacity" runat="server" Text="Comment on capacity" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtCommentOnCapacity" ReadOnly="true"  Enabled="False" Font-Bold="True" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="card">
                                        <div class="card-header align-items-center d-flex">
                                            <h4 class="card-title mb-0 flex-grow-1">Final output : Upgrade existing ones</h4>
                                        </div>
                                        <div class="card-body">
                                            <div class="live-preview">
                                                <div class="row gy-4">
                                                    <div class="col-xxl-4 col-md-6">
                                                        <div id="div16" runat="server">
                                                            <asp:Label ID="lblPyresToBeRevamped" runat="server" Text="Number of conventional pyres to be revamped" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtPyresToBeRevamped" ReadOnly="true"  Enabled="False" Font-Bold="True" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="col-xxl-4 col-md-6">
                                                        <div id="div15" runat="server">
                                                            <asp:Label ID="lblFundsRequired" runat="server" Text="Funds required in lacs (only includes pyres and not other facilities)" CssClass="form-label"></asp:Label>
                                                            <asp:TextBox ID="txtFundsRequired" AutoPostBack="true" runat="server" CssClass="form-control" onkeyup="isNumericVal(this);" TextMode="Number"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xxl-12 col-md-12">
                                    <div>
                                        <asp:Button ID="btnSave" Text="Save" OnClick="btnSave_Click" runat="server" CssClass="btn bg-success text-white"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSave" />
                    </Triggers>
                </asp:UpdatePanel>
                <asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="up">
                    <ProgressTemplate>
                        <div style="position: fixed; z-index: 999; height: 100%; width: 100%; top: 0; filter: alpha(opacity=60); opacity: 0.6; -moz-opacity: 0.8; cursor: not-allowed;">
                            <div style="z-index: 1000; margin: 300px auto; padding: 10px; width: 130px; background-color: transparent; border-radius: 1px; filter: alpha(opacity=100); opacity: 1; -moz-opacity: 1;">
                                <img src="assets/images/mb/mbloader.gif" style="height: 150px; width: 150px;" />
                            </div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function downloadGO(obj) {
            var path = document.getElementById('ctl00_ContentPlaceHolder1_hf_GO_Path').value;
            if (path.trim() == "") {
                obj.href = '#';
                alert('File Not Found');
                return false;
            }
            else {
                window.open(location.origin + path, "_blank", "", false);
                //location.href = window.location.origin + GO_FilePath;
                return false;
            }
        }
    </script>

    <script>
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            jQuery(function ($) {
                $('.modalBackground1').click(function () {
                    var id = $(this).attr('id').replace('_backgroundElement', '');
                    $find(id).hide();
                });
            })
        });
    </script>
</asp:Content>
