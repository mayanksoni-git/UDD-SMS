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
                                    <h4 class="mb-sm-0">Crematorium Main Tracker</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Home</a></li>
                                            <li class="breadcrumb-item">MIS</li>
                                            <li class="breadcrumb-item active">Crematorium Main Tracker</li>
                                        </ol>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-12">
                                <div class="card">
                                    <div class="card-header align-items-center d-flex">
                                        <h4 class="card-title mb-0 flex-grow-1">Crematorium Main Tracker</h4>
                                        <a class="btn btn-primary" href="PyersTracker.aspx">
                                            <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor" class="bi bi-database-fill-add" viewBox="0 0 16 16">
                                                <path d="M12.5 16a3.5 3.5 0 1 0 0-7 3.5 3.5 0 0 0 0 7m.5-5v1h1a.5.5 0 0 1 0 1h-1v1a.5.5 0 0 1-1 0v-1h-1a.5.5 0 0 1 0-1h1v-1a.5.5 0 0 1 1 0M8 1c-1.573 0-3.022.289-4.096.777C2.875 2.245 2 2.993 2 4s.875 1.755 1.904 2.223C4.978 6.711 6.427 7 8 7s3.022-.289 4.096-.777C13.125 5.755 14 5.007 14 4s-.875-1.755-1.904-2.223C11.022 1.289 9.573 1 8 1" />
                                                <path d="M2 7v-.839c.457.432 1.004.751 1.49.972C4.722 7.693 6.318 8 8 8s3.278-.307 4.51-.867c.486-.22 1.033-.54 1.49-.972V7c0 .424-.155.802-.411 1.133a4.51 4.51 0 0 0-4.815 1.843A12 12 0 0 1 8 10c-1.573 0-3.022-.289-4.096-.777C2.875 8.755 2 8.007 2 7m6.257 3.998L8 11c-1.682 0-3.278-.307-4.51-.867-.486-.22-1.033-.54-1.49-.972V10c0 1.007.875 1.755 1.904 2.223C4.978 12.711 6.427 13 8 13h.027a4.55 4.55 0 0 1 .23-2.002m-.002 3L8 14c-1.682 0-3.278-.307-4.51-.867-.486-.22-1.033-.54-1.49-.972V13c0 1.007.875 1.755 1.904 2.223C4.978 15.711 6.427 16 8 16c.536 0 1.058-.034 1.555-.097a4.5 4.5 0 0 1-1.3-1.905" />
                                            </svg> Add New</a>
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
                                                     <label class="d-block">&nbsp;</label>
                                                        <asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_Click" runat="server" CssClass="btn bg-success text-white"></asp:Button>
                                                   
                                                </div>

                                                <%--<div class="col-xxl-3 col-md-6">
                                                    <div>
                                                        <br />
                                                        <asp:Button ID="btnCreateNew" Text="Create New" OnClick="btnCreateNew_Click" runat="server" CssClass="btn btn-danger"></asp:Button>
                                                    </div>
                                                </div>--%>
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
                                            <h4 class="card-title mb-0 flex-grow-1">Crematorium Main Tracker</h4>
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
                                                        <asp:Button ID="btnExport" runat="server" Text="Export to Excel" OnClick="btnExport_Click" CssClass="btn btn-primary" />
                                                        <asp:Button ID="btnPrintA4" runat="server" Text="Export in A4"  OnClientClick="PrintMainTracker(); return false;"  CssClass="btn btn-primary"  />
                                                        <%--Same grid is used on Page PrintMainTracker.aspx and ExportToExcel.aspx page also, please change there as well if needed--%>

                                                        <asp:GridView ID="MainTracker" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnRowDataBound="MainTracker_RowDataBound">
                                                            <Columns> 
                                                                <asp:BoundField DataField="PyresTracker_Id" HeaderText="Pyres Tracker Id">
                                                                    <HeaderStyle CssClass="displayStyle" />
                                                                    <ItemStyle CssClass="displayStyle" />
                                                                    <FooterStyle CssClass="displayStyle" />
                                                                </asp:BoundField>
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
                                                                            <th colspan="7" style="background-color: #F1C232">B</th>
                                                                            <th colspan="5" style="background-color: #F1C232">C</th>
                                                                            <th colspan="2" style="background-color: #76A5AF">C2</th>
                                                                            <th colspan="5" style="background-color: #E69138">D1</th>
                                                                            <th colspan="2" style="background-color: #E69138">D3</th>
                                                                            <th colspan="2" style="background-color: #76A5AF">E</th>
                                                                        </tr>
                                                                        <tr>
                                                                            <th></th>
                                                                            <th rowspan="3" style="background-color: #CFE2F3">Sr.No.</th>
                                                                            <th colspan="7" style="background-color: #C9DAF8">City Profile</th>
                                                                            <th colspan="5" style="background-color: #D9D2E9">No. of existing pyres (excluding under construction) as per city administration</th>
                                                                            <th colspan="2" style="background-color: #C9DAF8">Decision for next step</th>
                                                                            <th colspan="5" style="background-color: #D9D2E9">Upgradation of existing conventional pyres</th>
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
                                                                            <th rowspan="2" style="background-color: #C9DAF8">No of Existing Crematorium</th>
                                                                            <th rowspan="2" style="background-color: #D9D2E9">Conventional</th>
                                                                            <th rowspan="2" style="background-color: #D9D2E9">Improvised Wood</th>
                                                                            <th rowspan="2" style="background-color: #D9D2E9">Gas</th>
                                                                            <th rowspan="2" style="background-color: #D9D2E9">Electric </th>
                                                                            <th rowspan="2" style="background-color: #D9D2E9">Existing &#39;mortal remains&#39; handling capacity</th>
                                                                            <th rowspan="2" style="background-color: #C9DAF8">Decision: Upgrade existing or build new dependent on existing capacity</th>
                                                                            <th rowspan="2" style="background-color: #C9DAF8">Total estimated deaths per day - Existing &#39;mortal remains&#39; handling capacity of EFCs = Remaining &#39;mortal remains&#39; to be handled</th>
                                                                            <th style="background-color: #D9D2E9">Improvised Wood </th>
                                                                            <th style="background-color: #D9D2E9">Gas </th>
                                                                            <th style="background-color: #D9D2E9">Electric </th>
                                                                            <th rowspan="2" style="background-color: #D9D2E9">Amenities Required</th>
                                                                            <th rowspan="2" style="background-color: #D9D2E9">Fun for Amenities(in Lakhs)</th>
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
                                                                            <th style="background-color: #A4C2F4">Funds required in Lakhs (only includes pyres and not other facilities)</th>
                                                                        </tr>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <td>
                                                                            <asp:ImageButton ID="btnEdit" Width="20px" Height="20px" OnClick="btnEdit_Click" ImageUrl="~/assets/images/edit_btn.png" runat="server" /></td>
                                                                        <td><%# Container.DataItemIndex + 1 %></td>
                                                                        <td><%# Eval("CircleName")%> <%# Eval("MonthName")%> <%# Eval("Year")%></td>
                                                                        <td><%# Eval("DivisionName")%></td>
                                                                        <td><%# Eval("UrbanPopulation")%></td>
                                                                        <td><%# Eval("PopulationCreamtion80")%></td>
                                                                        <td><%# Eval("DeathPer1000")%></td>
                                                                        <td><%# Eval("EstDeath10Buffer")%></td>
                                                                        <%--<td><%# Eval("ExistCMTR")%></td>--%>
                                                                        <td>
                                                                            <asp:LinkButton
                                                                                Text='<%# Eval("Filled")+" + "+Eval("RemainingToBeFill")+" = "+ Eval(" ExistCMTR") %>'
                                                                                CssClass="btn btn-primary btn-sm"
                                                                                ToolTip="Click to Open Crematorium Detail (Filled Crematorium Detail + Reamaining Crematorium Detail to be fill = Existing Crematorium)"
                                                                                runat="server"
                                                                                OnClientClick='<%# "openCrematoriumDetail(\"" + Eval("Zone") + "\", \"" + Eval("Circle") + "\", \"" + Eval("Division") + "\", \"" + Eval("Year") + "\", \"" + Eval("Month") + "\"); return false;" %>'>
                                                                            </asp:LinkButton>
                                                                        </td>
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
                                                                        <td><%# Eval("AmenitiesRequired")%></td>
                                                                        <td><%# Eval("FundforAmeneties")%></td>
                                                                        <td><%# Eval("RemainingCapacity")%></td>
                                                                        <td><%# Eval("CommentOnCapacity")%></td>
                                                                        <td><%# Eval("PyresToBeRevamped")%></td>
                                                                        <%--<td><asp:Label runat="server" ToolTip='(ImprovisedWood*CostofImprovisedWood)+(Gas*CostOfGas)+(Electric*CostOfElectric)'><%# Eval("FundsRequired")%>*</asp:Label></td>--%>
                                                                        <td>
                                                                            <asp:Label ID="lblFundsRequired" runat="server" CssClass="cursor-pointer" ToolTip='<%# GetToolTipText(Container.DataItem) %>'><%# Eval("FundsRequired")%></asp:Label>
                                                                            <asp:ImageButton ID="btnInfo" Width="20px" Height="20px" runat="server" ImageUrl="~/assets/images/info_icon.png" CssClass="ml-2" ToolTip='<%# GetToolTipText(Container.DataItem) %>' />
                                                                        </td>
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
                                                            <EmptyDataTemplate>
                                                                <tr>
                                                                    <td colspan="22" style="text-align: center; font-weight: bold; color: red;">No records found</td>
                                                                </tr>
                                                            </EmptyDataTemplate>
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
            <%--<asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="up">
                <ProgressTemplate>
                    <div style="position: fixed; z-index: 999; height: 100%; width: 100%; top: 0; background-color: Black; filter: alpha(opacity=60); opacity: 0.6; -moz-opacity: 0.8; cursor: not-allowed;">
                        <div style="z-index: 1000; margin: 300px auto; padding: 10px; width: 130px; background-color: transparent; border-radius: 1px; filter: alpha(opacity=100); opacity: 1; -moz-opacity: 1;">
                            <img src="assets/images/mb/mbloader.gif" style="height: 150px; width: 150px;" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>--%>
        </div>
    </div>

    <script type="text/javascript">
        function PrintMainTracker() {
            window.open('PrintMainTracker.aspx', '_blank');
        }
    </script>

    <script type="text/javascript">
        function openCrematoriumDetail(Zone, Circle, Division, Year, Month) {
            var url = 'RptCrematoriumDetail.aspx?Zone=' + encodeURIComponent(Zone) + '&Circle=' + encodeURIComponent(Circle) + '&Division=' + encodeURIComponent(Division) + '&Year=' + encodeURIComponent(Year) + '&Month=' + encodeURIComponent(Month);
            window.open(url, '_blank');
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



