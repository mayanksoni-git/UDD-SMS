<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="RptPyresTracker.aspx.cs" Inherits="RptPyresTracker" EnableEventValidation="false" ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-content">
        <div class="page-content">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-12">
                                <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                    <h4 class="mb-sm-0">Report Crematorium Tracker</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                            <li class="breadcrumb-item">Project Master</li>
                                            <li class="breadcrumb-item active">Report Crematorium Tracker</li>
                                        </ol>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Report Crematorium Tracker</h4>
                                    </div>
                                    <div class="card-body">
                                        <div class="live-preview">
                                            <div class="row gy-4">
                                                
                                                <div class="col-xxl-3 col-md-6">
                                                    <div>
                                                        <asp:Label ID="lblZoneH" runat="server" Text="Zone" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlZone" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6" id="divCircle" runat="server">
                                                    <div>
                                                        <asp:Label ID="lblCircleH" runat="server" Text="Circle" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6" id="divDivision" runat="server">
                                                    <div>
                                                        <asp:Label ID="lblDivisionH" runat="server" Text="Division" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-select"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div>
                                                        
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div>
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
                                                    <div>
                                                        <asp:Label ID="lblMonth" runat="server" Text="Month*" CssClass="form-label"></asp:Label>
                                                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-select"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div>
                                                        <br />
                                                        <asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_Click" runat="server" CssClass="btn btn-warning"></asp:Button>
                                                    </div>
                                                </div>

                                                <div class="col-xxl-3 col-md-6">
                                                    <div>
                                                        <br />
                                                        <asp:Button ID="btnCreateNew" Text="Create New" OnClick="btnCreateNew_Click" runat="server" CssClass="btn btn-danger"></asp:Button>
                                                    </div>
                                                </div>
                                            </div>
                                            <!--end row-->
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--end col-->
                        </div>

                        <div runat="server" visible="false" id="divData">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="card">
                                        <div class="card-header align-items-center d-flex">
                                            <h4 class="card-title mb-0 flex-grow-1">Main Crematorium Tracker</h4>
                                        </div>
                                        <!-- end card header -->
                                        <div class="card-body">
                                            <div class="live-preview">
                                                <div class="row gy-12">
                                                    <!-- div.table-responsive -->
                                                    <div class="clearfix" id="dtOptions" runat="server">
                                                        <div class="pull-right tableTools-container"></div>
                                                    </div>
                                                    <!-- div.dataTables_borderWrap -->
                                                    <div style="overflow: auto">
                                                        <asp:GridView ID="MainTracker" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender">
                                                            <Columns>  
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <%--<tr>
                                                                            <th></th>
                                                                            <th rowspan="4">Edit</th>
                                                                            <th>A</th>
                                                                            <th colspan="6">B</th>
                                                                            <th colspan="5">C</th>
                                                                            <th colspan="2">C2</th>
                                                                            <th colspan="3">D1</th>
                                                                            <th colspan="2">D3</th>
                                                                            <th colspan="2">E</th>
                                                                        </tr>
                                                                        <tr>
                                                                            <th></th>
                                                                            <th rowspan="3">Sr.No.</th>
                                                                            <th colspan="6">City Profile</th>
                                                                            <th colspan="5">No. of existing pyres (excluding under construction) as per city administration</th>
                                                                            <th colspan="2">Decision for next step</th>
                                                                            <th colspan="3">Upgradation of existing conventional pyres</th>
                                                                            <th colspan="2">Current status for installation of EFPs</th>
                                                                            <th colspan="2">Final output</th>
                                                                        </tr>
                                                                        <tr>
                                                                            <th></th>
                                                                            <th rowspan="2">District</th>
                                                                            <th rowspan="2">ULB</th>
                                                                            <th rowspan="2">Total urban population</th>
                                                                            <th rowspan="2">Population likely to be cremated (80%)</th>
                                                                            <th rowspan="2">Death rate per 1000 per year</th>
                                                                            <th rowspan="2">Estimated no. of deaths per day (incl 10% buffer)</th>
                                                                            <th rowspan="2">Conventional</th>
                                                                            <th rowspan="2">Improvised Wood</th>
                                                                            <th rowspan="2">Gas</th>
                                                                            <th rowspan="2">Electric </th>
                                                                            <th rowspan="2">Existing &#39;mortal remains&#39; handling capacity**</th>
                                                                            <th rowspan="2">Decision: Upgrade existing or build new dependent on existing capacity</th>
                                                                            <th rowspan="2">Total estimated deaths per day - Existing &#39;mortal remains&#39; handling capacity of EFCs = Remaining &#39;mortal remains&#39; to be handled</th>
                                                                            <th>Improvised Wood </th>
                                                                            <th>Gas </th>
                                                                            <th>Electric </th>
                                                                            <th rowspan="2">Remaining capacity (ideally should be negative)</th>
                                                                            <th rowspan="2">Comment on capacity</th>
                                                                            <th colspan="2">Upgrade existing ones</th>
                                                                        </tr>
                                                                        <tr>
                                                                            <th></th>
                                                                            <th>2</th>
                                                                            <th>4</th>
                                                                            <th>4</th>
                                                                            <th>Number of conventional pyres to be revamped</th>
                                                                            <th>Funds required in lacs (only includes pyres and not other facilities)</th>
                                                                        </tr>--%>

                                                                        <tr>
                    <th></th>
                    <th rowspan="4" style="background-color: #CFE2F3">Edit</th>
                    <th style="background-color: #76A5AF">A</th>
                    <th colspan="6" style="background-color: #F1C232">B</th>
                    <th colspan="5" style="background-color: #F1C232">C</th>
                    <th colspan="2" style="background-color: #76A5AF">C2</th>
                    <th colspan="3" style="background-color: #E69138">D1</th>
                    <th colspan="2" style="background-color: #E69138">D3</th>
                    <th colspan="2" style="background-color: #76A5AF">E</th>
                </tr>
                <tr>
                    <th></th>
                    <th rowspan="3" style="background-color: #CFE2F3">Sr.No.</th>
                    <th colspan="6" style="background-color: #C9DAF8">City Profile</th>
                    <th colspan="5" style="background-color: #D9D2E9">No. of existing pyres (excluding under construction) as per city administration</th>
                    <th colspan="2" style="background-color: #C9DAF8">Decision for next step</th>
                    <th colspan="3" style="background-color: #D9D2E9">Upgradation of existing conventional pyres</th>
                    <th colspan="2" style="background-color: #D9D2E9">Current status for installation of EFPs</th>
                    <th colspan="2" style="background-color: #00FFFF">Final output</th>
                </tr>
                <tr>
                    <th></th>
                    <th rowspan="2" style="background-color: #C9DAF8">District</th>
                    <th rowspan="2" style="background-color: #C9DAF8">ULB</th>
                    <th rowspan="2" style="background-color: #C9DAF8">Total urban population</th>
                    <th rowspan="2" style="background-color: #C9DAF8">Population likely to be cremated (80%)</th>
                    <th rowspan="2" style="background-color: #C9DAF8">Death rate per 1000 per year</th>
                    <th rowspan="2" style="background-color: #C9DAF8">Estimated no. of deaths per day (incl 10% buffer)</th>
                    <th rowspan="2" style="background-color: #D9D2E9">Conventional</th>
                    <th rowspan="2" style="background-color: #D9D2E9">Improvised Wood</th>
                    <th rowspan="2" style="background-color: #D9D2E9">Gas</th>
                    <th rowspan="2" style="background-color: #D9D2E9">Electric </th>
                    <th rowspan="2" style="background-color: #D9D2E9">Existing &#39;mortal remains&#39; handling capacity**</th>
                    <th rowspan="2" style="background-color: #C9DAF8">Decision: Upgrade existing or build new dependent on existing capacity</th>
                    <th rowspan="2" style="background-color: #C9DAF8">Total estimated deaths per day - Existing &#39;mortal remains&#39; handling capacity of EFCs = Remaining &#39;mortal remains&#39; to be handled</th>
                    <th style="background-color: #D9D2E9">Improvised Wood </th>
                    <th style="background-color: #D9D2E9">Gas </th>
                    <th style="background-color: #D9D2E9">Electric </th>
                    <th rowspan="2" style="background-color: #C9DAF8">Remaining capacity (ideally should be negative)</th>
                    <th rowspan="2" style="background-color: #D9D2E9">Comment on capacity</th>
                    <th colspan="2" style="background-color: #A4C2F4">Upgrade existing ones</th>
                </tr>
                <tr>
                    <th></th>
                    <th style="background-color: #D9D2E9">2</th>
                    <th style="background-color: #D9D2E9">4</th>
                    <th style="background-color: #D9D2E9">4</th>
                    <th style="background-color: #A4C2F4">Number of conventional pyres to be revamped</th>
                    <th style="background-color: #A4C2F4">Funds required in lacs (only includes pyres and not other facilities)</th>
                </tr>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <td><asp:ImageButton ID="btnEdit" Width="20px" Height="20px" OnClick="btnEdit_Click" ImageUrl="~/assets/images/edit_btn.png" runat="server" /></td>
                                                                        <td><%# Container.DataItemIndex + 1 %></td>
                                                                        <td><%# Eval("CircleName")%></td>
                                                                        <td><%# Eval("DivisionName")%></td>
                                                                        <td><%# Eval("UrbanPopulation")%></td>
                                                                        <td><%# Eval("PopulationCreamtion80")%></td>
                                                                        <td><%# Eval("DeathPer1000")%></td>
                                                                        <td><%# Eval("EstDeath10Buffer")%></td>
                                                                        <td><%# Eval("Conventional")%></td>
                                                                        <td><%# Eval("ImprovisedWood")%></td>
                                                                        <td><%# Eval("Gas")%></td>
                                                                        <td><%# Eval("Electric")%></td>
                                                                        <td><%# Eval("ExistCapacity")%></td>
                                                                        <td><%# Eval("UpgradeExisting")%></td>
                                                                        <td><%# Eval("RemainingToBeHandled")%></td>
                                                                        <td><%# Eval("UpgradeImprovisedWood")%></td>
                                                                        <td><%# Eval("UpgradeGas")%></td>
                                                                        <td><%# Eval("UpgradeElectric")%></td>
                                                                        <td><%# Eval("RemainingCapacity")%></td>
                                                                        <td><%# Eval("CommentOnCapacity")%></td>
                                                                        <td><%# Eval("PyresToBeRevamped")%></td>
                                                                        <td><%# Eval("FundsRequired")%></td>
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>
                                                                <%--<asp:TemplateField HeaderText="Sr. No.">
                                                                    <ItemTemplate>
                                                                        <%# Container.DataItemIndex + 1 %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Select">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btnEdit" Width="20px" Height="20px" OnClick="btnEdit_Click" ImageUrl="~/assets/images/edit_btn.png" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                                <%--<asp:BoundField HeaderText="Zone" DataField="ZoneName" />--%>
                                                                <%--<asp:BoundField HeaderText="District" DataField="CircleName" />
                                                                <asp:BoundField HeaderText="ULB" DataField="DivisionName" />
                                                                <asp:BoundField HeaderText="Year" DataField="Year" />
                                                                <asp:BoundField HeaderText="Month" DataField="MonthName" />
                                                                <asp:BoundField HeaderText="Total urban population" DataField="UrbanPopulation" />
                                                                <asp:BoundField HeaderText="Population likely to be cremated (80%)" DataField="PopulationCreamtion80" />
                                                                <asp:BoundField HeaderText="Death rate per 1000 per year" DataField="DeathPer1000" />
                                                                <asp:BoundField HeaderText="Estimated no. of deaths per day (incl 10% buffer)" DataField="EstDeath10Buffer" />
                                                                <asp:BoundField HeaderText="Conventional" DataField="Conventional" />
                                                                <asp:BoundField HeaderText="Improvised Wood" DataField="ImprovisedWood" />
                                                                <asp:BoundField HeaderText="Gas" DataField="Gas" />
                                                                <asp:BoundField HeaderText="Electric" DataField="Electric" />
                                                                <asp:BoundField HeaderText="Existing 'mortal remains' handling capacity**" DataField="ExistCapacity" />
                                                                <asp:BoundField HeaderText="Decision: Upgrade existing or build new dependent on existing capacity" DataField="UpgradeExisting" />
                                                                <asp:BoundField HeaderText="Total estimated deaths per day - Existing 'mortal remains' handling capacity of EFCs = Remaining" DataField="RemainingToBeHandled" />
                                                                <asp:BoundField HeaderText="Improvised Wood" DataField="UpgradeImprovisedWood" />
                                                                <asp:BoundField HeaderText="Gas" DataField="UpgradeGas" />
                                                                <asp:BoundField HeaderText="Electric" DataField="UpgradeElectric" />
                                                                <asp:BoundField HeaderText="Remaining capacity (ideally should be negative)" DataField="RemainingCapacity" />
                                                                <asp:BoundField HeaderText="Comment on capacity" DataField="CommentOnCapacity" />
                                                                <asp:BoundField HeaderText="Number of conventional pyres to be revamped" DataField="PyresToBeRevamped" />
                                                                <asp:BoundField HeaderText="Funds required in lacs (only includes pyres and not other facilities)" DataField="FundsRequired" />--%>
                                                                    
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                                <!--end row-->
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!--end col-->
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="up">
                <ProgressTemplate>
                    <div style="position: fixed; z-index: 999; height: 100%; width: 100%; top: 0; background-color: Black; filter: alpha(opacity=60); opacity: 0.6; -moz-opacity: 0.8; cursor: not-allowed;">
                        <div style="z-index: 1000; margin: 300px auto; padding: 10px; width: 130px; background-color: transparent; border-radius: 1px; filter: alpha(opacity=100); opacity: 1; -moz-opacity: 1;">
                            <img src="assets/images/mb/mbloader.gif" style="height: 150px; width: 150px;" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
    </div>
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



