<%@ Page Language="C#" MasterPageFile="~/TemplatePopup.master" AutoEventWireup="true" CodeFile="ProjectWorkFeildVisitUpload.aspx.cs" Inherits="ProjectWorkFeildVisitUpload" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .LOGIN_FORM {
            border: 2px solid darkgreen;
            border-style: groove;
            background-image: linear-gradient(to top left,white,#abbcf1);
            border-radius: 7px;
            box-shadow: rgb(128 128 128 / 0.89) 5px 5px 5px;
        }

        .heading {
            text-align: center;
            color: whitesmoke;
            /*text-shadow:3px 4px 1px gray;*/
            font-family: sans-serif;
            font-weight: bold;
            transition: 0.6s;
        }

            .heading:hover {
                letter-spacing: 1.5px;
                color: orangered;
            }
    </style>
    <div class="main-content">
        <div class="main-content-inner">
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true" AsyncPostBackTimeout="6000">
            </cc1:ToolkitScriptManager>
            <asp:UpdatePanel ID="up" runat="server">
                <ContentTemplate>
                    <div class="page-content">
                        <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="btnShowPopup"
                            CancelControlID="btnclose" BackgroundCssClass="modalBackground1">
                        </cc1:ModalPopupExtender>
                        <asp:Button ID="btnShowPopup" Text="Show" runat="server" Style="display: none;"></asp:Button>

                        <div id="divLoginStatus" runat="server" visible="false">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-6">
                                        <h3 class="header smaller lighter blue" id="lblWelcome" runat="server">Welcome!!</h3>
                                        <asp:LinkButton ID="btnNewFormat" runat="server" Text="Switch To New Field Visit Format" OnClick="btnNewFormat_Click" Font-Bold="true"></asp:LinkButton>
                                    </div>
                                    <div class="col-md-6 pull-right">
                                        <div class="form-group">
                                            <asp:Button ID="btnLogout" runat="server" Text="Logout" CssClass="width-35 pull-right btn btn-sm btn-primary" OnClick="btnLogout_Click" />
                                            &nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnSearchAnother" runat="server" Text="Change Project" CssClass="width-35 pull-right btn btn-sm btn-warning" OnClick="btnSearchAnother_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- Login Form Starts  -->

                        <div id="divLogin" runat="server">
                            <br>
                            <br />
                            <div class="row">
                                <div class="col-md-4"></div>
                                <div class="col-md-4 LOGIN_FORM">
                                    <div class="row">
                                        <div class="col-md-12" style="border-bottom: 2px solid whitesmoke; padding-top: 5px;">
                                            <h2 class="heading"><i class="blue fa fa-user"></i>&nbsp;Enter The Credentials</h2>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row col-md-12">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">User Name </label>
                                            <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" placeholder="User Name" autocomplete="off">                                       
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row col-md-12">
                                        <div class="form-group">
                                            <asp:Label ID="Label1" runat="server" Text="Password" CssClass="control-label no-padding-right"></asp:Label>
                                            <asp:TextBox ID="txtPassowrd" runat="server" CssClass="form-control" placeholder="Password" autocomplete="off" TextMode="Password">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row col-md-12">
                                        <div class="form-group">
                                            <label class="control-label no-padding-right">Fill Captcha Given Below</label>
                                            <asp:TextBox ID="txtCaptcha" runat="server" CssClass="form-control" placeholder="Captcha">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-9">
                                            <div class="form-group">
                                                <cc1:CaptchaControl ID="Captcha1" runat="server" CaptchaBackgroundNoise="Low" CaptchaLength="5"
                                                    CaptchaHeight="60" CaptchaWidth="200" CaptchaMinTimeout="5" CaptchaMaxTimeout="240"
                                                    FontColor="#D20B0C" NoiseColor="#B1B1B1" />
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:ImageButton ImageUrl="~/refresh.png" runat="server" CausesValidation="false" />
                                                <asp:CustomValidator ErrorMessage="Invalid. Please try again." OnServerValidate="ValidateCaptcha"
                                                    runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <br />
                                                <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="width-35 pull-right btn btn-sm btn-primary" OnClick="btnLogin_Click" /><br />
                                                <br />
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <br />
                                                <asp:Button ID="btnBack" runat="server" Text="Back To Main Login" CssClass="width-60 pull-right btn btn-sm btn-danger" OnClick="btnBack_Click" /><br />
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4"></div>
                    </div>
                    </div>
                                    <!-- Login Form ENd -->

                    <div id="divSearch" runat="server" visible="false">
                        <div class="row" style="overflow: auto">
                            <div class="col-xs-12">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <h3 class="header smaller lighter blue">Project List
                                        </h3>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="control-label no-padding-right">Scheme </label>
                                                        <asp:DropDownList ID="ddlScheme" runat="server" class="form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblZoneH" runat="server" Text="Zone" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlSearchZone" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlSearchZone_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-3" id="div1" runat="server">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblCircleH" runat="server" Text="Circle" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlSearchCircle" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSearchCircle_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-3" id="div2" runat="server">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblDivisionH" runat="server" Text="Division" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:DropDownList ID="ddlsearchDivision" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label14" runat="server" Text="Project Code" CssClass="control-label no-padding-right"></asp:Label>
                                                        <asp:TextBox ID="txtProjectCode" runat="server" CssClass="form-control" AutoCompleteType="None"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <br />
                                                        <asp:Button ID="btnSearch" Text="Search" OnClick="btnSearch_Click" runat="server" CssClass="btn btn-warning"></asp:Button>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <br />
                                                        <asp:Button ID="btnClose1" Text="Close" OnClick="btnClose1_Click" runat="server" CssClass="btn btn-danger"></asp:Button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- div.table-responsive -->
                                        <div class="clearfix" id="dtOptions" runat="server">
                                            <div class="pull-right tableTools-container"></div>
                                        </div>
                                        <!-- div.dataTables_borderWrap -->
                                        <div style="overflow: auto">
                                            <asp:GridView ID="grdPost" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender" OnRowDataBound="grdPost_RowDataBound">
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
                                                    <asp:BoundField DataField="ProjectWork_ProjectType_Id" HeaderText="ProjectWork_ProjectType_Id">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ProjectWork_DivisionId" HeaderText="ProjectWork_DivisionId">
                                                        <HeaderStyle CssClass="displayStyle" />
                                                        <ItemStyle CssClass="displayStyle" />
                                                        <FooterStyle CssClass="displayStyle" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="S No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Select">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnEdit" Width="20px" Height="20px" OnClick="btnEdit_Click" ImageUrl="~/assets/images/edit.png" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="District" DataField="Jurisdiction_Name_Eng" />
                                                    <asp:BoundField HeaderText="Zone" DataField="Zone_Name" />
                                                    <asp:BoundField HeaderText="Circle" DataField="Circle_Name" />
                                                    <asp:BoundField HeaderText="Division" DataField="Division_Name" />
                                                    <asp:BoundField HeaderText="Project Code" DataField="ProjectWork_ProjectCode" />
                                                    <asp:BoundField HeaderText="Work" DataField="ProjectWork_Name" />
                                                    <asp:BoundField HeaderText="Budget (In Lakhs)" DataField="ProjectWork_Budget" />
                                                    <asp:BoundField HeaderText="GO No" DataField="ProjectWork_GO_No" />
                                                    <asp:BoundField HeaderText="GO Date" DataField="ProjectWork_GO_Date" />
                                                    <asp:BoundField HeaderText="Physical %" DataField="Physical_Progress" />
                                                    <asp:BoundField HeaderText="Financial %" DataField="Financial_Progress" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div id="divUpload" runat="server" visible="false">
                        <div class="row">
                            <div style="overflow: auto">
                                <asp:GridView ID="grdPost1" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPost1_PreRender" OnRowDataBound="grdPost1_RowDataBound">
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
                                        <asp:BoundField DataField="ProjectWork_ProjectType_Id" HeaderText="ProjectWork_ProjectType_Id">
                                            <HeaderStyle CssClass="displayStyle" />
                                            <ItemStyle CssClass="displayStyle" />
                                            <FooterStyle CssClass="displayStyle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ProjectWork_DivisionId" HeaderText="ProjectWork_DivisionId">
                                            <HeaderStyle CssClass="displayStyle" />
                                            <ItemStyle CssClass="displayStyle" />
                                            <FooterStyle CssClass="displayStyle" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="S No.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="District" DataField="Jurisdiction_Name_Eng" />
                                        <asp:BoundField HeaderText="Zone" DataField="Zone_Name" />
                                        <asp:BoundField HeaderText="Circle" DataField="Circle_Name" />
                                        <asp:BoundField HeaderText="Division" DataField="Division_Name" />
                                        <asp:BoundField HeaderText="Project Code" DataField="ProjectWork_ProjectCode" />
                                        <asp:BoundField HeaderText="Work" DataField="ProjectWork_Name" />
                                        <asp:BoundField HeaderText="Budget (In Lakhs)" DataField="ProjectWork_Budget" />
                                        <asp:BoundField HeaderText="GO No" DataField="ProjectWork_GO_No" />
                                        <asp:BoundField HeaderText="GO Date" DataField="ProjectWork_GO_Date" />
                                        <asp:BoundField HeaderText="Physical %" DataField="Physical_Progress" />
                                        <asp:BoundField HeaderText="Financial %" DataField="Financial_Progress" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="table-header">
                                    Component Wise Breakup 
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div style="overflow: auto">
                                <asp:GridView ID="grdPhysicalProgress" runat="server" CssClass="table table-responsive table-striped table-bordered table-hover" AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPreRender="grdPhysicalProgress_PreRender">
                                    <Columns>
                                        <asp:BoundField DataField="Component_Id" HeaderText="Component_Id">
                                            <HeaderStyle CssClass="displayStyle" />
                                            <ItemStyle CssClass="displayStyle" />
                                            <FooterStyle CssClass="displayStyle" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="S No.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Component_Unit" HeaderText="Component" />
                                        <asp:BoundField DataField="Proposed" HeaderText="Proposed" />
                                        <asp:BoundField DataField="PhysicalProgress" HeaderText="Completed" />
                                        <asp:BoundField DataField="Functional" HeaderText="Functional" />
                                        <asp:BoundField DataField="NonFunctional" HeaderText="Non Functional" />
                                        <asp:BoundField DataField="Percentage_Cpmpleted" HeaderText="Completed Percentage" />
                                        <asp:BoundField DataField="Percentage_Cpmpleted_Functional" HeaderText="Completed Percentage (Functional)" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="tabbable">
                                    <ul class="nav nav-tabs" id="myTab22">
                                        <li class="active" id="inv_1" onclick="setTabPageActive('inv_1', 't_1', 'doc11', 2)">
                                            <a data-toggle="tab" href="#doc11" aria-expanded="true" id="t_1">
                                                <i class="green ace-icon fa fa-file-pdf-o"></i>
                                                Add New Field Visit
                                            </a>
                                        </li>

                                        <li class="" id="inv_2" onclick="setTabPageActive('inv_2', 't_2', 'doc22', 2)">
                                            <a data-toggle="tab" href="#doc22" aria-expanded="false" id="t_2">
                                                <i class="green ace-icon fa fa-file-pdf-o"></i>
                                                Details Of Previous Visits
                                            </a>
                                        </li>
                                    </ul>
                                    <div class="tab-content">
                                        <div id="doc11" class="tab-pane fade active in">
                                            <div class="row">
                                                <table class="display table table-bordered no-margin-bottom no-border-top">
                                                    <thead>
                                                        <tr>
                                                            <th>Description</th>
                                                            <th>Response</th>
                                                            <th>Response</th>
                                                        </tr>
                                                    </thead>

                                                    <tbody>
                                                        <tr>
                                                            <td runat="server" id="Q1">Name and Designation of Concerned Officer(s) conducting the site visit:</td>
                                                            <td>
                                                                <asp:TextBox ID="txtName1" runat="server" CssClass="form-control" placeholder="Officer Name"></asp:TextBox></td>
                                                            <td>
                                                                <asp:TextBox ID="txtDesignation1" runat="server" CssClass="form-control" placeholder="Designation"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td runat="server" id="Q2">Name and Designation of Key Concerned Officer(s) interacted during visit:</td>
                                                            <td>
                                                                <asp:TextBox ID="txtName2" runat="server" CssClass="form-control" placeholder="Officer Name"></asp:TextBox></td>
                                                            <td>
                                                                <asp:TextBox ID="txtDesignation2" runat="server" CssClass="form-control" placeholder="Designation"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td runat="server" id="Q3">Visit Number:</td>
                                                            <td colspan="2">
                                                                <asp:DropDownList ID="ddlVisitNumber" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlVisitNumber_SelectedIndexChanged">
                                                                    <asp:ListItem Selected="True" Text="---Select---" Value="0"></asp:ListItem>
                                                                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                                    <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                                                    <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                                                    <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                                    <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                    <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                                    <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                                    <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                                    <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                                    <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                                    <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                                    <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                                    <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                                    <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                                                    <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td runat="server" id="Q4">Visit Date:</td>
                                                            <td colspan="2">
                                                                <asp:TextBox ID="txtVisitDate" runat="server" CssClass="form-control date-picker" autocomplete="off"
                                                                    data-date-format="dd/mm/yyyy"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td runat="server" id="Q5">Current Physical Progress:</td>
                                                            <td colspan="2">
                                                                <asp:TextBox ID="txtPhysicalProgress" runat="server" CssClass="form-control" onkeyup="checknum(this);" MaxLength="5"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td runat="server" id="Q41">Scheduled Physical Progress:</td>
                                                            <td colspan="2">
                                                                <asp:TextBox ID="txtPhysicalProgressScheduled" runat="server" CssClass="form-control" onkeyup="checknum(this);" MaxLength="5"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td runat="server" id="Q6">Is the work going on as per estimated time?</td>
                                                            <td colspan="2">
                                                                <asp:RadioButtonList ID="rbtAsPerTime" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                                </asp:RadioButtonList></td>
                                                        </tr>
                                                        <tr id="divEstimatedTime" runat="server">
                                                            <td runat="server" id="Q35">Mention the reason:</td>
                                                            <td colspan="2">
                                                                <asp:TextBox ID="txtTimeDaviationReason" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                            </td>
                                                        </tr>


                                                        <tr>
                                                            <td runat="server" id="Q39">Is the work going on as per estimated cost?</td>
                                                            <td colspan="2">
                                                                <asp:RadioButtonList ID="rbtAsPerCost" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                                </asp:RadioButtonList></td>
                                                        </tr>
                                                        <tr id="divEstimatedCost" runat="server">
                                                            <td runat="server" id="Q40">Mention the reason:</td>
                                                            <td colspan="2">
                                                                <asp:TextBox ID="txtCostDaviationReason" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td runat="server" id="Q7">Is the work halted/stopped?</td>
                                                            <td colspan="2">
                                                                <asp:RadioButtonList ID="rbtHalted" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                                </asp:RadioButtonList></td>
                                                        </tr>
                                                        <tr id="divHaltedYes" runat="server">
                                                            <td runat="server" id="Q8">Mention the reason:</td>
                                                            <td colspan="2">
                                                                <asp:TextBox ID="txtHaltedReason" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td runat="server" id="Q9">Is the work delayed due to any specific reason?</td>
                                                            <td colspan="2">
                                                                <asp:RadioButtonList ID="rbtDelayReason" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                                </asp:RadioButtonList></td>
                                                        </tr>
                                                        <tr id="divDelayReason" runat="server">
                                                            <td runat="server" id="Q10">Mention the reason</td>
                                                            <td colspan="2">
                                                                <asp:TextBox ID="txtDelayReason" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td runat="server" id="Q11">Number of labourer present at the site:</td>
                                                            <td colspan="2">
                                                                <asp:TextBox ID="txtLabourCount" runat="server" CssClass="form-control" onkeyup="checknum(this);" MaxLength="6"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td runat="server" id="Q42">Number of labours Expected at Site as per Schedule:</td>
                                                            <td colspan="2">
                                                                <asp:TextBox ID="txtLabourScheduled" runat="server" CssClass="form-control" onkeyup="checknum(this);" MaxLength="6"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td runat="server" id="Q12">Quality of work observed during the visit:</td>
                                                            <td colspan="2">
                                                                <asp:DropDownList ID="ddlQuality" runat="server" CssClass="form-control">
                                                                    <asp:ListItem Selected="True" Text="---Select---" Value="0"></asp:ListItem>
                                                                    <asp:ListItem Text="Acceptable" Value="Acceptable"></asp:ListItem>
                                                                    <asp:ListItem Text="Not Acceptable" Value="Not Acceptable"></asp:ListItem>
                                                                </asp:DropDownList></td>
                                                        </tr>
                                                        <tr id="divQualityWork" runat="server">
                                                            <td runat="server" id="Q36">If Not Acceptable, mention the reason:</td>
                                                            <td colspan="2">
                                                                <asp:TextBox ID="txtQualityOfWorkReason" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td runat="server" id="Q13">Observe and report on the quality of construction materials used in the project and ascertain from the records</td>
                                                            <td colspan="2">
                                                                <asp:TextBox ID="txtQualityReport" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td runat="server" id="Q14">Is the Work going on as per specifications given in the Contract Agreement</td>
                                                            <td colspan="2">
                                                                <asp:RadioButtonList ID="rbtAsPerDPR" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                                </asp:RadioButtonList></td>
                                                        </tr>
                                                        <tr id="divAsPerDPR" runat="server">
                                                            <td runat="server" id="Q15">If Yes provide justification else mention the details of deviation</td>
                                                            <td colspan="2">
                                                                <asp:TextBox ID="txtDaviation" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox></td>
                                                        </tr>
                                                        <%--<tr>
                                                                    <td runat="server" id="Q16">If there are any major issues related to ascertain the quality of materials used, please specify:</td>
                                                                    <td colspan="2">
                                                                        <asp:TextBox ID="txtMajorIssueQuality" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox></td>
                                                                </tr>--%>
                                                        <tr>
                                                            <td runat="server" id="Q17">Quality Of Workmanship:</td>
                                                            <td colspan="2">
                                                                <asp:DropDownList ID="ddlQualityWorkmanship" runat="server" CssClass="form-control">
                                                                    <asp:ListItem Selected="True" Text="---Select---" Value="0"></asp:ListItem>
                                                                    <asp:ListItem Text="Acceptable" Value="Acceptable"></asp:ListItem>
                                                                    <asp:ListItem Text="Not Acceptable" Value="Not Acceptable"></asp:ListItem>
                                                                </asp:DropDownList></td>
                                                        </tr>
                                                        <tr id="divWorkmanship" runat="server">
                                                            <td runat="server" id="Q37">If Not Acceptable, mention the reason:</td>
                                                            <td colspan="2">
                                                                <asp:TextBox ID="txtWorkmanshipReason" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td runat="server" id="Q18">If there are any cases of non-conformity from quality reviews based on available documents and interactions, please specify:</td>
                                                            <td colspan="2">
                                                                <asp:TextBox ID="txtNonConfirmQuality" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox></td>
                                                        </tr>

                                                        <tr id="divComplianceOpen" runat="server">
                                                            <td runat="server" id="Q38">Have The Compliance Requirements from the Previous Visit been completed:</td>
                                                            <td colspan="2">
                                                                <asp:RadioButtonList ID="rbtCompliance" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                                </asp:RadioButtonList></td>
                                                        </tr>

                                                        <tr id="divCompliance" runat="server">
                                                            <td runat="server" id="Q19">Mention Reason:</td>
                                                            <td colspan="2">
                                                                <asp:TextBox ID="txtCompliance" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td runat="server" id="Q20">Are there any major issues noticed during the visit?</td>
                                                            <td colspan="2">
                                                                <asp:RadioButtonList ID="rbtMajorIssue" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                                </asp:RadioButtonList></td>
                                                        </tr>
                                                        <tr id="divMajorIssue" runat="server">
                                                            <td runat="server" id="Q21">Mention Details of issues</td>
                                                            <td colspan="2">
                                                                <asp:TextBox ID="txtMajorIssue" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td runat="server" id="Q22">Any issues raised by Key Concerned Officer(s) during the visit?</td>
                                                            <td colspan="2">
                                                                <asp:RadioButtonList ID="rbtIssueRelatedOfficers" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                                </asp:RadioButtonList></td>
                                                        </tr>
                                                        <tr id="divIssueRelatedOfficers" runat="server">
                                                            <td runat="server" id="Q23">Mention Details of issues</td>
                                                            <td colspan="2">
                                                                <asp:TextBox ID="txtIssueRelatedOfficers" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td runat="server" id="Q24">Any issues raised by the Contractor during the visit?</td>
                                                            <td colspan="2">
                                                                <asp:RadioButtonList ID="rbtIssueContractor" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                                </asp:RadioButtonList></td>
                                                        </tr>
                                                        <tr id="divIssueContractor" runat="server">
                                                            <td runat="server" id="Q25">Mention Details of issues</td>
                                                            <td colspan="2">
                                                                <asp:TextBox ID="txtIssueContractor" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td runat="server" id="Q26">Instructions Given to the Concerned Officer</td>
                                                            <td colspan="2">
                                                                <asp:TextBox ID="txtInstOfficer" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td runat="server" id="Q27">Instructions Given to the Contractor</td>
                                                            <td colspan="2">
                                                                <asp:TextBox ID="txtInstContractor" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td runat="server" id="Q28">Any other concerns?</td>
                                                            <td colspan="2">
                                                                <asp:TextBox ID="txtOtherConcerns" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td runat="server" id="Q29">Site Location:</td>
                                                            <td>
                                                                <asp:Label ID="lblLat" runat="server" CssClass="control-label no-padding-right"></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="lblLong" runat="server" CssClass="control-label no-padding-right"></asp:Label></td>
                                                        </tr>
                                                        <tr style="display: none">
                                                            <td>Site Address?</td>
                                                            <td colspan="2">
                                                                <asp:Label ID="lblAddress" runat="server" CssClass="control-label no-padding-right"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td runat="server" id="Q30">Upload Photo 1:</td>
                                                            <td colspan="2">
                                                                <asp:FileUpload ID="flAnnexture1" runat="server" /></td>
                                                        </tr>
                                                        <tr>
                                                            <td runat="server" id="Q31">Upload Photo 2:</td>
                                                            <td colspan="2">
                                                                <asp:FileUpload ID="flAnnexture2" runat="server" /></td>
                                                        </tr>
                                                        <tr>
                                                            <td runat="server" id="Q32">Upload Photo 3:</td>
                                                            <td colspan="2">
                                                                <asp:FileUpload ID="flAnnexture3" runat="server" /></td>
                                                        </tr>
                                                        <tr>
                                                            <td runat="server" id="Q33">Upload High Quality Video Clip (Max 500 MB):</td>
                                                            <td colspan="2">
                                                                <asp:FileUpload ID="flVideo" runat="server" /></td>
                                                        </tr>
                                                        <tr>
                                                            <td runat="server" id="Q34">Upload Inspection Report (In PDF File):</td>
                                                            <td colspan="2">
                                                                <asp:FileUpload ID="flInspectionReport" runat="server" /></td>
                                                        </tr>

                                                        <tr>
                                                            <td runat="server" id="Q43">Upload Scheduled Test Report 1 [Civil Work] (In PDF File):</td>
                                                            <td>
                                                                <asp:TextBox ID="txtTestReportCivil1" runat="server" CssClass="form-control" placeholder="Test Name"></asp:TextBox></td>
                                                            <td>
                                                                <asp:FileUpload ID="flTestCivil1" runat="server" /></td>
                                                        </tr>

                                                        <tr>
                                                            <td runat="server" id="Q44">Upload Scheduled Test Report 2 [Civil Work] (In PDF File):</td>
                                                            <td>
                                                                <asp:TextBox ID="txtTestReportCivil2" runat="server" CssClass="form-control" placeholder="Test Name"></asp:TextBox></td>
                                                            <td>
                                                                <asp:FileUpload ID="flTestCivil2" runat="server" /></td>
                                                        </tr>

                                                        <tr>
                                                            <td runat="server" id="Q45">Upload Scheduled Test Report 1 [E/M Work] (In PDF File):</td>
                                                            <td>
                                                                <asp:TextBox ID="txtTestReportEM1" runat="server" CssClass="form-control" placeholder="Test Name"></asp:TextBox></td>
                                                            <td>
                                                                <asp:FileUpload ID="flTestEM1" runat="server" /></td>
                                                        </tr>

                                                        <tr>
                                                            <td runat="server" id="Q46">Upload Scheduled Test Report 2 [E/M Work] (In PDF File):</td>
                                                            <td>
                                                                <asp:TextBox ID="txtTestReportEM2" runat="server" CssClass="form-control" placeholder="Test Name"></asp:TextBox></td>
                                                            <td>
                                                                <asp:FileUpload ID="flTestEM2" runat="server" /></td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <br />
                                                            <asp:Button ID="btnUpload" Text="Upload and Save Visit Details" OnClick="btnUpload_Click" runat="server" CssClass="btn btn-warning"></asp:Button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div id="doc22" class="tab-pane fade">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <br />
                                                            <asp:DropDownList ID="ddlVisitsMade" runat="server" CssClass="form-control">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <br />
                                                            <asp:Button ID="btnGetVisitData" Text="Get Visit Details" OnClick="btnGetVisitData_Click" runat="server" CssClass="btn btn-purple"></asp:Button>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3" id="divReportVerification" runat="server" visible="false">
                                                        <div class="form-group">
                                                            <br />
                                                            <asp:Button ID="btnReportVerification" Text="Open Report Verification" OnClick="btnReportVerification_Click" runat="server" CssClass="btn btn-inverse"></asp:Button>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3" id="divMap" runat="server" visible="false">
                                                        <div class="form-group" id="divMap1" runat="server">
                                                            <br />
                                                            <a onclick="return openPopup(this);" role="button" class="bigger bg-primary white" data-toggle="modal">&nbsp;Map 
                                                            </a>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="col-md-12">
                                                        <div style="overflow: auto">
                                                            <asp:GridView ID="grdVisitDetails" runat="server" AutoGenerateColumns="False" CssClass="display table table-bordered" EmptyDataText="No Records Found" OnPreRender="grdVisitDetails_PreRender">
                                                                <Columns>
                                                                    <asp:BoundField DataField="ProjectUC_Concent_Id" HeaderText="ProjectUC_Concent_Id">
                                                                        <HeaderStyle CssClass="displayStyle" />
                                                                        <ItemStyle CssClass="displayStyle" />
                                                                        <FooterStyle CssClass="displayStyle" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="S No.">
                                                                        <ItemTemplate>
                                                                            <%# Container.DataItemIndex + 1 %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="ProjectUC_Concent_Question" HeaderText="Question" />
                                                                    <asp:BoundField DataField="ProjectUC_Concent_Answer" HeaderText="Response" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="col-md-12">
                                                        <div style="overflow: auto">
                                                            <asp:GridView ID="grdSitePics" runat="server" AutoGenerateColumns="False" CssClass="display table table-bordered" EmptyDataText="No Records Found" OnPreRender="grdSitePics_PreRender">
                                                                <Columns>
                                                                    <asp:BoundField DataField="ProjectPkgSitePics_Id" HeaderText="ProjectPkgSitePics_Id">
                                                                        <HeaderStyle CssClass="displayStyle" />
                                                                        <ItemStyle CssClass="displayStyle" />
                                                                        <FooterStyle CssClass="displayStyle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="ProjectPkgSitePics_SitePic_Path1" HeaderText="ProjectPkgSitePics_SitePic_Path1">
                                                                        <HeaderStyle CssClass="displayStyle" />
                                                                        <ItemStyle CssClass="displayStyle" />
                                                                        <FooterStyle CssClass="displayStyle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="ProjectPkgSitePics_SitePic_Path2" HeaderText="ProjectPkgSitePics_SitePic_Path2">
                                                                        <HeaderStyle CssClass="displayStyle" />
                                                                        <ItemStyle CssClass="displayStyle" />
                                                                        <FooterStyle CssClass="displayStyle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="ProjectPkgSitePics_SitePic_Path3" HeaderText="ProjectPkgSitePics_SitePic_Path3">
                                                                        <HeaderStyle CssClass="displayStyle" />
                                                                        <ItemStyle CssClass="displayStyle" />
                                                                        <FooterStyle CssClass="displayStyle" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="ProjectPkgSitePics_SitePic_Path4" HeaderText="ProjectPkgSitePics_SitePic_Path4">
                                                                        <HeaderStyle CssClass="displayStyle" />
                                                                        <ItemStyle CssClass="displayStyle" />
                                                                        <FooterStyle CssClass="displayStyle" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="S No.">
                                                                        <ItemTemplate>
                                                                            <%# Container.DataItemIndex + 1 %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="ProjectPkgSitePics_ComponentName" HeaderText="Component" />
                                                                    <asp:TemplateField HeaderText="Site Pic 1">
                                                                        <ItemTemplate>
                                                                            <asp:Image ID="img1" Width="200px" Height="200px" runat="server" ImageUrl='<%# Eval("ProjectPkgSitePics_SitePic_Path1") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Site Pic 2">
                                                                        <ItemTemplate>
                                                                            <asp:Image ID="img2" Width="200px" Height="200px" runat="server" ImageUrl='<%# Eval("ProjectPkgSitePics_SitePic_Path2") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Site Pic 3">
                                                                        <ItemTemplate>
                                                                            <asp:Image ID="img3" Width="200px" Height="200px" runat="server" ImageUrl='<%# Eval("ProjectPkgSitePics_SitePic_Path3") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Video">
                                                                        <ItemTemplate>
                                                                            <video width="200" height="200" controls>
                                                                                <source src='<%# Eval("ProjectPkgSitePics_SitePic_Path4") %>' type="video/mp4">
                                                                            </video>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Inspection Report">
                                                                        <ItemTemplate>
                                                                            <a href="<%# Eval("ProjectPkgSitePics_SitePic_Path5") %>" target="_blank">View Report</a>
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
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <br />
                                        <asp:Button ID="btnClose2" Text="Close" OnClick="btnClose1_Click" runat="server" CssClass="btn btn-danger"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup1" Style="display: none; width: 950px; margin-left: -32px" Height="600px" ScrollBars="Auto">
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="table-header">
                                    Field Visit Report Verification Report Submission
                                </div>
                            </div>
                        </div>


                        <div class="row">
                            <table class="display table table-bordered no-margin-bottom no-border-top">
                                <thead>
                                    <tr>
                                        <th>Criteria for evaluation</th>
                                        <th>Appropriate</th>
                                        <th>Not Appropriate</th>
                                        <th>Not Filled</th>
                                        <th>Not Applicable</th>
                                    </tr>
                                </thead>

                                <tbody>
                                    <tr>
                                        <td runat="server" id="QE0">Is the component wise progress checked and mentioned in the report for all the components</td>
                                        <td>
                                            <asp:RadioButton ID="chk_App_0" runat="server" AutoPostBack="true" OnCheckedChanged="EvaluateMarks" GroupName="0"></asp:RadioButton></td>
                                        <td>
                                            <asp:RadioButton ID="chk_NApp_0" runat="server" AutoPostBack="true" OnCheckedChanged="EvaluateMarks" GroupName="0"></asp:RadioButton></td>
                                        <td>
                                            <asp:RadioButton ID="chk_NFilled_0" runat="server" AutoPostBack="true" OnCheckedChanged="EvaluateMarks" GroupName="0"></asp:RadioButton></td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td runat="server" id="QE1">If there is discrepancy in  physical progress on PMIS and field visit, what action suggested in inspection report:</td>
                                        <td>
                                            <asp:RadioButton ID="chk_App_1" runat="server" AutoPostBack="true" OnCheckedChanged="EvaluateMarks" GroupName="1"></asp:RadioButton></td>
                                        <td>
                                            <asp:RadioButton ID="chk_NApp_1" runat="server" AutoPostBack="true" OnCheckedChanged="EvaluateMarks" GroupName="1"></asp:RadioButton></td>
                                        <td>
                                            <asp:RadioButton ID="chk_NFilled_1" runat="server" AutoPostBack="true" OnCheckedChanged="EvaluateMarks" GroupName="1"></asp:RadioButton></td>
                                        <td>
                                            <asp:RadioButton ID="chk_NotApp_1" runat="server" AutoPostBack="true" OnCheckedChanged="EvaluateMarks" GroupName="1"></asp:RadioButton></td>
                                    </tr>
                                    <tr>
                                        <td runat="server" id="QE2">If work is not progressing as per estimated cost and time, what actions suggested in inspection report:</td>
                                        <td>
                                            <asp:RadioButton ID="chk_App_2" runat="server" AutoPostBack="true" OnCheckedChanged="EvaluateMarks" GroupName="2"></asp:RadioButton></td>
                                        <td>
                                            <asp:RadioButton ID="chk_NApp_2" runat="server" AutoPostBack="true" OnCheckedChanged="EvaluateMarks" GroupName="2"></asp:RadioButton></td>
                                        <td>
                                            <asp:RadioButton ID="chk_NFilled_2" runat="server" AutoPostBack="true" OnCheckedChanged="EvaluateMarks" GroupName="2"></asp:RadioButton></td>
                                        <td>
                                            <asp:RadioButton ID="chk_NotApp_2" runat="server" AutoPostBack="true" OnCheckedChanged="EvaluateMarks" GroupName="2"></asp:RadioButton></td>
                                    </tr>
                                    <tr>
                                        <td runat="server" id="QE3">if Work is currently Stopped, is the reason mentioned and remedial measures suggested:</td>
                                        <td>
                                            <asp:RadioButton ID="chk_App_3" runat="server" AutoPostBack="true" OnCheckedChanged="EvaluateMarks" GroupName="3"></asp:RadioButton></td>
                                        <td>
                                            <asp:RadioButton ID="chk_NApp_3" runat="server" AutoPostBack="true" OnCheckedChanged="EvaluateMarks" GroupName="3"></asp:RadioButton></td>
                                        <td>
                                            <asp:RadioButton ID="chk_NFilled_3" runat="server" AutoPostBack="true" OnCheckedChanged="EvaluateMarks" GroupName="3"></asp:RadioButton></td>
                                        <td>
                                            <asp:RadioButton ID="chk_NotApp_3" runat="server" AutoPostBack="true" OnCheckedChanged="EvaluateMarks" GroupName="3"></asp:RadioButton></td>
                                    </tr>
                                    <tr>
                                        <td runat="server" id="QE4">If work is delayed due to some specific reason, has that been mentioned in inspection report and remedial action suggested:</td>
                                        <td>
                                            <asp:RadioButton ID="chk_App_4" runat="server" AutoPostBack="true" OnCheckedChanged="EvaluateMarks" GroupName="4"></asp:RadioButton></td>
                                        <td>
                                            <asp:RadioButton ID="chk_NApp_4" runat="server" AutoPostBack="true" OnCheckedChanged="EvaluateMarks" GroupName="4"></asp:RadioButton></td>
                                        <td>
                                            <asp:RadioButton ID="chk_NFilled_4" runat="server" AutoPostBack="true" OnCheckedChanged="EvaluateMarks" GroupName="4"></asp:RadioButton></td>
                                        <td>
                                            <asp:RadioButton ID="chk_NotApp_4" runat="server" AutoPostBack="true" OnCheckedChanged="EvaluateMarks" GroupName="4"></asp:RadioButton></td>
                                    </tr>

                                    <tr>
                                        <td runat="server" id="QE5">If the quality of work is suggested to be Poor, wheather details and actions have been suggested</td>
                                        <td>
                                            <asp:RadioButton ID="chk_App_5" runat="server" AutoPostBack="true" OnCheckedChanged="EvaluateMarks1" GroupName="5"></asp:RadioButton></td>
                                        <td>
                                            <asp:RadioButton ID="chk_NApp_5" runat="server" AutoPostBack="true" OnCheckedChanged="EvaluateMarks1" GroupName="5"></asp:RadioButton></td>
                                        <td>
                                            <asp:RadioButton ID="chk_NFilled_5" runat="server" AutoPostBack="true" OnCheckedChanged="EvaluateMarks1" GroupName="5"></asp:RadioButton></td>
                                        <td>
                                            <asp:RadioButton ID="chk_NotApp_5" runat="server" AutoPostBack="true" OnCheckedChanged="EvaluateMarks1" GroupName="5"></asp:RadioButton></td>
                                    </tr>

                                    <tr>
                                        <td runat="server" id="QE6">If Quality of material is mentioned as Poor, wheather details and actions have been suggested</td>
                                        <td>
                                            <asp:RadioButton ID="chk_App_6" runat="server" AutoPostBack="true" OnCheckedChanged="EvaluateMarks1" GroupName="6"></asp:RadioButton></td>
                                        <td>
                                            <asp:RadioButton ID="chk_NApp_6" runat="server" AutoPostBack="true" OnCheckedChanged="EvaluateMarks1" GroupName="6"></asp:RadioButton></td>
                                        <td>
                                            <asp:RadioButton ID="chk_NFilled_6" runat="server" AutoPostBack="true" OnCheckedChanged="EvaluateMarks1" GroupName="6"></asp:RadioButton></td>
                                        <td>
                                            <asp:RadioButton ID="chk_NotApp_6" runat="server" AutoPostBack="true" OnCheckedChanged="EvaluateMarks1" GroupName="6"></asp:RadioButton></td>
                                    </tr>

                                    <tr>
                                        <td runat="server" id="QE7">Has the Pdf report for inspections been uploaded</td>
                                        <td>
                                            <asp:RadioButton ID="chk_App_7" runat="server" AutoPostBack="true" OnCheckedChanged="EvaluateMarks" GroupName="7"></asp:RadioButton></td>
                                        <td>
                                            <asp:RadioButton ID="chk_NApp_7" runat="server" AutoPostBack="true" OnCheckedChanged="EvaluateMarks" GroupName="7"></asp:RadioButton></td>
                                        <td>
                                            <asp:RadioButton ID="chk_NFilled_7" runat="server" AutoPostBack="true" OnCheckedChanged="EvaluateMarks" GroupName="7"></asp:RadioButton></td>
                                        <td>&nbsp;</td>
                                    </tr>

                                    <tr>
                                        <td runat="server" id="QE8">In case where compliance to previous visit is not completed, have the comments and actions suggested</td>
                                        <td>
                                            <asp:RadioButton ID="chk_App_8" runat="server" AutoPostBack="true" OnCheckedChanged="EvaluateMarks" GroupName="8"></asp:RadioButton></td>
                                        <td>
                                            <asp:RadioButton ID="chk_NApp_8" runat="server" AutoPostBack="true" OnCheckedChanged="EvaluateMarks" GroupName="8"></asp:RadioButton></td>
                                        <td>
                                            <asp:RadioButton ID="chk_NFilled_8" runat="server" AutoPostBack="true" OnCheckedChanged="EvaluateMarks" GroupName="8"></asp:RadioButton></td>
                                        <td>
                                            <asp:RadioButton ID="chk_NotApp_8" runat="server" AutoPostBack="true" OnCheckedChanged="EvaluateMarks" GroupName="8"></asp:RadioButton></td>
                                    </tr>

                                    <tr>
                                        <td runat="server" id="QE9">Have the quality Photographs of the project and visit been uploaded</td>
                                        <td>
                                            <asp:RadioButton ID="chk_App_9" runat="server" AutoPostBack="true" OnCheckedChanged="EvaluateMarks" GroupName="9"></asp:RadioButton></td>
                                        <td>
                                            <asp:RadioButton ID="chk_NApp_9" runat="server" AutoPostBack="true" OnCheckedChanged="EvaluateMarks" GroupName="9"></asp:RadioButton></td>
                                        <td>
                                            <asp:RadioButton ID="chk_NFilled_9" runat="server" AutoPostBack="true" OnCheckedChanged="EvaluateMarks" GroupName="9"></asp:RadioButton></td>
                                        <td>&nbsp;</td>
                                    </tr>

                                    <tr>
                                        <td runat="server" id="QE10">Is the public interaction done during the field visit</td>
                                        <td>
                                            <asp:RadioButton ID="chk_App_10" runat="server" AutoPostBack="true" OnCheckedChanged="EvaluateMarks" GroupName="10"></asp:RadioButton></td>
                                        <td>
                                            <asp:RadioButton ID="chk_NApp_10" runat="server" AutoPostBack="true" OnCheckedChanged="EvaluateMarks" GroupName="10"></asp:RadioButton></td>
                                        <td>
                                            <asp:RadioButton ID="chk_NFilled_10" runat="server" AutoPostBack="true" OnCheckedChanged="EvaluateMarks" GroupName="10"></asp:RadioButton></td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </tbody>
                                <tfoot style="background-color: gray; font-weight: bold; color: white;">
                                    <tr>
                                        <td colspan="4">Total Marks:</td>
                                        <td runat="server" id="tf_Marks">0 / 100</td>
                                    </tr>
                                </tfoot>
                            </table>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <br />
                                        <asp:Button ID="btnSaveVerificationDetails" Text="Submit Verification Details" OnClick="btnSaveVerificationDetails_Click" runat="server" CssClass="btn btn-inverse"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <button id="btnclose" runat="server" text="Close" cssclass="btn btn-warning" style="display: none"></button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <div id="UpdatePopup" class="modal fade" tabindex="-1" style="padding-left: 0px;">
                        <div class="modal-dialog" style="margin-top: 0px; margin-left: 150px; width: 1000px;">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                    <h3 class="smaller lighter blue no-margin">View Site Location</h3>
                                </div>

                                <div class="modal-body" style="height: 600px;">
                                    <div id="map" style="width: 100%; height: 500px"></div>
                                    <br />
                                    <div id="mapaddress"></div>
                                </div>

                                <div class="modal-footer">
                                    <button class="btn btn-sm btn-danger pull-right" data-dismiss="modal">
                                        <i class="ace-icon fa fa-times"></i>
                                        Close
                                    </button>
                                </div>
                            </div>
                            <!-- /.modal-content -->
                        </div>
                    </div>
                    <asp:HiddenField ID="hf_Physical_Progress" runat="server" Value="0" />
                    <asp:HiddenField ID="hf_Financial_Progress" runat="server" Value="0" />
                    <asp:HiddenField ID="hf_ProjectWork_Id" runat="server" Value="0" />
                    <asp:HiddenField ID="hf_Address" Value="" runat="server" />
                    <asp:HiddenField ID="hf_MarksTotal" Value="" runat="server" />
                    <asp:HiddenField ID="hf_ProjectVisit_Id" Value="" runat="server" />
                    <asp:HiddenField ID="hf_Location" Value="" runat="server" />
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnUpload" />
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
    <script src="assets/js/md5.js"></script>

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
                                        header: true,
                                        footer: false
                                    },
                                    bAutoWidth: false,
                                    "aoColumns": [
                                        null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null
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

                                    "iDisplayLength": 25,
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

    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyAtesZxiIJNhGcfDmtkQRs_OuSmCPUH9f4&sensor=false">  
    </script>

    <script src="Scripts/jquery-1.11.2.js"></script>
    <%--<script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyD1KxtLqZtakEaBbSMouMIPl5tDKUz0IqM"></script>--%>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">

    <script type="text/javascript">  
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(success);
        } else {
            //alert("There is Some Problem on your current browser to get Geo Location!");
        }

        function success(position) {
            var lat = position.coords.latitude;
            var long = position.coords.longitude;
            $('#ctl00_ContentPlaceHolder1_hf_Location').val(lat + "|" + long);
            $('#ctl00_ContentPlaceHolder1_lblLat').val(lat);
            $('#ctl00_ContentPlaceHolder1_lblLong').val(long);
            //var geocoder = new google.maps.Geocoder;
            //geocodeLatLng(geocoder, lat, long);

            var latlng = new google.maps.LatLng(lat, long);
            var geocoder = geocoder = new google.maps.Geocoder();
            geocoder.geocode({ 'latLng': latlng }, function (results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                    if (results[1]) {
                        $('#ctl00_ContentPlaceHolder1_lblAddress').val(results[1].formatted_address);
                        $('#ctl00_ContentPlaceHolder1_hf_Address').val(results[1].formatted_address);
                    }
                }
            });
        }

        function geocodeLatLng(geocoder, lat, lng) {
            var latlng = { lat: parseFloat(lat), lng: parseFloat(lng) };
            geocoder.geocode({ 'location': latlng }, function (results, status) {
                if (status === 'OK') {
                    if (results[0]) {
                        $('#ctl00_ContentPlaceHolder1_lblAddress').val(results[0].formatted_address);
                        $('#ctl00_ContentPlaceHolder1_hf_Address').val(results[0].formatted_address);
                    } else {
                        window.alert('No results found');
                    }
                } else {
                    window.alert('Geocoder failed due to: ' + status);
                }
            });
        }
    </script>

    <script type="text/javascript">
        var _lst = [];
        var _lsttemp = [];
        var _lat;
        var _lng;
        var _address;
        function openPopup(obj) {
            getMapData(obj);
            obj.href = "#UpdatePopup";
            return false;
        }

        function getMapData(obj) {
            _lst = [];
            try {
                _lat = obj.attributes.lat.nodeValue;
                _lng = obj.attributes.long.nodeValue;
                _address = 'Site Location';
                document.getElementById('mapaddress').innerHTML = _address;
                var _obj = {};
                _obj.lat = parseFloat(_lat);
                _obj.lng = parseFloat(_lng);
                _obj.Created_date = "";
                _lst.push(_obj);
                if (_lst.length > 0)
                    initMap();
            }
            catch
            {

            }
        }

        var marker;
        getMapData();

        function initMap() {
            // debugger;
            var a = 0;
            var labels = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ';
            var labelIndex = 0;

            var map = new google.maps.Map(document.getElementById('map'), {
                zoom: 18,
                center: { lat: parseFloat(_lat), lng: parseFloat(_lng) },
                mapTypeId: 'terrain'
            });

            var lineSymbol = {
                path: google.maps.SymbolPath.CIRCLE,
                scale: 1,
                strokeColor: '#393'
            };

            var endPoint = {
                path: 'M17.070 2.93c-3.906-3.906-10.234-3.906-14.141 0-3.906 3.904-3.906 10.238 0 14.14 0.001 0 7.071 6.93 7.071 14.93 0-8 7.070-14.93 7.070-14.93 3.907-3.902 3.907-10.236 0-14.14zM10 14c-2.211 0-4-1.789-4-4s1.789-4 4-4 4 1.789 4 4-1.789 4-4 4z',
                fillOpacity: 1,
                scale: 1.5,
                strokeColor: '#008000',
                //strokeWeight: 8
            };

            var startPoint = {
                path: 'M17.070 2.93c-3.906-3.906-10.234-3.906-14.141 0-3.906 3.904-3.906 10.238 0 14.14 0.001 0 7.071 6.93 7.071 14.93 0-8 7.070-14.93 7.070-14.93 3.907-3.902 3.907-10.236 0-14.14zM10 14c-2.211 0-4-1.789-4-4s1.789-4 4-4 4 1.789 4 4-1.789 4-4 4z',
                fillOpacity: 0.8,
                scale: 1.5,
                // strokeWeight: 10
                strokeColor: '#1e90ff'
            };

            var flightPlanCoordinates = new Array();
            for (var i = 0; i < _lst.length; i++) {
                flightPlanCoordinates.push('lat: ' + _lst[i].lat + ', lng: ' + _lst[i].lng + '');

                if (i == 0) {
                    var marker = new google.maps.Marker({
                        position: _lst[i],
                        map: map,
                        draggable: false,
                        icon: startPoint,
                        //label: labels[labelIndex++ % labels.length],
                        animation: google.maps.Animation.BOUNCE,
                        //animation: google.maps.Animation.DROP,

                        title: 'Lat: ' + _lst[i].lat + ', Lng: ' + _lst[i].lng + ' Time Stamp: ' + _lst[i].Created_date
                    });
                    marker.addListener('click', toggleBounce);

                }
                else if (i == _lst.length - 1) {
                    var marker = new google.maps.Marker({
                        position: _lst[i],
                        map: map,
                        draggable: false,
                        icon: endPoint,
                        animation: google.maps.Animation.BOUNCE,
                        //animation: google.maps.Animation.DROP,
                        title: 'Lat: ' + _lst[i].lat + ', Lng: ' + _lst[i].lng + ' Time Stamp: ' + _lst[i].Created_date
                    });
                    marker.addListener('click', toggleBounce);

                }
                else {
                    var marker = new google.maps.Marker({
                        position: _lst[i],
                        map: map,
                        draggable: false,
                        label: labels[labelIndex++ % labels.length],
                        //animation: google.maps.Animation.BOUNCE,
                        //animation: google.maps.Animation.DROP,
                        title: 'Lat: ' + _lst[i].lat + ', Lng: ' + _lst[i].lng + ' Time Stamp: ' + _lst[i].Created_date
                    });
                    marker.addListener('click', toggleBounce);
                }
            }

            flightPlanCoordinates = _lst;
            var flightPath = new google.maps.Polyline({
                path: flightPlanCoordinates,
                geodesic: true,
                strokeColor: '#393',
                strokeOpacity: 1.0,
                strokeWeight: 1
            });
            flightPath.setMap(map);
            marker = new google.maps.Marker({
                map: map,
                draggable: true,
                animation: google.maps.Animation.DROP,
            });
            marker.addListener('click', toggleBounce);

            var lineSymbol = {
                path: google.maps.SymbolPath.FORWARD_CLOSED_ARROW,
                scale: 4,
                strokeColor: '#393'
            };
            var line = new google.maps.Polyline({
                path: flightPlanCoordinates,
                icons: [{
                    icon: lineSymbol,
                    offset: '100%'
                }],
                map: map
            });

            animateCircle(line);

            function animateCircle(line) {
                var count = 0;
                window.setInterval(function () {
                    count = (count + 1) % 200;

                    var icons = line.get('icons');
                    icons[0].offset = (count / 2) + '%';
                    line.set('icons', icons);
                }, 400);
            }

            function toggleBounce() { };
        };

        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            jQuery(function ($) {
                $('.modalBackground1').click(function () {
                    var id = $(this).attr('id').replace('_backgroundElement', '');
                    $find(id).hide();
                });
            })
        });
    </script>

    <script type="text/javascript">

        function setTabPageActive(mainMenuId, subMenuId, contentPageId, totalCount) {
            for (var i = 0; i < totalCount; i++) {
                $("#inv_" + (i + 1)).removeClass('active');
                $("#t_" + (i + 1)).attr('aria-expanded', 'false');
                $("#doc" + (i + 1) + (i + 1)).removeClass('active in');
            }
            //$('#nav nav-tabs').find('li[class^="active"]').removeClass('active');
            //$('#nav nav-tabs').find('a').removeAttr('aria-expanded');

            $("#" + mainMenuId + "").addClass('active');
            $("#" + subMenuId + "").attr('aria-expanded', 'true');
            $("#" + contentPageId + "").addClass('active in');

            sessionStorage["_activeMainTabMenu"] = mainMenuId;
            sessionStorage["_activeSubTabMenu"] = subMenuId;
            sessionStorage["_activecontentPageId"] = contentPageId;
            sessionStorage["_activetotalCount"] = totalCount;
        }

        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            $(document).ready(function () {
                debugger;
                if (sessionStorage["_activeMainTabMenu"] == "" || sessionStorage["_activeSubTabMenu"] == undefined || sessionStorage["_activetotalCount"] == undefined) { }
                else {
                    //$('#nav nav-tabs').find('li').removeAttr('class');
                    var totalTabs = sessionStorage["_activetotalCount"];
                    for (var i = 0; i < totalTabs; i++) {
                        $("#inv_" + (i + 1)).removeClass('active');
                        $("#t_" + (i + 1)).attr('aria-expanded', 'false');
                        $("#doc" + (i + 1) + (i + 1)).removeClass('active in');
                    }

                    $("#" + sessionStorage["_activeMainTabMenu"] + "").addClass('active');
                    $("#" + sessionStorage["_activecontentPageId"] + "").addClass('active in');
                    $("#" + sessionStorage["_activeSubTabMenu"] + "").attr('aria-expanded', 'true');
                }
            });
        });

    </script>

    <script type="text/javascript">
        function md5auth(seed) {
            var username1 = document.getElementById('<%=txtUserName.ClientID %>');
            var password1 = document.getElementById('<%=txtPassowrd.ClientID %>');
            var password = password1.value;

            if (username1.value === '') {
                alert("Please enter valid username"); username1.focus();
                return false;
            }
            if (password1.value === '') {
                alert("Please enter valid password"); password1.focus();
                return false;
            }
            var hash = calcMD5(seed + (calcMD5(password).toUpperCase()));
            password1.value = hash.toUpperCase();
            return true;
        }
    </script>
</asp:Content>

