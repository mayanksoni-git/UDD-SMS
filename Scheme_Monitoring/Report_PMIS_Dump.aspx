<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="Report_PMIS_Dump.aspx.cs" Inherits="Report_PMIS_Dump" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="main-content">
        <div class="main-content-inner">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>

                    <div class="page-content">
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <h3 class="header smaller lighter blue">Project List PMIS</h3>
                                        <div class="row">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:LinkButton runat="server" ID="lnkOnePager" Text="Download One Pager" OnClick="lnkOnePager_Click"></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">
                                                            Scheme
                                                        </label>
                                                        <asp:ListBox ID="ddlScheme" runat="server" class="chosen-select form-control" data-placeholder="Choose a Scheme..." SelectionMode="Multiple"></asp:ListBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-3" id="divZone" runat="server">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblZoneH" runat="server" Text="Zone" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlZone" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-3" id="divCircle" runat="server">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblCircleH" runat="server" Text="Circle" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlCircle" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCircle_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-3" id="divDivision" runat="server">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblDivisionH" runat="server" Text="Division" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-md-3" id="divDistrict" runat="server" visible="false">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">District* </label>
                                                        <asp:DropDownList ID="ddlDistrict" runat="server" class="chosen-select form-control" data-placeholder="Choose a District..."></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label1" runat="server" Text="Project Code" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:TextBox ID="txtProjectCode" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-3" id="divNodal" runat="server">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label7" runat="server" Text="Nodal Department" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlNodalDepartment" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">
                                                            Sanction Year
                                                            <div style="float: right; margin-left: 30px">
                                                                <asp:CheckBox runat="server" ID="chkFY_Wise" OnCheckedChanged="chkFY_Wise_CheckedChanged" AutoPostBack="true" ToolTip="Select To Switch Between Year Wise and Fnancial Year Wise Filter" />
                                                            </div>
                                                        </label>
                                                        <asp:ListBox SelectionMode="Multiple" ID="ddlSanctionYear" runat="server" class="chosen-select form-control" data-placeholder="Choose Project Sanction Year..."></asp:ListBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <br />
                                                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-warning" OnClick="btnSearch_Click" Text="Search" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <asp:ImageButton ID="btnDownload" OnClick="btnDownload_Click" runat="server" ImageUrl="~/assets/images/excel_import.png"
                                                            Width="60px" Height="50px"></asp:ImageButton>
                                                    </div>
                                                </div>

                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <asp:ImageButton ID="btnMPR" OnClick="btnMPR_Click" runat="server" ImageUrl="~/assets/images/mpr.jpg"
                                                            Width="80px" Height="80px"></asp:ImageButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- div.table-responsive -->
                                        <div id="dtOptions" runat="server" class="clearfix">
                                            <div class="pull-right tableTools-container">
                                            </div>
                                        </div>
                                        <!-- div.dataTables_borderWrap -->
                                        <div style="overflow: auto">
                                            <asp:GridView ID="grdPost" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender" OnRowDataBound="grdPost_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="ProjectWork_Id" HeaderText="ProjectWork_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectWork_Project_Id" HeaderText="ProjectWork_Project_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectWork_DivisionId" HeaderText="ProjectWork_DivisionId">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectWork_DistrictId" HeaderText="ProjectWork_DistrictId">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Physical_Component" HeaderText="Physical_Component">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="S No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                            <br />
                                                            <b><a href='Report_MasterProjectWorkMIS.aspx?ProjectWork_Id=<%# Eval("ProjectWork_Id") %>' target="_blank">MIS Report</a></b>
                                                            <br />
                                                            <b><a href='One_Pager_Detail.aspx?ProjectWork_Id=<%# Eval("ProjectWork_Id") %>' target="_blank">One Pager</a></b>
                                                            <br />
                                                            <div class="btn-group">
                                                                <button class="btn btn-app btn-pink btn-xs">
                                                                    <i class="ace-icon fa fa-share bigger-175"></i>
                                                                    MIS Steps										
                                                                </button>
                                                                <button data-toggle="dropdown" class="btn btn-app btn-pink btn-xs dropdown-toggle" aria-expanded="false">
                                                                    <span class="bigger-110 ace-icon fa fa-caret-down icon-only"></span>
                                                                </button>

                                                                <ul class="dropdown-menu dropdown-pink">
                                                                    <li>
                                                                        <a target="_blank" href="Report_MasterProjectWorkMIS.aspx?ProjectWork_Id=<%# Eval("ProjectWork_Id") %>">MIS Report</a>
                                                                    </li>
                                                                    <li class="divider"></li>
                                                                    <li>
                                                                        <a href="#" onclick="openBasicDetails(this);" project_id='<%# Eval("ProjectWork_Project_Id") %>' projectwork_id='<%# Eval("ProjectWork_Id") %>'>Go To Step 1: Basic Details</a>
                                                                    </li>

                                                                    <li>
                                                                        <a href="#" onclick="openGODetails(this);" project_id='<%# Eval("ProjectWork_Project_Id") %>' projectwork_id='<%# Eval("ProjectWork_Id") %>' district_id='<%# Eval("ProjectWork_DistrictId") %>'>Go To Step 2: GO Release Details</a>
                                                                    </li>
                                                                    <li>
                                                                        <a href="MasterProjectWorkMIS_3.aspx?ProjectWork_Id=<%# Eval("ProjectWork_Id") %>&Id=<%# Eval("ProjectWork_Project_Id") %>">Go To Step 3: Target & Achivments</a>
                                                                    </li>
                                                                    <li>
                                                                        <a href="MasterProjectWorkMIS_4.aspx?ProjectWork_Id=<%# Eval("ProjectWork_Id") %>&Id=<%# Eval("ProjectWork_Project_Id") %>">Go To Step 4: Physical Components</a>
                                                                    </li>
                                                                    <li>
                                                                        <a href="MasterProjectWorkMIS_5.aspx?ProjectWork_Id=<%# Eval("ProjectWork_Id") %>&Id=<%# Eval("ProjectWork_Project_Id") %>">Go To Step 5: Documents Upload</a>
                                                                    </li>
                                                                    <li>
                                                                        <a href="MasterProjectWorkMIS_6.aspx?ProjectWork_Id=<%# Eval("ProjectWork_Id") %>&Id=<%# Eval("ProjectWork_Project_Id") %>">Go To Step 6: UC Details and Other Issues</a>
                                                                    </li>
                                                                    <li>
                                                                        <a href="MasterProjectWorkMIS_7.aspx?ProjectWork_Id=<%# Eval("ProjectWork_Id") %>&Id=<%# Eval("ProjectWork_Project_Id") %>">Go To Step 7: Variation Details</a>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Jurisdiction">
                                                        <ItemTemplate>
                                                            <table class="table table-striped table-bordered table-hover no-margin-bottom no-border-top">
                                                                <thead>
                                                                    <tr>
                                                                        <th>Jurisdiction</th>
                                                                        <th>Name</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td><%=Session["Default_Zone"].ToString() %></td>
                                                                        <td><%# Eval("Zone_Name") %></td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td><%=Session["Default_Circle"].ToString() %></td>
                                                                        <td><%# Eval("Circle_Name") %></td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td><%=Session["Default_Division"].ToString() %></td>
                                                                        <td><%# Eval("Division_Name") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>District</td>
                                                                        <td><%# Eval("Jurisdiction_Name_Eng") %></td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Scheme">
                                                        <ItemTemplate>
                                                            <table class="table table-striped table-bordered table-hover no-margin-bottom no-border-top">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>Scheme</td>
                                                                        <td><%# Eval("Project_Name") %></td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td>Project Code</td>
                                                                        <td><%# Eval("ProjectWork_ProjectCode") %></td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td>Project Type</td>
                                                                        <td><%# Eval("ProjectType_Name") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Program</td>
                                                                        <td><%# Eval("Program_Name") %></td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ProjectWork_Name" HeaderText="Work" />
                                                    <asp:TemplateField HeaderText="Date & GO">
                                                        <ItemTemplate>
                                                            <table class="table table-striped table-bordered table-hover no-margin-bottom no-border-top">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>Start Date</td>
                                                                        <td><%# Eval("Start_Date") %></td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td>End Date</td>
                                                                        <td><%# Eval("End_Date") %></td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td>GO Date</td>
                                                                        <td><%# Eval("GO_Date") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>GO No</td>
                                                                        <td><%# Eval("ProjectWork_GO_No") %></td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Funding Pattern">
                                                        <ItemTemplate>
                                                            <table class="table table-striped table-bordered table-hover no-margin-bottom no-border-top">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>Central Share</td>
                                                                        <td><%# Eval("Central_Share") %></td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td>State Share</td>
                                                                        <td><%# Eval("State_Share") %></td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td>ULB Share</td>
                                                                        <td><%# Eval("ULB_Share") %></td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td>Total Share</td>
                                                                        <td><%# Eval("Total_Share") %></td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Cost">
                                                        <ItemTemplate>
                                                            <table class="table table-striped table-bordered table-hover no-margin-bottom no-border-top">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>Work Cost</td>
                                                                        <td><%# Eval("Work_Cost") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Centage</td>
                                                                        <td><%# Eval("Centage") %></td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td>Total Cost Inc. Centage</td>
                                                                        <td><%# Eval("Total_Cost_Including_Centage") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Tender Cost (With GST)</td>
                                                                        <td><%# Eval("Tender_Cost") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Tender Cost (Without GST)</td>
                                                                        <td><%# Eval("Tender_Cost_WithOut_GST") %></td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="1st Installment">
                                                        <ItemTemplate>
                                                            <table class="table table-striped table-bordered table-hover no-margin-bottom no-border-top">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>Central Share</td>
                                                                        <td><%# Eval("Central_Share_1st_Instalment") %></td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td>State Share</td>
                                                                        <td><%# Eval("State_Share_1st_Instalment") %></td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td>ULB Share</td>
                                                                        <td><%# Eval("ULB_Share_1st_Instalment") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Centage</td>
                                                                        <td><%# Eval("Centage_1st_Instalment") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Date</td>
                                                                        <td><%# Eval("Date_1st_Instalment") %></td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="2nd Installment">
                                                        <ItemTemplate>
                                                            <table class="table table-striped table-bordered table-hover no-margin-bottom no-border-top">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>Central Share</td>
                                                                        <td><%# Eval("Central_Share_2nd_Instalment") %></td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td>State Share</td>
                                                                        <td><%# Eval("State_Share_2nd_Instalment") %></td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td>ULB Share</td>
                                                                        <td><%# Eval("ULB_Share_2nd_Instalment") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Centage</td>
                                                                        <td><%# Eval("Centage_2nd_Instalment") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Date</td>
                                                                        <td><%# Eval("Date_2nd_Instalment") %></td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="3rd Installment">
                                                        <ItemTemplate>
                                                            <table class="table table-striped table-bordered table-hover no-margin-bottom no-border-top">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>Central Share</td>
                                                                        <td><%# Eval("Central_Share_3rd_Instalment") %></td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td>State Share</td>
                                                                        <td><%# Eval("State_Share_3rd_Instalment") %></td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td>ULB Share</td>
                                                                        <td><%# Eval("ULB_Share_3rd_Instalment") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Centage</td>
                                                                        <td><%# Eval("Centage_3rd_Instalment") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Date</td>
                                                                        <td><%# Eval("Date_3rd_Instalment") %></td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Total Installments">
                                                        <ItemTemplate>
                                                            <table class="table table-striped table-bordered table-hover no-margin-bottom no-border-top">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>Central Share</td>
                                                                        <td><%# Eval("Total_Funds_released_against_Central_Share") %></td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td>State Share</td>
                                                                        <td><%# Eval("Total_Funds_released_against_State_Share") %></td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td>ULB Share</td>
                                                                        <td><%# Eval("Total_Funds_released_against_ULB_Share") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Centage</td>
                                                                        <td><%# Eval("Total_Funds_released_against_Centage") %></td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Invoices">
                                                        <ItemTemplate>
                                                            <table class="table table-striped table-bordered table-hover no-margin-bottom no-border-top">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>Before ePayment</td>
                                                                        <td><%# Eval("Invoice_Before_ePayment") %></td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td>After ePayment</td>
                                                                        <td><%# Eval("Total_Invoice_Value_after_ePayment_Till_Date2") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Total Expenditure Value</td>
                                                                        <td><%# Eval("Total_Expenditure") %></td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Invoices (Current Month)">
                                                        <ItemTemplate>
                                                            <table class="table table-striped table-bordered table-hover no-margin-bottom no-border-top">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>Total Invoive</td>
                                                                        <td><%# Eval("Total_Invoice_Value_Current_Month") %></td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td>Total EMB</td>
                                                                        <td><%# Eval("EMB_Current_Month") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Other Department</td>
                                                                        <td><%# Eval("Other_Dept_Current_Month") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Deduction Release</td>
                                                                        <td><%# Eval("Deduction_Release_Current_Month") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Moblization Advance</td>
                                                                        <td><%# Eval("Moblization_Adv_Current_Month") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Total Achivment</td>
                                                                        <td><%# Eval("Total_Achivment_In_Current_Month") %></td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Progress (%)">
                                                        <ItemTemplate>
                                                            <table class="table table-striped table-bordered table-hover no-margin-bottom no-border-top">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>Physical</td>
                                                                        <td><%# Eval("Physical_Progress") %></td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td>Financial</td>
                                                                        <td><%# Eval("Financial_Progress") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Last Updation On</td>
                                                                        <td><%# Eval("Last_Date_Of_Updation_of_Step_3") %></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Updated By</td>
                                                                        <td><%# Eval("Updated_By") %></td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Issues 1 (If Any)">
                                                        <ItemTemplate>
                                                            <table class="table table-striped table-bordered table-hover no-margin-bottom no-border-top">
                                                                <tbody>
                                                                    <tr>
                                                                        <td><%# Eval("Issue") %></td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Physical Component Details">
                                                        <ItemTemplate>
                                                            <asp:GridView ID="grdPhysicalComponent" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover"
                                                                EmptyDataText="No Records Found" OnPreRender="grdPhysicalComponent_PreRender">
                                                                <Columns>
                                                                    <asp:BoundField HeaderText="Component" DataField="Component_Unit" />
                                                                    <asp:BoundField HeaderText="Proposed" DataField="Proposed" />
                                                                    <asp:BoundField HeaderText="Physical Progress" DataField="PhysicalProgress" />
                                                                    <asp:BoundField HeaderText="Functional" DataField="Functional" />
                                                                    <asp:BoundField HeaderText="Non Functional" DataField="NonFunctional" />
                                                                    <asp:BoundField HeaderText="Remarks" DataField="Remarks" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnDownload" />
                    <asp:PostBackTrigger ControlID="lnkOnePager" />
                    <asp:PostBackTrigger ControlID="btnMPR" />
                </Triggers>
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


    <!-- DataTable specific plugin scripts -->
    <script src="assets/js/jquery-2.1.4.min.js"></script>
    <script src="assets/js/jquery.dataTables.min.js"></script>
    <script src="assets/js/jquery.dataTables.bootstrap.min.js"></script>
    <script src="assets/js/dataTables.buttons.min.js"></script>
    <script src="assets/js/buttons.flash.min.js"></script>
    <script src="assets/js/buttons.html5.min.js"></script>
    <script src="assets/js/buttons.print.min.js"></script>
    <script src="assets/js/buttons.colVis.min.js"></script>
    <script src="assets/js/dataTables.select.min.js"></script>
    <script src="assets/js/ace-elements.min.js"></script>
    <script src="assets/js/ace.min.js"></script>
    <script src="assets/js/dataTables.fixedHeader.min.js"></script>
    <script src="assets/js/jquery.mark.min.js"></script>
    <script src="assets/js/datatables.mark.js"></script>
    <%--<script src="assets/js/dataTables.colReorder.min.js"></script>--%>

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            jQuery(function ($) {
                var DataTableLength = $('#ctl00_ContentPlaceHolder1_grdPost').length;
                if (DataTableLength > 0) {
                    var outerHTML = $('#ctl00_ContentPlaceHolder1_grdPost')[0].outerText;
                    if (outerHTML.trim() !== "No Records Found") {
                        //initiate dataTables plugin
                        var myTable =
                            $('#ctl00_ContentPlaceHolder1_grdPost')
                                //.wrap("<div class='dataTables_borderWrap' />")   //if you are applying horizontal scrolling (sScrollX)
                                .DataTable({
                                    mark: true,
                                    colReorder: false,
                                    fixedHeader: {
                                        header: false,
                                        footer: false
                                    },
                                    bAutoWidth: false,
                                    "aoColumns": [
                                        null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null
                                    ],
                                    "aaSorting": [],
                                    //"bProcessing": true,
                                    //"bServerSide": true,
                                    //"sAjaxSource": "http://127.0.0.1/table.php"	,

                                    //,
                                    //"sScrollY": "200px",
                                    //"bPaginate": false,
                                    //"sScrollX": "100%",
                                    //"sScrollXInner": "120%",
                                    //"bScrollCollapse": true,
                                    //Note: if you are applying horizontal scrolling (sScrollX) on a ".table-bordered"
                                    //you may want to wrap the table inside a "div.dataTables_borderWrap" element

                                    "iDisplayLength": 100,
                                    select: {
                                        style: 'multi'
                                    }
                                });
                        $.fn.dataTable.Buttons.defaults.dom.container.className = 'dt-buttons btn-overlap btn-group btn-overlap';
                        new $.fn.dataTable.Buttons(myTable, {
                            buttons: [
                                {
                                    "extend": "colvis",
                                    "text": "<i class='fa fa-search bigger-110 blue'></i> <span class='hidden'>Show/hide columns</span>",
                                    "className": "btn btn-white btn-primary btn-bold",
                                    columns: ':not(:first):not(:last)'
                                },
                                {
                                    "extend": "copy",
                                    "text": "<i class='fa fa-copy bigger-110 pink'></i> <span class='hidden'>Copy to clipboard</span>",
                                    "className": "btn btn-white btn-primary btn-bold"
                                },
                                {
                                    "extend": "csv",
                                    "text": "<i class='fa fa-database bigger-110 orange'></i> <span class='hidden'>Export to CSV</span>",
                                    "className": "btn btn-white btn-primary btn-bold"
                                },
                                {
                                    "extend": "excel",
                                    "text": "<i class='fa fa-file-excel-o bigger-110 green'></i> <span class='hidden'>Export to Excel</span>",
                                    "className": "btn btn-white btn-primary btn-bold"
                                },
                                {
                                    "extend": "pdf",
                                    "text": "<i class='fa fa-file-pdf-o bigger-110 red'></i> <span class='hidden'>Export to PDF</span>",
                                    "className": "btn btn-white btn-primary btn-bold"
                                },
                                {
                                    "extend": "print",
                                    "text": "<i class='fa fa-print bigger-110 grey'></i> <span class='hidden'>Print</span>",
                                    "className": "btn btn-white btn-primary btn-bold",
                                    autoPrint: true,
                                    message: 'This print was produced using the Print button for DataTables',
                                    exportOptions: {
                                        columns: ':visible'
                                    }
                                }
                            ]
                        });
                        myTable.buttons().container().appendTo($('.tableTools-container'));

                        //style the message box
                        var defaultCopyAction = myTable.button(1).action();
                        myTable.button(1).action(function (e, dt, button, config) {
                            defaultCopyAction(e, dt, button, config);
                            $('.dt-button-info').addClass('gritter-item-wrapper gritter-info gritter-center white');
                        });
                        var defaultColvisAction = myTable.button(0).action();
                        myTable.button(0).action(function (e, dt, button, config) {

                            defaultColvisAction(e, dt, button, config);
                            if ($('.dt-button-collection > .dropdown-menu').length == 0) {
                                $('.dt-button-collection')
                                    .wrapInner('<ul class="dropdown-menu dropdown-light dropdown-caret dropdown-caret" />')
                                    .find('a').attr('href', '#').wrap("<li />")
                            }
                            $('.dt-button-collection').appendTo('.tableTools-container .dt-buttons')
                        });
                        ////
                        setTimeout(function () {
                            $($('.tableTools-container')).find('a.dt-button').each(function () {
                                var div = $(this).find(' > div').first();
                                if (div.length == 1) div.tooltip({ container: 'body', title: div.parent().text() });
                                else $(this).tooltip({ container: 'body', title: $(this).text() });
                            });
                        }, 500);

                        $(document).on('click', '#ctl00_ContentPlaceHolder1_grdPost .dropdown-toggle', function (e) {
                            e.stopImmediatePropagation();
                            e.stopPropagation();
                            //e.preventDefault();
                        });
                        //And for the first simple table, which doesn't have TableTools or dataTables
                        //select/deselect all rows according to table header checkbox
                        var active_class = 'active';
                        /********************************/
                        //add tooltip for small view action buttons in dropdown menu
                        $('[data-rel="tooltip"]').tooltip({ placement: tooltip_placement });

                        //tooltip placement on right or left
                        function tooltip_placement(context, source) {
                            var $source = $(source);
                            var $parent = $source.closest('table')
                            var off1 = $parent.offset();
                            var w1 = $parent.width();

                            var off2 = $source.offset();
                            //var w2 = $source.width();

                            if (parseInt(off2.left) < parseInt(off1.left) + parseInt(w1 / 2)) return 'right';
                            return 'left';
                        }
                        /***************/
                        $('.show-details-btn').on('click', function (e) {
                            e.preventDefault();
                            $(this).closest('tr').next().toggleClass('open');
                            $(this).find(ace.vars['.icon']).toggleClass('fa-angle-double-down').toggleClass('fa-angle-double-up');
                        });
                    }
                }
            })
        });

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

        function openBasicDetails(obj) {
            var project_id = obj.attributes.project_id.nodeValue;
            var projectwork_id = obj.attributes.projectwork_id.nodeValue;
            if (project_id == "1014") {
                obj.href = "MasterProjectWorkMIS_1_Green.aspx?ProjectWork_Id=" + projectwork_id;
            }
            else if (project_id == "1015") {
                obj.href = "MasterProjectWorkMIS_1_DW.aspx?ProjectWork_Id=" + projectwork_id;
            }
            else {
                obj.href = "MasterProjectWorkMIS_1.aspx?ProjectWork_Id=" + projectwork_id;
            }
        }

        function openGODetails(obj) {
            var project_id = obj.attributes.project_id.nodeValue;
            var projectwork_id = obj.attributes.projectwork_id.nodeValue;
            var district_id = obj.attributes.district_id.nodeValue;
            if (project_id == "12") {
                obj.href = "MasterProjectWorkMIS_2_State.aspx?ProjectWork_Id=" + projectwork_id + "&District_Id=" + district_id + "&Id=" + project_id;
            }
            else if (project_id == "3") {
                obj.href = "MasterProjectWorkMIS_2_CNDS.aspx?ProjectWork_Id=" + projectwork_id + "&District_Id=" + district_id + "&Id=" + project_id;
            }
            else if (project_id == "1015") {
                obj.href = "MasterProjectWorkMIS_2_DW.aspx?ProjectWork_Id=" + projectwork_id + "&District_Id=" + district_id + "&Id=" + project_id;
            }
            else {
                obj.href = "MasterProjectWorkMIS_2.aspx?ProjectWork_Id=" + projectwork_id + "&District_Id=" + district_id + "&Id=" + project_id;
            }
        }
    </script>
</asp:Content>
