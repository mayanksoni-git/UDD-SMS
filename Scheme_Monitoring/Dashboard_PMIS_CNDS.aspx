<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="Dashboard_PMIS_CNDS.aspx.cs" Inherits="Dashboard_PMIS_CNDS" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="main-content">
        <div class="main-content-inner">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="btnShowPopup"
                        CancelControlID="btnclose" BackgroundCssClass="modalBackground1">
                    </cc1:ModalPopupExtender>
                    <asp:Button ID="btnShowPopup" Text="Show" runat="server" Style="display: none;"></asp:Button>
                    <div class="page-content">
                        <!-- /.ace-settings-container -->
                        <div class="page-header">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <h1>PMIS Dashboard							
                                       
                                        <small>
                                            <i class="ace-icon fa fa-angle-double-right"></i>
                                            Overview &amp; Stats
                                        </small>
                                        <div style="float: right">
                                            <span class="label label-warning arrowed arrowed-right">ALL FIGURES IN LAKHS ONLY</span>
                                        </div>
                                    </h1>
                                </div>
                                <div class="col-md-3" runat="server" visible="false" id="divShowDeptWise">
                                    <asp:LinkButton ID="lnkDepartmentWise" runat="server" OnClick="lnkDepartmentWise_Click" Text="Show Department Wise" Font-Bold="true"></asp:LinkButton>
                                </div>
                                <div class="col-md-3 blink">
                                    <div class="blink" style="margin-right: 200px;">
                                        <asp:ImageButton ID="btnMIS" runat="server" OnClick="btnMIS_Click" ImageUrl="~/assets/images/Update_PMIS.png" Height="50px"></asp:ImageButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- /.page-header -->

                        <div id="divOverAllStatus" runat="server">
                            <div class="row">
                                <div class="col-md-12">
                                    <div id="Div1" runat="server" class="clearfix">
                                        <div class="pull-right grdOverAllStatustableTools-container">
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <asp:GridView ID="grdOverAllStatus" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" EmptyDataText="No Records Found" OnPreRender="grdOverAllStatus_PreRender" OnRowDataBound="grdOverAllStatus_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Data_Type" HeaderText="Particulars">
                                                    <ItemStyle Font-Bold="true" Font-Size="Medium" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Total Projects">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkTotalProjects" runat="server" OnClick="lnkTotalProjects_Click" Font-Bold="true" Text='<%# Eval("Total") %>' Font-Size="Medium"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Within UP">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkWithInUP" runat="server" OnClick="lnkWithInUP_Click" Font-Bold="true" Text='<%# Eval("WithIn_UP") %>' Font-Size="Medium"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Outside UP">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkOutsideUP" runat="server" OnClick="lnkOutsideUP_Click" Font-Bold="true" Text='<%# Eval("OutSide_UP") %>' Font-Size="Medium"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Completed Projects">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkCompleted" runat="server" OnClick="lnkCompleted_Click" Font-Bold="true" Text='<%# Eval("Completed") %>' Font-Size="Medium"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ongoing Projects">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkOngoing" runat="server" OnClick="lnkOngoing_Click" Font-Bold="true" Text='<%# Eval("OnGoing") %>' Font-Size="Medium"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <div id="chartContainerOverAll" style="height: 500px; width: 100%; margin: 0px auto;"></div>
                                </div>
                            </div>
                        </div>



                        <div runat="server" id="divNodalDeptWise" visible="false">
                            <div class="row">
                                <div class="col-md-12">
                                    <div id="Div2" runat="server" class="clearfix">
                                        <div class="pull-right grdNodalDepttableTools-container">
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <asp:GridView ID="grdNodalDept" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" EmptyDataText="No Records Found" OnPreRender="grdNodalDept_PreRender" ShowFooter="true">
                                            <Columns>
                                                <asp:BoundField DataField="ProjectWork_NodalDepartment_Id" HeaderText="ProjectWork_NodalDepartment_Id">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="S No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nodal Department">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblNodalDept" runat="server" OnClick="lblNodalDept_Click" Font-Bold="true" Text='<%# Eval("Person_Name") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Total Projects" HeaderStyle-BackColor="Orange" HeaderStyle-ForeColor="White">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblTotalProjects" runat="server" OnClick="lblTotalProjects_Click" Font-Bold="true" Text='<%# Eval("Total_Projects") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lblTotalProjectsF" runat="server" OnClick="lblTotalProjectsF_Click" Font-Bold="true" ForeColor="White"></asp:LinkButton>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sanctioned Cost" HeaderStyle-BackColor="Orange" HeaderStyle-ForeColor="White">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblTotalSanctionedCost" runat="server" OnClick="lblTotalProjects_Click" Font-Bold="true" Text='<%# Eval("Total_Sanction") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lblTotalSanctionedCostF" runat="server" OnClick="lblTotalProjectsF_Click" Font-Bold="true" ForeColor="White"></asp:LinkButton>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Released Amount" HeaderStyle-BackColor="Orange" HeaderStyle-ForeColor="White">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblTotalReleasedAmount" runat="server" OnClick="lblTotalProjects_Click" Font-Bold="true" Text='<%# Eval("Total_Release") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lblTotalReleasedAmountF" runat="server" OnClick="lblTotalProjectsF_Click" Font-Bold="true" ForeColor="White"></asp:LinkButton>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Expenditure" HeaderStyle-BackColor="Orange" HeaderStyle-ForeColor="White">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblTotalExpenditure" runat="server" OnClick="lblTotalProjects_Click" Font-Bold="true" Text='<%# Eval("Total_Expenditure") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lblTotalExpenditureF" runat="server" OnClick="lblTotalProjectsF_Click" Font-Bold="true" ForeColor="White"></asp:LinkButton>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Completed Projects" HeaderStyle-BackColor="LightGreen" HeaderStyle-ForeColor="White">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblCompleted" runat="server" OnClick="lblCompleted_Click" Font-Bold="true" Text='<%# Eval("Projects_Completed") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lblCompletedF" runat="server" OnClick="lblCompletedF_Click" Font-Bold="true" ForeColor="White"></asp:LinkButton>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Completed Sanctioned Cost" HeaderStyle-BackColor="LightGreen" HeaderStyle-ForeColor="White">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblCompletedSanctionedCost" runat="server" OnClick="lblCompleted_Click" Font-Bold="true" Text='<%# Eval("Completed_Sanction") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lblCompletedSanctionedCostF" runat="server" OnClick="lblCompletedF_Click" Font-Bold="true" ForeColor="White"></asp:LinkButton>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Completed Released Amount" HeaderStyle-BackColor="LightGreen" HeaderStyle-ForeColor="White">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblCompletedReleasedAmount" runat="server" OnClick="lblCompleted_Click" Font-Bold="true" Text='<%# Eval("Completed_Release") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lblCompletedReleasedAmountF" runat="server" OnClick="lblCompletedF_Click" Font-Bold="true" ForeColor="White"></asp:LinkButton>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Completed Expenditure" HeaderStyle-BackColor="LightGreen" HeaderStyle-ForeColor="White">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblCompletedExpenditure" runat="server" OnClick="lblCompleted_Click" Font-Bold="true" Text='<%# Eval("Completed_Expenditure") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lblCompletedExpenditureF" runat="server" OnClick="lblCompletedF_Click" Font-Bold="true" ForeColor="White"></asp:LinkButton>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Ongoing Projects" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblOngoing" runat="server" OnClick="lblOngoing_Click" Font-Bold="true" Text='<%# Eval("Projects_onGoing") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lblOngoingF" runat="server" OnClick="lblOngoingF_Click" Font-Bold="true" ForeColor="White"></asp:LinkButton>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Ongoing Sanctioned Cost" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblOngoingSanctionedCost" runat="server" OnClick="lblOngoing_Click" Font-Bold="true" Text='<%# Eval("OnGoing_Sanction") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lblOngoingSanctionedCostF" runat="server" OnClick="lblOngoingF_Click" Font-Bold="true" ForeColor="White"></asp:LinkButton>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ongoing Released Amount" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblOngoingReleasedAmount" runat="server" OnClick="lblOngoing_Click" Font-Bold="true" Text='<%# Eval("Ongoing_Release") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lblOngoingReleasedAmountF" runat="server" OnClick="lblOngoingF_Click" Font-Bold="true" ForeColor="White"></asp:LinkButton>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ongoing Expenditure" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblOngoingExpenditure" runat="server" OnClick="lblOngoing_Click" Font-Bold="true" Text='<%# Eval("Ongoing_Expenditure") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lblOngoingExpenditureF" runat="server" OnClick="lblOngoingF_Click" Font-Bold="true" ForeColor="White"></asp:LinkButton>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="Black" ForeColor="White" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>




                        <div class="space-6"></div>
                        <h3 class="header smaller red">Search / Filter Criteria
                        </h3>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:CheckBoxList runat="server" ID="chkJurisdictionFilter">
                                                    <asp:ListItem Text="Within State" Value="1" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Outside State" Value="0" Selected="True"></asp:ListItem>
                                                    <%--<asp:ListItem Text="Urban Development Department, GoUP" Value="3"></asp:ListItem>--%>
                                                </asp:CheckBoxList>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:RadioButtonList runat="server" ID="rbtViewMode" AutoPostBack="true" OnSelectedIndexChanged="rbtViewMode_SelectedIndexChanged">
                                                    <asp:ListItem Text="Nodal Department Wise" Value="N"></asp:ListItem>
                                                    <asp:ListItem Text="Administrative GM Wise" Value="A" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label class="control-label no-padding-right">District* </label>
                                    <asp:DropDownList ID="ddlDistrict" runat="server" class="chosen-select form-control" data-placeholder="Choose a District..."></asp:DropDownList>
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

                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:CheckBox ID="chkSelectAllNodal" runat="server" Text="Nodal Department (click To Select All)" AutoPostBack="true" OnCheckedChanged="chkSelectAllNodal_CheckedChanged"></asp:CheckBox>
                                    <asp:ListBox SelectionMode="Multiple" ID="ddlNodalDept" runat="server" class="chosen-select form-control" data-placeholder="Choose a Nodal Department..." AutoPostBack="true" OnSelectedIndexChanged="ddlNodalDept_SelectedIndexChanged"></asp:ListBox>
                                </div>
                            </div>
                            <div class="col-md-3" runat="server" id="divScheme" visible="false">
                                <div class="form-group">
                                    <label class="control-label no-padding-right">
                                        Scheme
                                    </label>
                                    <asp:DropDownList ID="ddlScheme" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:CheckBox ID="chkSelectAllNodalScheme" runat="server" Text="Scheme (click To Select All)" AutoPostBack="true" OnCheckedChanged="chkSelectAllNodalScheme_CheckedChanged"></asp:CheckBox>
                                    <asp:ListBox SelectionMode="Multiple" ID="ddlNodalDeptScheme" runat="server" class="chosen-select form-control" data-placeholder="Choose a Scheme..."></asp:ListBox>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label class="control-label no-padding-right">
                                        Sanction / Financial Year
                                                    <div style="float: right; margin-left: 30px">
                                                        <asp:CheckBox runat="server" ID="chkFY_Wise" OnCheckedChanged="chkFY_Wise_CheckedChanged" AutoPostBack="true" ToolTip="Select To Switch Between Year Wise and Fnancial Year Wise Filter" />
                                                    </div>
                                    </label>
                                    <asp:ListBox SelectionMode="Multiple" ID="ddlSanctionYear" runat="server" class="chosen-select form-control" data-placeholder="Choose Project Sanction Year..."></asp:ListBox>
                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <br />
                                    <asp:Button ID="btnSearch" Text="Search" runat="server" CssClass="btn btn-info" OnClick="btnSearch_Click"></asp:Button>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-3 pricing-span-header">
                                <div class="widget-box transparent">
                                    <div class="widget-header">
                                        <h5 class="widget-title bigger lighter">Desctiption
                    <br />
                                        </h5>
                                    </div>

                                    <div class="widget-body" style="margin-top: 18px;">
                                        <div class="widget-main no-padding">
                                            <ul class="list-unstyled list-striped pricing-table-header">
                                                <li style="font-weight: bold; font-size: medium;">
                                                    <asp:LinkButton ID="lnkTotalYear" runat="server" OnClick="lnkTotalYear_Click" Font-Bold="true" Text="Total Project" Font-Size="Medium"></asp:LinkButton></li>
                                                <li style="font-weight: bold; font-size: medium;">
                                                    <asp:LinkButton ID="lnkOngoingYear" runat="server" OnClick="lnkOngoingYear_Click" Font-Bold="true" Text="Ongoing Project" Font-Size="Medium"></asp:LinkButton></li>
                                                <li style="font-weight: bold; font-size: medium;">
                                                    <asp:LinkButton ID="lnkcompletedYear" runat="server" OnClick="lnkcompletedYear_Click" Font-Bold="true" Text="Completed Project" Font-Size="Medium"></asp:LinkButton></li>
                                                <li style="font-weight: bold; font-size: medium;">
                                                    <asp:LinkButton ID="lnkNewWorks" runat="server" OnClick="lnkNewWorks_Click" Font-Bold="true" Text="New Works" Font-Size="Medium"></asp:LinkButton></li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-xs-9 pricing-span-body">
                                <div class="pricing-span">
                                    <div class="widget-box pricing-box-small widget-color-red3">
                                        <div class="widget-header">
                                            <center>
                                                <h5 class="widget-title bigger lighter">Project
                        <br />
                                                    Count</h5>
                                            </center>
                                        </div>

                                        <div class="widget-body">
                                            <div class="widget-main no-padding">
                                                <ul class="list-unstyled list-striped pricing-table">
                                                    <li>
                                                        <asp:LinkButton ID="lnkTotal_Projects" runat="server" OnClick="lnkTotal_Projects_Click" Font-Bold="true" Text="0" Font-Size="Medium"></asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="lnkTotal_Projects_Ongoing" runat="server" OnClick="lnkTotal_Projects_Ongoing_Click" Font-Bold="true" Text="0" Font-Size="Medium"></asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="lnkTotal_Projects_Completed" runat="server" OnClick="lnkTotal_Projects_Completed_Click" Font-Bold="true" Text="0" Font-Size="Medium"></asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="lnkTotal_Projects_New" runat="server" OnClick="lnkTotal_Projects_New_Click" Font-Bold="true" Text="0" Font-Size="Medium"></asp:LinkButton>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="pricing-span">
                                    <div class="widget-box pricing-box-small widget-color-orange">
                                        <div class="widget-header">
                                            <center>
                                                <h5 class="widget-title bigger lighter">Sanctioned Cost
                            <br />
                                                    (In Lakhs)</h5>
                                            </center>
                                        </div>

                                        <div class="widget-body">
                                            <div class="widget-main no-padding">
                                                <ul class="list-unstyled list-striped pricing-table">
                                                    <li>
                                                        <asp:LinkButton ID="lnk_Total_Sanction_Cost" runat="server" OnClick="lnkTotal_Projects_Click" Font-Bold="true" Text="0" Font-Size="Medium"></asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="lnk_Ongoing_Sanction_Cost" runat="server" OnClick="lnk_Ongoing_Sanction_Cost_Click" Font-Bold="true" Text="0" Font-Size="Medium"></asp:LinkButton></li>
                                                    <li>
                                                        <asp:LinkButton ID="lnk_Completed_Sanction_Cost" runat="server" OnClick="lnk_Completed_Sanction_Cost_Click" Font-Bold="true" Text="0" Font-Size="Medium"></asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="lnk_New_Sanction_Cost" runat="server" OnClick="lnk_New_Sanction_Cost_Click" Font-Bold="true" Text="0" Font-Size="Medium"></asp:LinkButton>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="pricing-span">
                                    <div class="widget-box pricing-box-small widget-color-green">
                                        <div class="widget-header">
                                            <center>
                                                <h5 class="widget-title bigger lighter">Total Released
                            <br />
                                                    (In Lakhs)</h5>
                                            </center>
                                        </div>

                                        <div class="widget-body">
                                            <div class="widget-main no-padding">
                                                <ul class="list-unstyled list-striped pricing-table">
                                                    <li>
                                                        <asp:LinkButton ID="lnk_Total_Release" runat="server" OnClick="lnk_Total_Release_Click" Font-Bold="true" Text="0" Font-Size="Medium"></asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="lnk_Ongoing_Release" runat="server" OnClick="lnk_Ongoing_Release_Click" Font-Bold="true" Text="0" Font-Size="Medium"></asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="lnk_Completed_Release" runat="server" OnClick="lnk_Completed_Release_Click" Font-Bold="true" Text="0" Font-Size="Medium"></asp:LinkButton>
                                                    </li>
                                                    <li>Not Applicable
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="pricing-span">
                                    <div class="widget-box pricing-box-small widget-color-grey">
                                        <div class="widget-header">
                                            <center>
                                                <h5 class="widget-title bigger lighter">Total Expenditure 
                            <br />
                                                    (In Lakhs)</h5>
                                            </center>
                                        </div>

                                        <div class="widget-body">
                                            <div class="widget-main no-padding">
                                                <ul class="list-unstyled list-striped pricing-table">
                                                    <li>
                                                        <asp:LinkButton ID="lnk_Total_Expenditure" runat="server" OnClick="lnk_Total_Expenditure_Click" Font-Bold="true" Text="0" Font-Size="Medium"></asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="lnk_Ongoing_Expenditure" runat="server" OnClick="lnk_Ongoing_Expenditure_Click" Font-Bold="true" Text="0" Font-Size="Medium"></asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="lnk_Completed_Expenditure" runat="server" OnClick="lnk_Completed_Expenditure_Click" Font-Bold="true" Text="0" Font-Size="Medium"></asp:LinkButton>
                                                    </li>
                                                    <li>Not Applicable
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="pricing-span">
                                    <div class="widget-box pricing-box-small widget-color-dark">
                                        <div class="widget-header">
                                            <center>
                                                <h5 class="widget-title bigger lighter">Remaining Amount To Be Released
                            (In Lakhs)</h5>
                                            </center>
                                        </div>

                                        <div class="widget-body">
                                            <div class="widget-main no-padding">
                                                <ul class="list-unstyled list-striped pricing-table">
                                                    <li>
                                                        <asp:LinkButton ID="lnk_Total_Remaining" runat="server" OnClick="lnk_Total_Remaining_Click" Font-Bold="true" Text="0" Font-Size="Medium"></asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="lnk_Ongoing_Remaining" runat="server" OnClick="lnk_Ongoing_Remaining_Click" Font-Bold="true" Text="0" Font-Size="Medium"></asp:LinkButton>
                                                    </li>
                                                    <li>
                                                        <asp:LinkButton ID="lnk_Completed_Remaining" runat="server" OnClick="lnk_Completed_Remaining_Click" Font-Bold="true" Text="0" Font-Size="Medium"></asp:LinkButton>
                                                    </li>
                                                    <li>Not Applicable
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div runat="server" id="divYearWiseBreakup" visible="false">
                            <h3 class="header smaller red">Year Wise Projects Data Analysis
                            </h3>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView ID="grdYearWiseDataAnalysis" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" EmptyDataText="No Records Found" OnPreRender="grdYearWiseDataAnalysis_PreRender" OnRowDataBound="grdYearWiseDataAnalysis_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S No.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Year">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkYear" runat="server" OnClick="lnkYear_Click" Font-Bold="true" Text='<%# Eval("Year") %>' Font-Size="Medium"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Total_Count" HeaderText="Total Projects" />
                                            <asp:BoundField DataField="Total_Sanction" HeaderText="Total Sanctioned Cost" />
                                            <asp:BoundField DataField="Total_Release" HeaderText="Total Released Amount" />
                                            <asp:BoundField DataField="Total_Expenditure" HeaderText="Total Expenditure Amount" />
                                            <asp:BoundField DataField="Total_Remaining_Amount" HeaderText="Remaining Amount To Be Released" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>

                        <div runat="server" id="divNewProjects" visible="false">
                            <h3 class="header smaller red">New Projects
                            </h3>
                            <div class="row">
                                <table class="table table-striped table-bordered table-hover no-margin-bottom no-border-top">
                                    <thead>
                                        <tr>
                                            <th>Module</th>
                                            <th>Flow</th>
                                            <th>Total No Of Projects</th>
                                            <th>Cost (In Lakhs)</th>
                                            <th>Action</th>
                                        </tr>
                                    </thead>

                                    <tbody>
                                        <tr>
                                            <td style="background-color: #8B9AA3!important; color: white; font-weight: bold;"><span class="col-xs-6">GO</span>
                                            </td>
                                            <td style="font-weight: bold; font-size: medium;"><span class="col-xs-6">Total Works where C&DS is nominated as an Executing Agency  </span>
                                            </td>
                                            <td style="font-weight: bold; font-size: medium;">

                                                <div class="infobox infobox-blue">
                                                    <div class="infobox-icon">
                                                        <i>
                                                            <img src="assets/images/DPR/1.png" width="60px" height="60px" />
                                                        </i>
                                                    </div>
                                                    <div class="infobox-data">
                                                        <span class="infobox-data-number" style="margin-left: 15px;" id="sp_Nodal" runat="server">0</span>

                                                    </div>
                                                </div>

                                            </td>
                                            <td style="font-weight: bold; font-size: medium;">
                                                <div class="infobox infobox-blue">
                                                    <div class="infobox-icon">
                                                        <i>
                                                            <img src="assets/images/rupee.png" width="50px" height="50px" />
                                                        </i>
                                                    </div>
                                                    <div class="infobox-data">
                                                        <span class="infobox-data-number" style="margin-left: 15px;" id="sp_NodalCost" runat="server">0</span>

                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnkNewNodal" runat="server" OnClick="lnkNewNodal_Click" Font-Bold="true" Text="Open" Font-Size="Medium"></asp:LinkButton></td>
                                        </tr>

                                        <tr>
                                            <td style="background-color: #8B9AA3!important; color: white; font-weight: bold;"><span class="col-xs-6">GO </span>
                                            </td>
                                            <td style="font-weight: bold; font-size: medium;"><span class="col-xs-6">Works where C&DS is nominated as an Executing Agency via G.O. </span>
                                            </td>
                                            <td style="font-weight: bold; font-size: medium;">

                                                <div class="infobox infobox-blue">
                                                    <div class="infobox-icon">
                                                        <i>
                                                            <img src="assets/images/DPR/2.jpg" width="60px" height="60px" />
                                                        </i>
                                                    </div>
                                                    <div class="infobox-data">
                                                        <span class="infobox-data-number" style="margin-left: 15px;" id="sp_GO" runat="server">0</span>

                                                    </div>
                                                </div>

                                            </td>
                                            <td style="font-weight: bold; font-size: medium;">
                                                <div class="infobox infobox-blue">
                                                    <div class="infobox-icon">
                                                        <i>
                                                            <img src="assets/images/rupee.png" width="50px" height="50px" />
                                                        </i>
                                                    </div>
                                                    <div class="infobox-data">
                                                        <span class="infobox-data-number" style="margin-left: 15px;" id="sp_GOCost" runat="server">0</span>

                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnkNewGO" runat="server" OnClick="lnkNewGO_Click" Font-Bold="true" Text="Open" Font-Size="Medium"></asp:LinkButton></td>
                                        </tr>

                                        <tr>
                                            <td style="background-color: #8B9AA3!important; color: white; font-weight: bold;"><span class="col-xs-6">GO </span>
                                            </td>
                                            <td style="font-weight: bold; font-size: medium;"><span class="col-xs-6">Works where G.O. is Pending</span>
                                            </td>
                                            <td style="font-weight: bold; font-size: medium;">

                                                <div class="infobox infobox-blue">
                                                    <div class="infobox-icon">
                                                        <i>
                                                            <img src="assets/images/DPR/3.jpg" width="60px" height="60px" />
                                                        </i>
                                                    </div>
                                                    <div class="infobox-data">
                                                        <span class="infobox-data-number" style="margin-left: 15px;" id="sp_GO_Pending" runat="server">0</span>

                                                    </div>
                                                </div>

                                            </td>
                                            <td style="font-weight: bold; font-size: medium;">
                                                <div class="infobox infobox-blue">
                                                    <div class="infobox-icon">
                                                        <i>
                                                            <img src="assets/images/rupee.png" width="50px" height="50px" />
                                                        </i>
                                                    </div>
                                                    <div class="infobox-data">
                                                        <span class="infobox-data-number" style="margin-left: 15px;" id="sp_GO_PendingCost" runat="server">0</span>

                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnkNewGOPending" runat="server" OnClick="lnkNewGOPending_Click" Font-Bold="true" Text="Open" Font-Size="Medium"></asp:LinkButton></td>
                                        </tr>

                                        <tr>
                                            <td style="background-color: #E59729!important; color: white; font-weight: bold;"><span class="col-xs-6">DPR </span>
                                            </td>
                                            <td style="font-weight: bold; font-size: medium;"><span class="col-xs-6">PE/DPR To be Prepared </span>
                                            </td>
                                            <td style="font-weight: bold; font-size: medium;">

                                                <div class="infobox infobox-blue">
                                                    <div class="infobox-icon">
                                                        <i>
                                                            <img src="assets/images/DPR/4.jpg" width="60px" height="60px" />
                                                        </i>
                                                    </div>
                                                    <div class="infobox-data">
                                                        <span class="infobox-data-number" style="margin-left: 15px;" id="sp_DPRPrepared" runat="server">0</span>

                                                    </div>
                                                </div>

                                            </td>
                                            <td style="font-weight: bold; font-size: medium;">
                                                <div class="infobox infobox-blue">
                                                    <div class="infobox-icon">
                                                        <i>
                                                            <img src="assets/images/rupee.png" width="50px" height="50px" />
                                                        </i>
                                                    </div>
                                                    <div class="infobox-data">
                                                        <span class="infobox-data-number" style="margin-left: 15px;" id="sp_DPRPreparedCost" runat="server">0</span>

                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnkNewDPRPrepared" runat="server" OnClick="lnkNewDPRPrepared_Click" Font-Bold="true" Text="Open" Font-Size="Medium"></asp:LinkButton></td>
                                        </tr>

                                        <tr>
                                            <td style="background-color: #E59729!important; color: white; font-weight: bold;"><span class="col-xs-6">DPR </span>
                                            </td>
                                            <td style="font-weight: bold; font-size: medium;"><span class="col-xs-6">PE/DPR Prepared and Sent to Client </span>
                                            </td>
                                            <td style="font-weight: bold; font-size: medium;">

                                                <div class="infobox infobox-blue">
                                                    <div class="infobox-icon">
                                                        <i>
                                                            <img src="assets/images/DPR/5.png" width="60px" height="60px" />
                                                        </i>
                                                    </div>
                                                    <div class="infobox-data">
                                                        <span class="infobox-data-number" style="margin-left: 15px;" id="sp_DPR_Send_Client" runat="server">0</span>

                                                    </div>
                                                </div>

                                            </td>
                                            <td style="font-weight: bold; font-size: medium;">
                                                <div class="infobox infobox-blue">
                                                    <div class="infobox-icon">
                                                        <i>
                                                            <img src="assets/images/rupee.png" width="50px" height="50px" />
                                                        </i>
                                                    </div>
                                                    <div class="infobox-data">
                                                        <span class="infobox-data-number" style="margin-left: 15px;" id="sp_DPR_Send_ClientCost" runat="server">0</span>

                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnkNewDPRSendClient" runat="server" OnClick="lnkNewDPRSendClient_Click" Font-Bold="true" Text="Open" Font-Size="Medium"></asp:LinkButton></td>
                                        </tr>

                                        <tr>
                                            <td style="background-color: #E59729!important; color: white; font-weight: bold;"><span class="col-xs-6">DPR </span>
                                            </td>
                                            <td style="font-weight: bold; font-size: medium;"><span class="col-xs-6">PE/DPR Approved by Client Department</span>
                                            </td>
                                            <td style="font-weight: bold; font-size: medium;">

                                                <div class="infobox infobox-blue">
                                                    <div class="infobox-icon">
                                                        <i>
                                                            <img src="assets/images/DPR/6.png" width="60px" height="60px" />
                                                        </i>
                                                    </div>
                                                    <div class="infobox-data">
                                                        <span class="infobox-data-number" style="margin-left: 15px;" id="sp_DPR_Approved_Client" runat="server">0</span>

                                                    </div>
                                                </div>

                                            </td>
                                            <td style="font-weight: bold; font-size: medium;">
                                                <div class="infobox infobox-blue">
                                                    <div class="infobox-icon">
                                                        <i>
                                                            <img src="assets/images/rupee.png" width="50px" height="50px" />
                                                        </i>
                                                    </div>
                                                    <div class="infobox-data">
                                                        <span class="infobox-data-number" style="margin-left: 15px;" id="sp_DPR_Approved_ClientCost" runat="server">0</span>

                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnkNewDPR_Approved_Client" runat="server" OnClick="lnkNewDPR_Approved_Client_Click" Font-Bold="true" Text="Open" Font-Size="Medium"></asp:LinkButton></td>
                                        </tr>

                                        <tr>
                                            <td style="background-color: #E59729!important; color: white; font-weight: bold;"><span class="col-xs-6">DPR </span>
                                            </td>
                                            <td style="font-weight: bold; font-size: medium;"><span class="col-xs-6">PE/DPR Approved Where Technical Sanction is Done </span>
                                            </td>

                                            <td style="font-weight: bold; font-size: medium;">

                                                <div class="infobox infobox-blue">
                                                    <div class="infobox-icon">
                                                        <i>
                                                            <img src="assets/images/DPR/7.png" width="60px" height="60px" />
                                                        </i>
                                                    </div>
                                                    <div class="infobox-data">
                                                        <span class="infobox-data-number" style="margin-left: 15px;" id="sp_TS_Done" runat="server">0</span>

                                                    </div>
                                                </div>

                                            </td>
                                            <td style="font-weight: bold; font-size: medium;">
                                                <div class="infobox infobox-blue">
                                                    <div class="infobox-icon">
                                                        <i>
                                                            <img src="assets/images/rupee.png" width="50px" height="50px" />
                                                        </i>
                                                    </div>
                                                    <div class="infobox-data">
                                                        <span class="infobox-data-number" style="margin-left: 15px;" id="sp_TS_DoneCost" runat="server">0</span>

                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnkNewTS_Done" runat="server" OnClick="lnkNewTS_Done_Click" Font-Bold="true" Text="Open" Font-Size="Medium"></asp:LinkButton></td>
                                        </tr>

                                        <tr>
                                            <td style="background-color: #E59729!important; color: white; font-weight: bold;"><span class="col-xs-6">DPR </span>
                                            </td>
                                            <td style="font-weight: bold; font-size: medium;"><span class="col-xs-6">PE/DPR Approved Where Technical Sanction is Pending at HQ</span>
                                            </td>

                                            <td style="font-weight: bold; font-size: medium;">

                                                <div class="infobox infobox-blue">
                                                    <div class="infobox-icon">
                                                        <i>
                                                            <img src="assets/images/DPR/8.png" width="60px" height="60px" />
                                                        </i>
                                                    </div>
                                                    <div class="infobox-data">
                                                        <span class="infobox-data-number" style="margin-left: 15px;" id="sp_TS_Pending" runat="server">0</span>

                                                    </div>
                                                </div>

                                            </td>
                                            <td style="font-weight: bold; font-size: medium;">
                                                <div class="infobox infobox-blue">
                                                    <div class="infobox-icon">
                                                        <i>
                                                            <img src="assets/images/rupee.png" width="50px" height="50px" />
                                                        </i>
                                                    </div>
                                                    <div class="infobox-data">
                                                        <span class="infobox-data-number" style="margin-left: 15px;" id="sp_TS_PendingCost" runat="server">0</span>

                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnkNewTS_Pending" runat="server" OnClick="lnkNewTS_Pending_Click" Font-Bold="true" Text="Open" Font-Size="Medium"></asp:LinkButton></td>
                                        </tr>

                                        <tr>
                                            <td style="background-color: #E59729!important; color: white; font-weight: bold;"><span class="col-xs-6">DPR </span>
                                            </td>
                                            <td style="font-weight: bold; font-size: medium;"><span class="col-xs-6">DPR Under Preparation</span>
                                            </td>

                                            <td style="font-weight: bold; font-size: medium;">

                                                <div class="infobox infobox-blue">
                                                    <div class="infobox-icon">
                                                        <i>
                                                            <img src="assets/images/DPR/9.png" width="60px" height="60px" />
                                                        </i>
                                                    </div>
                                                    <div class="infobox-data">
                                                        <span class="infobox-data-number" style="margin-left: 15px;" id="sp_DPR_Under_Prepration" runat="server">0</span>

                                                    </div>
                                                </div>

                                            </td>
                                            <td style="font-weight: bold; font-size: medium;">
                                                <div class="infobox infobox-blue">
                                                    <div class="infobox-icon">
                                                        <i>
                                                            <img src="assets/images/rupee.png" width="50px" height="50px" />
                                                        </i>
                                                    </div>
                                                    <div class="infobox-data">
                                                        <span class="infobox-data-number" style="margin-left: 15px;" id="sp_DPR_Under_PreprationCost" runat="server">0</span>

                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnkNewDPR_Under_Prepration" runat="server" OnClick="lnkNewDPR_Under_Prepration_Click" Font-Bold="true" Text="Open" Font-Size="Medium"></asp:LinkButton></td>
                                        </tr>

                                        <tr>
                                            <td style="background-color: #87B87F!important; color: white; font-weight: bold;"><span class="col-xs-6">Tender </span>
                                            </td>
                                            <td style="font-weight: bold; font-size: medium;"><span class="col-xs-6">NIT Floated</span>
                                            </td>

                                            <td style="font-weight: bold; font-size: medium;">

                                                <div class="infobox infobox-blue">
                                                    <div class="infobox-icon">
                                                        <i>
                                                            <img src="assets/images/DPR/10.jpg" width="60px" height="60px" />
                                                        </i>
                                                    </div>
                                                    <div class="infobox-data">
                                                        <span class="infobox-data-number" style="margin-left: 15px;" id="sp_NIT_Floated" runat="server">0</span>

                                                    </div>
                                                </div>

                                            </td>
                                            <td style="font-weight: bold; font-size: medium;">
                                                <div class="infobox infobox-blue">
                                                    <div class="infobox-icon">
                                                        <i>
                                                            <img src="assets/images/rupee.png" width="50px" height="50px" />
                                                        </i>
                                                    </div>
                                                    <div class="infobox-data">
                                                        <span class="infobox-data-number" style="margin-left: 15px;" id="sp_NIT_FloatedCost" runat="server">0</span>

                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnkNewNIT_Floated" runat="server" OnClick="lnkNewNIT_Floated_Click" Font-Bold="true" Text="Open" Font-Size="Medium"></asp:LinkButton></td>
                                        </tr>

                                        <tr>
                                            <td style="background-color: #87B87F!important; color: white; font-weight: bold;"><span class="col-xs-6">Tender </span>
                                            </td>
                                            <td style="font-weight: bold; font-size: medium;"><span class="col-xs-6">Technical Bid Opened </span>
                                            </td>
                                            <td style="font-weight: bold; font-size: medium;">

                                                <div class="infobox infobox-blue">
                                                    <div class="infobox-icon">
                                                        <i>
                                                            <img src="assets/images/DPR/11.jpg" width="60px" height="60px" />
                                                        </i>
                                                    </div>
                                                    <div class="infobox-data">
                                                        <span class="infobox-data-number" style="margin-left: 15px;" id="sp_Bid_Opened_Technical" runat="server">0</span>

                                                    </div>
                                                </div>

                                            </td>
                                            <td style="font-weight: bold; font-size: medium;">
                                                <div class="infobox infobox-blue">
                                                    <div class="infobox-icon">
                                                        <i>
                                                            <img src="assets/images/rupee.png" width="50px" height="50px" />
                                                        </i>
                                                    </div>
                                                    <div class="infobox-data">
                                                        <span class="infobox-data-number" style="margin-left: 15px;" id="sp_Bid_Opened_TechnicalCost" runat="server">0</span>

                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnkNewBid_Opened_Technical" runat="server" OnClick="lnkNewBid_Opened_Technical_Click" Font-Bold="true" Text="Open" Font-Size="Medium"></asp:LinkButton></td>
                                        </tr>

                                        <tr>
                                            <td style="background-color: #87B87F!important; color: white; font-weight: bold;"><span class="col-xs-6">Tender </span>
                                            </td>
                                            <td style="font-weight: bold; font-size: medium;"><span class="col-xs-6">Technical Bid To Be Opened </span>
                                            </td>

                                            <td style="font-weight: bold; font-size: medium;">

                                                <div class="infobox infobox-blue">
                                                    <div class="infobox-icon">
                                                        <i>
                                                            <img src="assets/images/DPR/12.png" width="60px" height="60px" />
                                                        </i>
                                                    </div>
                                                    <div class="infobox-data">
                                                        <span class="infobox-data-number" style="margin-left: 15px;" id="sp_Bid_To_Be_Opened_Technical" runat="server">0</span>

                                                    </div>
                                                </div>

                                            </td>
                                            <td style="font-weight: bold; font-size: medium;">
                                                <div class="infobox infobox-blue">
                                                    <div class="infobox-icon">
                                                        <i>
                                                            <img src="assets/images/rupee.png" width="50px" height="50px" />
                                                        </i>
                                                    </div>
                                                    <div class="infobox-data">
                                                        <span class="infobox-data-number" style="margin-left: 15px;" id="sp_Bid_To_Be_Opened_TechnicalCost" runat="server">0</span>

                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnkNewBid_To_Be_Opened_Technical" runat="server" OnClick="lnkNewBid_To_Be_Opened_Technical_Click" Font-Bold="true" Text="Open" Font-Size="Medium"></asp:LinkButton></td>
                                        </tr>

                                        <tr>
                                            <td style="background-color: #87B87F!important; color: white; font-weight: bold;"><span class="col-xs-6">Tender </span>
                                            </td>
                                            <td style="font-weight: bold; font-size: medium;"><span class="col-xs-6">Financial Bid Opened </span>
                                            </td>

                                            <td style="font-weight: bold; font-size: medium;">

                                                <div class="infobox infobox-blue">
                                                    <div class="infobox-icon">
                                                        <i>
                                                            <img src="assets/images/DPR/13.jpg" width="60px" height="60px" />
                                                        </i>
                                                    </div>
                                                    <div class="infobox-data">
                                                        <span class="infobox-data-number" style="margin-left: 15px;" id="sp_Bid_Opened_Financial" runat="server">0</span>

                                                    </div>
                                                </div>

                                            </td>
                                            <td style="font-weight: bold; font-size: medium;">
                                                <div class="infobox infobox-blue">
                                                    <div class="infobox-icon">
                                                        <i>
                                                            <img src="assets/images/rupee.png" width="50px" height="50px" />
                                                        </i>
                                                    </div>
                                                    <div class="infobox-data">
                                                        <span class="infobox-data-number" style="margin-left: 15px;" id="sp_Bid_Opened_FinancialCost" runat="server">0</span>

                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnkNewBid_Opened_Financial" runat="server" OnClick="lnkNewBid_Opened_Financial_Click" Font-Bold="true" Text="Open" Font-Size="Medium"></asp:LinkButton></td>
                                        </tr>

                                        <tr>
                                            <td style="background-color: #87B87F!important; color: white; font-weight: bold;"><span class="col-xs-6">Tender </span>
                                            </td>
                                            <td style="font-weight: bold; font-size: medium;"><span class="col-xs-6">Financial Bid To Be Opened </span>
                                            </td>
                                            <td style="font-weight: bold; font-size: medium;">

                                                <div class="infobox infobox-blue">
                                                    <div class="infobox-icon">
                                                        <i>
                                                            <img src="assets/images/DPR/14.jpg" width="60px" height="60px" />
                                                        </i>
                                                    </div>
                                                    <div class="infobox-data">
                                                        <span class="infobox-data-number" style="margin-left: 15px;" id="sp_Bid_To_Be_Opened_Financial" runat="server">0</span>

                                                    </div>
                                                </div>

                                            </td>
                                            <td style="font-weight: bold; font-size: medium;">
                                                <div class="infobox infobox-blue">
                                                    <div class="infobox-icon">
                                                        <i>
                                                            <img src="assets/images/rupee.png" width="50px" height="50px" />
                                                        </i>
                                                    </div>
                                                    <div class="infobox-data">
                                                        <span class="infobox-data-number" style="margin-left: 15px;" id="sp_Bid_To_Be_Opened_FinancialCost" runat="server">0</span>

                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnkNewBid_To_Be_Opened_Financial" runat="server" OnClick="lnkNewBid_To_Be_Opened_Financial_Click" Font-Bold="true" Text="Open" Font-Size="Medium"></asp:LinkButton></td>
                                        </tr>

                                        <tr>
                                            <td style="background-color: #87B87F!important; color: white; font-weight: bold;"><span class="col-xs-6">Tender </span>
                                            </td>
                                            <td style="font-weight: bold; font-size: medium;"><span class="col-xs-6">LoA Issued </span>
                                            </td>
                                            <td style="font-weight: bold; font-size: medium;">

                                                <div class="infobox infobox-blue">
                                                    <div class="infobox-icon">
                                                        <i>
                                                            <img src="assets/images/DPR/15.jpg" width="60px" height="60px" />
                                                        </i>
                                                    </div>
                                                    <div class="infobox-data">
                                                        <span class="infobox-data-number" style="margin-left: 15px;" id="sp_LOA_Issued" runat="server">0</span>

                                                    </div>
                                                </div>

                                            </td>
                                            <td style="font-weight: bold; font-size: medium;">
                                                <div class="infobox infobox-blue">
                                                    <div class="infobox-icon">
                                                        <i>
                                                            <img src="assets/images/rupee.png" width="50px" height="50px" />
                                                        </i>
                                                    </div>
                                                    <div class="infobox-data">
                                                        <span class="infobox-data-number" style="margin-left: 15px;" id="sp_LOA_IssuedCost" runat="server">0</span>

                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnkNewLOA_Issued" runat="server" OnClick="lnkNewLOA_Issued_Click" Font-Bold="true" Text="Open" Font-Size="Medium"></asp:LinkButton></td>
                                        </tr>

                                        <tr>
                                            <td style="background-color: #87B87F!important; color: white; font-weight: bold;"><span class="col-xs-6">Tender </span>
                                            </td>
                                            <td style="font-weight: bold; font-size: medium;"><span class="col-xs-6">Work Started </span>
                                            </td>
                                            <td style="font-weight: bold; font-size: medium;">

                                                <div class="infobox infobox-blue">
                                                    <div class="infobox-icon">
                                                        <i>
                                                            <img src="assets/images/DPR/16.jpg" width="60px" height="60px" />
                                                        </i>
                                                    </div>
                                                    <div class="infobox-data">
                                                        <span class="infobox-data-number" style="margin-left: 15px;" id="sp_Work_Started" runat="server">0</span>

                                                    </div>
                                                </div>

                                            </td>
                                            <td style="font-weight: bold; font-size: medium;">
                                                <div class="infobox infobox-blue">
                                                    <div class="infobox-icon">
                                                        <i>
                                                            <img src="assets/images/rupee.png" width="50px" height="50px" />
                                                        </i>
                                                    </div>
                                                    <div class="infobox-data">
                                                        <span class="infobox-data-number" style="margin-left: 15px;" id="sp_Work_StartedCost" runat="server">0</span>

                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnkNewWork_Started" runat="server" OnClick="lnkNewWork_Started_Click" Font-Bold="true" Text="Open" Font-Size="Medium"></asp:LinkButton></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3">
                                <asp:Label ID="Label2" runat="server" Text="Display Graph For Data" CssClass="control-label no-padding-right"></asp:Label>
                            </div>
                            <div class="col-md-9">
                                <asp:RadioButtonList runat="server" ID="rbtGraphFor" AutoPostBack="true" OnSelectedIndexChanged="rbtGraphFor_SelectedIndexChanged" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="&nbsp;&nbsp;Total Projects&nbsp;&nbsp;" Value="A" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="&nbsp;&nbsp;Ongoing Projects&nbsp;&nbsp;" Value="O"></asp:ListItem>
                                    <asp:ListItem Text="&nbsp;&nbsp;Completed Projects&nbsp;&nbsp;" Value="C"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div id="chartContainerFilter" style="height: 500px; width: 100%; margin: 0px auto;"></div>
                            </div>
                        </div>

                        <div class="space-6"></div>
                        <h3 class="header smaller red">Target Vs Achivement
                        </h3>

                        <div class="row">
                            <div class="col-sm-12">
                                <div class="tabbable">
                                    <ul class="nav nav-tabs" id="myTab2">
                                        <li class="active" id="w_1">
                                            <a data-toggle="tab" href="#doc1" aria-expanded="true" id="wt_1">
                                                <i class="green ace-icon fa fa-file-pdf-o"></i>
                                                PHYSICAL - TARGET VS ACHIEVEMENT
                                            </a>
                                        </li>

                                        <li class="" id="w_2">
                                            <a data-toggle="tab" href="#doc2" aria-expanded="false" id="wt_2">
                                                <i class="green ace-icon fa fa-file-pdf-o"></i>
                                                FINANCIAL - TARGET VS ACHIEVEMENT
                                            </a>
                                        </li>
                                    </ul>
                                    <div class="tab-content">
                                        <div id="doc1" class="tab-pane fade active in">
                                            <div style="overflow: auto">
                                                <asp:GridView ID="grdPhysical" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPhysical_PreRender" ShowFooter="true" OnRowDataBound="grdPhysical_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S No.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Zone_Name" HeaderText="Zone" />
                                                        <asp:BoundField DataField="Circle_Name" HeaderText="Circle" />
                                                        <asp:BoundField DataField="Division_Name" HeaderText="Division" />

                                                        <asp:BoundField DataField="DivisionPhysicalTarget_PhysicalCompletionTarget" HeaderText="Work Completion Target" />
                                                        <asp:BoundField DataField="DivisionPhysicalTarget_PhysicalCompletionAchivment" HeaderText="Work Completion Achievement" />
                                                        <asp:BoundField DataField="PhysicalCompletionAchivment_Per" HeaderText="Work Completion Achievement Precentage (%)" />

                                                        <asp:BoundField DataField="DivisionPhysicalTarget_PhysicalHandoverTarget" HeaderText="Handingover Target" />
                                                        <asp:BoundField DataField="DivisionPhysicalTarget_PhysicalHandoverAchivment" HeaderText="Handingover Achievement" />
                                                        <asp:BoundField DataField="PhysicalHandoverAchivment_Per" HeaderText="Handingover Achievement Precentage (%)" />

                                                        <asp:BoundField DataField="DivisionPhysicalTarget_FinancialHandoverTarget" HeaderText="Financial Closure Target" />
                                                        <asp:BoundField DataField="DivisionPhysicalTarget_FinancialHandoverAchivment" HeaderText="Financial Closure Achievement" />
                                                        <asp:BoundField DataField="FinancialHandoverAchivment_Per" HeaderText="Financial Closure Achievement Precentage (%)" />
                                                    </Columns>
                                                    <FooterStyle Font-Bold="true" BackColor="Black" ForeColor="White" />
                                                </asp:GridView>
                                            </div>
                                        </div>

                                        <div id="doc2" class="tab-pane fade">
                                            <div style="overflow: auto">
                                                <asp:GridView ID="grdFinancial" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdFinancial_PreRender" OnRowDataBound="grdFinancial_RowDataBound" ShowFooter="true">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S No.">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Zone_Name" HeaderText="Zone" />
                                                        <asp:BoundField DataField="Circle_Name" HeaderText="Circle" />
                                                        <asp:BoundField DataField="Division_Name" HeaderText="Division" />

                                                        <asp:BoundField DataField="DivisionFinancialTarget_YearTarget" HeaderText="Financial Target For Financial Year" />
                                                        <asp:BoundField DataField="DivisionFinancialTarget_YearAchivment" HeaderText="Financial Achivment For Financial Year" />
                                                        <asp:BoundField DataField="YearAchivment_Per" HeaderText="Financial Achivment Percentage (%)" />

                                                        <asp:BoundField DataField="DivisionFinancialTarget_Q1Target" HeaderText="Financial Target For Q1" />
                                                        <asp:BoundField DataField="DivisionFinancialTarget_Q1Achivment" HeaderText="Financial Achievement For Q1" />
                                                        <asp:BoundField DataField="Q1Achivment_Per" HeaderText="Q1 Achivment Percentage (%)" />

                                                        <asp:BoundField DataField="DivisionFinancialTarget_Q2Target" HeaderText="Financial Target For Q2" />
                                                        <asp:BoundField DataField="DivisionFinancialTarget_Q2Achivment" HeaderText="Financial Achievement For Q2" />
                                                        <asp:BoundField DataField="Q2Achivment_Per" HeaderText="Q2 Achivment Percentage (%)" />

                                                        <asp:BoundField DataField="DivisionFinancialTarget_Q3Target" HeaderText="Financial Target For Q3" />
                                                        <asp:BoundField DataField="DivisionFinancialTarget_Q3Achivment" HeaderText="Financial Achievement For Q3" />
                                                        <asp:BoundField DataField="Q3Achivment_Per" HeaderText="Q3 Achivment Percentage (%)" />

                                                        <asp:BoundField DataField="DivisionFinancialTarget_Q4Target" HeaderText="Financial Target For Q4" />
                                                        <asp:BoundField DataField="DivisionFinancialTarget_Q4Achivment" HeaderText="Financial Achievement For Q4" />
                                                        <asp:BoundField DataField="Q4Achivment_Per" HeaderText="Q4 Achivment Percentage (%)" />
                                                    </Columns>
                                                    <FooterStyle Font-Bold="true" BackColor="Black" ForeColor="White" />
                                                </asp:GridView>
                                            </div>
                                        </div>


                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="space-6"></div>

                        <h3 class="header smaller red">Projects Progress [Physical & Financial])
                        </h3>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <asp:GridView ID="grdPMISUpdation" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" EmptyDataText="No Records Found" OnPreRender="grdPMISUpdation_PreRender">
                                        <Columns>
                                            <asp:BoundField DataField="Data_Type" HeaderText="Project Status" />
                                            <asp:BoundField DataField="Total_Projects" HeaderText="Total Projects" />
                                            <asp:TemplateField HeaderText="Progress 100%">
                                                <ItemTemplate>
                                                    <div class="row">
                                                        <div class="col-md-6 pull-left">
                                                            <asp:LinkButton ID="lblUpdation100" runat="server" OnClick="lblUpdation100_Click" Font-Bold="true" Text='<%# Eval("More_100") %>'></asp:LinkButton>
                                                        </div>
                                                        <div class="col-md-6 pull-right">
                                                            <asp:ImageButton ID="btnInfo100" runat="server" ImageUrl="~/assets/images/info.png" Width="20px" Height="20px" OnClick="btnInfo100_Click" ToolTip="Click Here To View Details Of Sanctioned Cost, Release, Expenditure and Remaining Amount To Be Released."></asp:ImageButton>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Progress BW 76% to 99%">
                                                <ItemTemplate>
                                                    <div class="row">
                                                        <div class="col-md-6 pull-left">
                                                            <asp:LinkButton ID="lblUpdation99" runat="server" OnClick="lblUpdation99_Click" Font-Bold="true" Text='<%# Eval("BW_76_99") %>'></asp:LinkButton>
                                                        </div>
                                                        <div class="col-md-6 pull-right">
                                                            <asp:ImageButton ID="btnInfo99" runat="server" ImageUrl="~/assets/images/info.png" Width="20px" Height="20px" OnClick="btnInfo99_Click" ToolTip="Click Here To View Details Of Sanctioned Cost, Release, Expenditure and Remaining Amount To Be Released."></asp:ImageButton>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Progress BW 51% to 75%">
                                                <ItemTemplate>
                                                    <div class="row">
                                                        <div class="col-md-6 pull-left">
                                                            <asp:LinkButton ID="lblUpdation75" runat="server" OnClick="lblUpdation75_Click" Font-Bold="true" Text='<%# Eval("BW_51_75") %>'></asp:LinkButton>
                                                        </div>
                                                        <div class="col-md-6 pull-right">
                                                            <asp:ImageButton ID="btnInfo75" runat="server" ImageUrl="~/assets/images/info.png" Width="20px" Height="20px" OnClick="btnInfo75_Click" ToolTip="Click Here To View Details Of Sanctioned Cost, Release, Expenditure and Remaining Amount To Be Released."></asp:ImageButton>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Progress BW 26% to 50%">
                                                <ItemTemplate>
                                                    <div class="row">
                                                        <div class="col-md-6 pull-left">
                                                            <asp:LinkButton ID="lblUpdation50" runat="server" OnClick="lblUpdation50_Click" Font-Bold="true" Text='<%# Eval("BW_26_50") %>'></asp:LinkButton>
                                                        </div>
                                                        <div class="col-md-6 pull-right">
                                                            <asp:ImageButton ID="btnInfo50" runat="server" ImageUrl="~/assets/images/info.png" Width="20px" Height="20px" OnClick="btnInfo50_Click" ToolTip="Click Here To View Details Of Sanctioned Cost, Release, Expenditure and Remaining Amount To Be Released."></asp:ImageButton>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Progress BW 1 to 25%">
                                                <ItemTemplate>
                                                    <div class="row">
                                                        <div class="col-md-6 pull-left">
                                                            <asp:LinkButton ID="lblUpdation25" runat="server" OnClick="lblUpdation25_Click" Font-Bold="true" Text='<%# Eval("BW_0_25") %>'></asp:LinkButton>
                                                        </div>
                                                        <div class="col-md-6 pull-right">
                                                            <asp:ImageButton ID="btnInfo25" runat="server" ImageUrl="~/assets/images/info.png" Width="20px" Height="20px" OnClick="btnInfo25_Click" ToolTip="Click Here To View Details Of Sanctioned Cost, Release, Expenditure and Remaining Amount To Be Released."></asp:ImageButton>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Progress 0%">
                                                <ItemTemplate>
                                                    <div class="row">
                                                        <div class="col-md-6 pull-left">
                                                            <asp:LinkButton ID="lblUpdation0" runat="server" OnClick="lblUpdation0_Click" Font-Bold="true" Text='<%# Eval("Zero") %>'></asp:LinkButton>
                                                        </div>
                                                        <div class="col-md-6 pull-right">
                                                            <asp:ImageButton ID="btnInfo0" runat="server" ImageUrl="~/assets/images/info.png" Width="20px" Height="20px" OnClick="btnInfo0_Click" ToolTip="Click Here To View Details Of Sanctioned Cost, Release, Expenditure and Remaining Amount To Be Released."></asp:ImageButton>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <div id="chartContainerPhysical" style="height: 500px; width: 100%; margin: 0px auto;"></div>
                            </div>
                            <div class="col-md-6">
                                <div id="chartContainerFinancial" style="height: 500px; width: 100%; margin: 0px auto;"></div>
                            </div>
                        </div>

                        <div class="space-6"></div>
                        <h3 class="header smaller red">Delay Analysis 
                                        <div style="float: right">
                                            <asp:CheckBox Text="Show Only Ongoing Projects" runat="server" Checked="true" AutoPostBack="true" OnCheckedChanged="chkDelayOngoing_CheckedChanged" ID="chkDelayOngoing" />
                                        </div>
                        </h3>




                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-12">
                                                <div class="col-xs-6 col-sm-12 pricing-box">
                                                    <div class="widget-box widget-color-green">
                                                        <div class="widget-header">
                                                            <h5 class="widget-title bigger lighter">Project With No Delay</h5>
                                                        </div>

                                                        <div class="widget-body">
                                                            <div class="widget-main">
                                                                <ul class="list-unstyled spaced2">
                                                                    <li>
                                                                        <div class="infobox infobox-blue">
                                                                            <div class="infobox-icon">
                                                                                <i>
                                                                                    <img src="assets/images/pmis/contractend.png" width="60px" height="60px" />
                                                                                </i>
                                                                            </div>
                                                                            <div class="infobox-data">
                                                                                <span class="infobox-data-number" style="margin-left: 15px;">
                                                                                    <asp:LinkButton ID="lnkNoDelay" runat="server" Font-Bold="true" Text="0" OnClick="lnkNoDelay_Click"></asp:LinkButton></span>

                                                                            </div>
                                                                        </div>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-12">
                                                <div class="col-xs-6 col-sm-12 pricing-box">
                                                    <div class="widget-box widget-color-blue">
                                                        <div class="widget-header">
                                                            <h5 class="widget-title bigger lighter">Projects With Delay</h5>
                                                        </div>

                                                        <div class="widget-body">
                                                            <div class="widget-main">
                                                                <ul class="list-unstyled spaced2">
                                                                    <li>
                                                                        <div class="infobox infobox-blue">
                                                                            <div class="infobox-icon">
                                                                                <i>
                                                                                    <img src="assets/images/pmis/contractend.png" width="60px" height="60px" />
                                                                                </i>
                                                                            </div>
                                                                            <div class="infobox-data">
                                                                                <span class="infobox-data-number" style="margin-left: 15px;">
                                                                                    <asp:LinkButton ID="lnkDelay" runat="server" OnClick="lnkDelay_Click"></asp:LinkButton></span>

                                                                            </div>
                                                                        </div>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-9">
                                    <div id="chartContainerDelay" style="height: 500px; width: 100%; margin: 0px auto;"></div>
                                </div>
                            </div>
                        </div>

                        <div class="space-6"></div>
                        <h3 class="header smaller red">Issue Analysis
                            <div style="float: right">
                                <asp:CheckBox Text="Show Only Ongoing Projects" runat="server" Checked="true" AutoPostBack="true" OnCheckedChanged="chkIssueOngoing_CheckedChanged" ID="chkIssueOngoing" />
                            </div>
                        </h3>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <asp:GridView ID="grdIssueReported" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" EmptyDataText="No Records Found" OnPreRender="grdIssueReported_PreRender" ShowFooter="true">
                                        <Columns>
                                            <asp:BoundField DataField="ProjectWorkIssueDetails_Issue_Id" HeaderText="ProjectWorkIssueDetails_Issue_Id">
                                                <HeaderStyle CssClass="displayStyle" />
                                                <ItemStyle CssClass="displayStyle" />
                                                <FooterStyle CssClass="displayStyle" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="S No.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ProjectIssue_Name" HeaderText="Issue" />
                                            <asp:TemplateField HeaderText="Total Issues">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkTotalIssues" runat="server" OnClick="lnkTotalIssues_Click" Font-Bold="true" Text='<%# Eval("Total_Isues") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lnkTotalIssuesF" runat="server" OnClick="lnkTotalIssuesF_Click" Font-Bold="true" ForeColor="White"></asp:LinkButton>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="Black" ForeColor="White" Font-Bold="true" />
                                    </asp:GridView>
                                </div>
                                <div class="col-md-6">
                                    <div id="chartContainerIssue" style="height: 500px; width: 100%; margin: 0px auto;"></div>
                                </div>
                            </div>
                        </div>


                        <div class="space-6"></div>
                        <h3 class="header smaller red">Field Visit / Inspection Report
                        </h3>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <div style="overflow: auto">
                                        <asp:GridView ID="grdSiteVisit2" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdSiteVisit2_PreRender">
                                            <Columns>
                                                <asp:BoundField DataField="ProjectVisit_AddedBy" HeaderText="ProjectVisit_AddedBy">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="S No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Officer" DataField="Person_Name" />
                                                <asp:BoundField HeaderText="Designation" DataField="Designation_DesignationName" />
                                                <asp:TemplateField HeaderText="Total Visits Made">
                                                    <ItemTemplate>
                                                        <a target="_blank" href="ProjectWorkFieldVisitView.aspx?Mode=AddedBy&Added_By=<%# Eval("ProjectVisit_AddedBy") %>"><%# Eval("Total_Visits") %></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup1" Style="display: none; width: 1000px; height: 200px; margin-left: -32px" ScrollBars="Auto">

                            <div class="space-6"></div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-xs-6 col-sm-3 pricing-box">
                                        <div class="widget-box widget-color-red">
                                            <div class="widget-header">
                                                <h5 class="widget-title bigger lighter">Sanctioned Cost</h5>
                                            </div>

                                            <div class="widget-body">
                                                <div class="widget-main">
                                                    <ul class="list-unstyled spaced2">
                                                        <li>
                                                            <div class="infobox infobox-blue">
                                                                <div class="infobox-data">
                                                                    <span class="infobox-data-number" style="margin-left: 15px;">
                                                                        <asp:Label ID="lblSanctionedCost" runat="server" Font-Bold="true" Text="0"></asp:Label></span>
                                                                </div>
                                                            </div>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xs-6 col-sm-3 pricing-box">
                                        <div class="widget-box widget-color-grey">
                                            <div class="widget-header">
                                                <h5 class="widget-title bigger lighter">Total Released</h5>
                                            </div>

                                            <div class="widget-body">
                                                <div class="widget-main">
                                                    <ul class="list-unstyled spaced2">
                                                        <li>
                                                            <div class="infobox infobox-blue">
                                                                <div class="infobox-data">
                                                                    <span class="infobox-data-number" style="margin-left: 15px;">
                                                                        <asp:Label ID="lblTotalReleased" runat="server" Font-Bold="true" Text="0"></asp:Label></span>
                                                                </div>
                                                            </div>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xs-6 col-sm-3 pricing-box">
                                        <div class="widget-box widget-color-green">
                                            <div class="widget-header">
                                                <h5 class="widget-title bigger lighter">Total Expenditure</h5>
                                            </div>

                                            <div class="widget-body">
                                                <div class="widget-main">
                                                    <ul class="list-unstyled spaced2">
                                                        <li>
                                                            <div class="infobox infobox-blue">
                                                                <div class="infobox-data">
                                                                    <span class="infobox-data-number" style="margin-left: 15px;">
                                                                        <asp:Label ID="lblTotalExpenditure" runat="server" Font-Bold="true" Text="0"></asp:Label></span>
                                                                </div>
                                                            </div>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xs-6 col-sm-3 pricing-box">
                                        <div class="widget-box widget-color-blue">
                                            <div class="widget-header">
                                                <h5 class="widget-title bigger lighter">Remaining Amount To Be Released</h5>
                                            </div>

                                            <div class="widget-body">
                                                <div class="widget-main">
                                                    <ul class="list-unstyled spaced2">
                                                        <li>
                                                            <div class="infobox infobox-blue">
                                                                <div class="infobox-data">
                                                                    <span class="infobox-data-number" style="margin-left: 15px;">
                                                                        <asp:Label ID="lblRemainingAmount" runat="server"></asp:Label></span>
                                                                </div>
                                                            </div>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <button id="btnclose" runat="server" text="Close" cssclass="btn btn-warning" style="display: none"></button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:HiddenField runat="server" ID="hf_Delay_Analysis" Value="" />
                        <asp:HiddenField runat="server" ID="hf_Issue_Analysis" Value="" />

                        <asp:HiddenField runat="server" ID="hf_Financal_Progress_Filter" Value="" />
                        <asp:HiddenField runat="server" ID="hf_Physical_Progress_Filter" Value="" />
                        <asp:HiddenField runat="server" ID="hf_Data_Filter" Value="" />
                        <asp:HiddenField runat="server" ID="hf_Over_All_Data" Value="" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="up">
                <ProgressTemplate>
                    <div style="position: fixed; z-index: 999; height: 100%; width: 100%; top: 0; background-color: Black; filter: alpha(opacity=60); opacity: 0.6; -moz-opacity: 0.8; cursor: not-allowed;">
                        <div style="z-index: 1000; margin: 300px auto; padding: 10px; width: 130px; background-color: White; border-radius: 10px; filter: alpha(opacity=100); opacity: 1; -moz-opacity: 1;">
                            <img src="assets/images/mb/mbloader.gif" style="height: 100px; width: 100px;" />
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
                var DataTableLength = $('#ctl00_ContentPlaceHolder1_grdOverAllStatus').length;
                var chartContainerOverAll = $('#chartContainerOverAll');
                if (chartContainerOverAll != undefined) {
                    if (DataTableLength > 0) {
                        var outerHTML = $('#ctl00_ContentPlaceHolder1_grdOverAllStatus')[0].outerText;
                        if (outerHTML.trim() !== "No Records Found") {
                            //initiate dataTables plugin
                            var myTable =
                                $('#ctl00_ContentPlaceHolder1_grdOverAllStatus')
                                    //.wrap("<div class='dataTables_borderWrap' />")   //if you are applying horizontal scrolling (sScrollX)
                                    .DataTable({
                                        mark: true,
                                        colReorder: true,
                                        fixedHeader: {
                                            header: true,
                                            footer: false
                                        },
                                        bAutoWidth: false,
                                        "aoColumns": [
                                            null, null, null, null, null, null, null
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
                            myTable.buttons().container().appendTo($('.grdOverAllStatustableTools-container'));

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
                                $('.dt-button-collection').appendTo('.grdOverAllStatustableTools-container .dt-buttons')
                            });
                            ////
                            setTimeout(function () {
                                $($('.grdOverAllStatustableTools-container')).find('a.dt-button').each(function () {
                                    var div = $(this).find(' > div').first();
                                    if (div.length == 1) div.tooltip({ container: 'body', title: div.parent().text() });
                                    else $(this).tooltip({ container: 'body', title: $(this).text() });
                                });
                            }, 500);

                            $(document).on('click', '#ctl00_ContentPlaceHolder1_grdOverAllStatus .dropdown-toggle', function (e) {
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
                }
            })
        });
    </script>

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            jQuery(function ($) {
                var DataTableLength = $('#ctl00_ContentPlaceHolder1_grdNodalDept').length;
                var chartContainerOverAll = $('#chartContainerOverAll');
                if (chartContainerOverAll != undefined) {
                    if (DataTableLength > 0) {
                        var outerHTML = $('#ctl00_ContentPlaceHolder1_grdNodalDept')[0].outerText;
                        if (outerHTML.trim() !== "No Records Found") {
                            //initiate dataTables plugin
                            var myTable =
                                $('#ctl00_ContentPlaceHolder1_grdNodalDept')
                                    //.wrap("<div class='dataTables_borderWrap' />")   //if you are applying horizontal scrolling (sScrollX)
                                    .DataTable({
                                        mark: true,
                                        colReorder: true,
                                        fixedHeader: {
                                            header: true,
                                            footer: false
                                        },
                                        bAutoWidth: false,
                                        "aoColumns": [
                                            null, null, null, null, null, null, null, null, null, null, null, null, null, null, null
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
                            myTable.buttons().container().appendTo($('.grdNodalDepttableTools-container'));

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
                                $('.dt-button-collection').appendTo('.grdNodalDepttableTools-container .dt-buttons')
                            });
                            ////
                            setTimeout(function () {
                                $($('.grdNodalDepttableTools-container')).find('a.dt-button').each(function () {
                                    var div = $(this).find(' > div').first();
                                    if (div.length == 1) div.tooltip({ container: 'body', title: div.parent().text() });
                                    else $(this).tooltip({ container: 'body', title: $(this).text() });
                                });
                            }, 500);

                            $(document).on('click', '#ctl00_ContentPlaceHolder1_grdNodalDept .dropdown-toggle', function (e) {
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

    </script>

    <script src="canvasjs/canvasjs.min.js"></script>

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            jQuery(function ($) {
                var hf_Over_All_Data = $('#ctl00_ContentPlaceHolder1_hf_Over_All_Data').val();
                var Over_All_Data;
                Over_All_Data = JSON.parse(hf_Over_All_Data);
                if (Over_All_Data != undefined && Over_All_Data != "") {
                    var chartP = new CanvasJS.Chart("chartContainerOverAll", {
                        animationEnabled: true,
                        title: {
                            text: "PROJECT DASHBOARD"
                        },
                        axisY: {
                            title: "No Of Projects",
                            titleFontColor: "#4F81BC",
                            lineColor: "#4F81BC",
                            labelFontColor: "#4F81BC",
                            tickColor: "#4F81BC"
                        },
                        axisY2: {
                            title: "Amount In Lakhs",
                            titleFontColor: "#C0504E",
                            lineColor: "#C0504E",
                            labelFontColor: "#C0504E",
                            tickColor: "#C0504E"
                        },
                        toolTip: {
                            shared: true
                        },
                        legend: {
                            cursor: "pointer",
                            itemclick: toggleDataSeries
                        },
                        data: [{
                            type: "column",
                            name: "Total No Of Project",
                            legendText: "No Of Project",
                            showInLegend: true,
                            dataPoints: [
                                { label: "Total Project", y: Over_All_Data[0].Total },
                                { label: "With In UP", y: Over_All_Data[0].WithIn_UP },
                                { label: "Outside UP", y: Over_All_Data[0].OutSide_UP }
                            ]
                        },
                        {
                            type: "column",
                            name: "Sanctioned Cost (In Lakhs)",
                            legendText: "Sanctioned Cost",
                            axisYType: "secondary",
                            showInLegend: true,
                            dataPoints: [
                                { label: "Total Project", y: Over_All_Data[1].Total },
                                { label: "With In UP", y: Over_All_Data[1].WithIn_UP },
                                { label: "Outside UP", y: Over_All_Data[1].OutSide_UP }
                            ]
                        },
                        {
                            type: "column",
                            name: "Released Amount (In Lakhs)",
                            legendText: "Released Amount",
                            axisYType: "secondary",
                            showInLegend: true,
                            dataPoints: [
                                { label: "Total Project", y: Over_All_Data[2].Total },
                                { label: "With In UP", y: Over_All_Data[2].WithIn_UP },
                                { label: "Outside UP", y: Over_All_Data[2].OutSide_UP }
                            ]
                        },
                        {
                            type: "column",
                            name: "Total Expenditure (In Lakhs)",
                            legendText: "Total Expenditure",
                            axisYType: "secondary",
                            showInLegend: true,
                            dataPoints: [
                                { label: "Total Project", y: Over_All_Data[3].Total },
                                { label: "With In UP", y: Over_All_Data[3].WithIn_UP },
                                { label: "Outside UP", y: Over_All_Data[3].OutSide_UP }
                            ]
                        }]
                    });
                    chartP.render();
                }
                function toggleDataSeries(e) {
                    if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
                        e.dataSeries.visible = false;
                    }
                    else {
                        e.dataSeries.visible = true;
                    }
                    chartP.render();
                }
            })
        })
    </script>

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            jQuery(function ($) {
                var hf_Physical_Progress_Filter = $('#ctl00_ContentPlaceHolder1_hf_Physical_Progress_Filter').val();
                var Physical_Progress_Filter;
                Physical_Progress_Filter = JSON.parse(hf_Physical_Progress_Filter);
                if (Physical_Progress_Filter != undefined && Physical_Progress_Filter != "") {
                    var chartP = new CanvasJS.Chart("chartContainerPhysical", {
                        animationEnabled: true,
                        title: {
                            text: "Physical Progress",
                            horizontalAlign: "left"
                        },
                        data: [{
                            type: "doughnut",
                            startAngle: 60,
                            //innerRadius: 60,
                            indexLabelFontSize: 12,
                            indexLabel: "{label} - {y}",
                            toolTipContent: "<b>{label}:</b> {y}",
                            dataPoints: [
                                { y: Physical_Progress_Filter.Zero, label: "Project Not Started" },
                                { y: Physical_Progress_Filter.BW_0_25, label: "Progress BW 1 to 25%" },
                                { y: Physical_Progress_Filter.BW_26_50, label: "Progress BW 26 to 50%" },
                                { y: Physical_Progress_Filter.BW_51_75, label: "Progress BW 51 to 75%" },
                                { y: Physical_Progress_Filter.BW_76_99, label: "Progress BW 76 to 99%" },
                                { y: Physical_Progress_Filter.More_100, label: "Progress 100%" }
                            ]
                        }]
                    });
                    chartP.render();
                }
            })
        })
    </script>

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            jQuery(function ($) {
                var hf_Financal_Progress_Filter = $('#ctl00_ContentPlaceHolder1_hf_Financal_Progress_Filter').val();
                var Financal_Progress_Filter;
                Financal_Progress_Filter = JSON.parse(hf_Financal_Progress_Filter);
                if (Financal_Progress_Filter != undefined && Financal_Progress_Filter != "") {
                    var chartP = new CanvasJS.Chart("chartContainerFinancial", {
                        animationEnabled: true,
                        title: {
                            text: "Financial Progress",
                            horizontalAlign: "left"
                        },
                        data: [{
                            type: "pie",
                            startAngle: 240,
                            yValueFormatString: "##0",
                            indexLabel: "{label} {y}",
                            dataPoints: [
                                { y: Financal_Progress_Filter.Zero, label: "Project Not Started" },
                                { y: Financal_Progress_Filter.BW_0_25, label: "Progress BW 1 to 25%" },
                                { y: Financal_Progress_Filter.BW_26_50, label: "Progress BW 26 to 50%" },
                                { y: Financal_Progress_Filter.BW_51_75, label: "Progress BW 51 to 75%" },
                                { y: Financal_Progress_Filter.BW_76_99, label: "Progress BW 76 to 99%" },
                                { y: Financal_Progress_Filter.More_100, label: "Progress 100%" }
                            ]
                        }]
                    });
                    chartP.render();
                }
            })
        })
    </script>

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            jQuery(function ($) {
                var hf_Data_Filter = $('#ctl00_ContentPlaceHolder1_hf_Data_Filter').val();
                var Data_Filter;
                Data_Filter = JSON.parse(hf_Data_Filter);
                if (Data_Filter != undefined && Data_Filter != "") {
                    var chartP = new CanvasJS.Chart("chartContainerFilter", {
                        animationEnabled: true,
                        theme: "light1", // "light1", "light2", "dark1", "dark2"
                        title: {
                            text: ""
                        },
                        axisY: {
                            title: "Amount (In Lakhs)"
                        },
                        data: [{
                            type: "column",
                            showInLegend: true,
                            legendMarkerColor: "grey",
                            legendText: "All Figures In Lakhs Only",
                            dataPoints: [
                                { y: Data_Filter.Total_Sanction_Cost, label: "Sanctioned Cost" },
                                { y: Data_Filter.Total_Released_Amount, label: "Total Released" },
                                { y: Data_Filter.Total_Expenditure, label: "Total Expenditure" },
                                { y: Data_Filter.Total_Remaining_Amount, label: "Remaining Amount To Be Released" }
                            ]
                        }]
                    });
                    chartP.render();
                }
            })
        })
    </script>

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            jQuery(function ($) {
                var hf_Delay_Analysis = $('#ctl00_ContentPlaceHolder1_hf_Delay_Analysis').val();
                var Delay_Analysis;
                Delay_Analysis = JSON.parse(hf_Delay_Analysis);
                if (Delay_Analysis != undefined && Delay_Analysis != "") {
                    var chartP = new CanvasJS.Chart("chartContainerDelay", {
                        animationEnabled: true,

                        title: {
                            text: "Time Delay Analysis"
                        },
                        axisX: {
                            interval: 1
                        },
                        axisY2: {
                            interlacedColor: "rgba(1,77,101,.2)",
                            gridColor: "rgba(1,77,101,.1)",
                            title: "Number of Projects"
                        },
                        data: [{
                            type: "bar",
                            name: "companies",
                            axisYType: "secondary",
                            color: "#014D65",
                            dataPoints: [
                                { y: Delay_Analysis.Projects_No_Delay, label: "Projects With No Delay" },
                                { y: Delay_Analysis.Projects_Delay, label: "Projects With Delay" },
                                { y: Delay_Analysis.Total_Projects, label: "Total Projects" }
                            ]
                        }]
                    });
                    chartP.render();
                }
            })
        })
    </script>

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            jQuery(function ($) {
                debugger;
                var hf_Issue_Analysis = $('#ctl00_ContentPlaceHolder1_hf_Issue_Analysis').val();
                var Issue_Analysis;
                Issue_Analysis = JSON.parse(hf_Issue_Analysis);
                if (Issue_Analysis != undefined && Issue_Analysis != "") {
                    var chartP = new CanvasJS.Chart("chartContainerIssue", {
                        animationEnabled: true,
                        theme: "light1", // "light1", "light2", "dark1", "dark2"
                        title: {
                            text: ""
                        },
                        axisY: {
                            title: "Total Issues Raised (Count)"
                        },
                        data: [{
                            type: "column",
                            showInLegend: true,
                            legendMarkerColor: "grey",
                            legendText: "Issues Names",
                            dataPoints: [
                                { y: Issue_Analysis[0].Total_Issues, label: Issue_Analysis[0].Issue_Name },
                                { y: Issue_Analysis[1].Total_Issues, label: Issue_Analysis[1].Issue_Name },
                                { y: Issue_Analysis[2].Total_Issues, label: Issue_Analysis[2].Issue_Name },
                                { y: Issue_Analysis[3].Total_Issues, label: Issue_Analysis[3].Issue_Name },
                                { y: Issue_Analysis[4].Total_Issues, label: Issue_Analysis[4].Issue_Name }
                            ]
                        }]
                    });
                    chartP.render();
                }
            })
        })
    </script>
</asp:Content>

