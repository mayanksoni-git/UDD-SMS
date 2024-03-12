<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="Demo.aspx.cs" Inherits="Demo" %>

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
                                <div id="DivRoot" align="left">

                                    <div style="overflow: hidden;" id="DivHeaderRow">
                                    </div>

                                    <div style="overflow: scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                        <asp:GridView ID="grdPost" runat="server" AutoGenerateColumns="False" CssClass="display table table-bordered" EmptyDataText="No Records Found" OnPreRender="grdPost_PreRender" ShowFooter="true" PageSize="50" OnPageIndexChanging="grdPost_PageIndexChanging" AllowPaging="True">
                                            <Columns>
                                                <asp:BoundField DataField="SNAAccountMaster_Id" HeaderText="SNAAccountMaster_Id">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ProjectWork_Id" HeaderText="ProjectWork_Id">
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
                                                <asp:TemplateField HeaderText="S No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Jurisdiction_Name_Eng" HeaderText="District" />
                                                <asp:BoundField DataField="Zone_Name" HeaderText="Zone" />
                                                <asp:BoundField DataField="Circle_Name" HeaderText="Circle" />
                                                <asp:BoundField DataField="Division_Name" HeaderText="Division" />
                                                <asp:BoundField DataField="ProjectWork_ProjectCode" HeaderText="Project Code" />
                                                <asp:BoundField DataField="ProjectWork_Name" HeaderText="Work" />
                                                <asp:BoundField HeaderText="ACCOUNT NAME" DataField="SNAAccountMaster_ACCT_NAME">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="ACCOUNT NUMBER" DataField="SNAAccountMaster_ACCT_NO">
                                                    <HeaderStyle CssClass="displayStyle" />
                                                    <ItemStyle CssClass="displayStyle" />
                                                    <FooterStyle CssClass="displayStyle" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="No Of Pendency Days" DataField="Max_Pendency" />
                                                <asp:BoundField HeaderText="Date Of Oldest Invoice" DataField="Min_Date" />
                                                <asp:BoundField HeaderText="Days Since Last Limit Assigned" DataField="Last_Assigned_Day_Diff" />
                                                <asp:BoundField HeaderText="Total Limit Assigned" DataField="SNAAccountLimit_AssignedLimit" />
                                                <asp:BoundField HeaderText="Total Limit Used" DataField="SNAAccountLimitUsed_UsedLimit" />
                                                <asp:BoundField HeaderText="Total Available Limit" DataField="SNAAccountAvailableLimit" />
                                                <asp:BoundField HeaderText="Total In Pipeline" DataField="SNAAccountPipelineLimit" />
                                                <asp:BoundField HeaderText="Total In Pipeline (Invoice)" DataField="SNAAccountPipelineLimitInvoice" />
                                                <asp:BoundField HeaderText="Total In Pipeline (Deduction Release)" DataField="SNAAccountPipelineLimitDR" />
                                                <asp:BoundField HeaderText="Total In Pipeline (Other Dept)" DataField="SNAAccountPipelineLimitADP" />
                                                <asp:BoundField HeaderText="Total In Pipeline (Mob Adv)" DataField="SNAAccountPipelineLimitMA" />
                                                <asp:BoundField HeaderText="Available Balance In Bank (PNB)" DataField="SNAAccountMaster_Balance" />
                                            </Columns>
                                            <FooterStyle BackColor="Black" ForeColor="White" Font-Bold="true" />
                                        </asp:GridView>
                                    </div>

                                    <div id="DivFooterRow" style="overflow: hidden">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>



    <script language="javascript" type="text/javascript">
        function MakeStaticHeader(gridId, height, width, headerHeight, isFooter) {
            var tbl = document.getElementById(gridId);
            if (tbl) {
                var DivHR = document.getElementById('DivHeaderRow');
                var DivMC = document.getElementById('DivMainContent');
                var DivFR = document.getElementById('DivFooterRow');

                //*** Set divheaderRow Properties ****
                DivHR.style.height = headerHeight + 'px';
                DivHR.style.width = (parseInt(width) - 16) + 'px';
                DivHR.style.position = 'relative';
                DivHR.style.top = '0px';
                DivHR.style.zIndex = '10';
                DivHR.style.verticalAlign = 'top';

                //*** Set divMainContent Properties ****
                DivMC.style.width = width + 'px';
                DivMC.style.height = height + 'px';
                DivMC.style.position = 'relative';
                DivMC.style.top = -headerHeight + 'px';
                DivMC.style.zIndex = '1';

                //*** Set divFooterRow Properties ****
                DivFR.style.width = (parseInt(width) - 16) + 'px';
                DivFR.style.position = 'relative';
                DivFR.style.top = -headerHeight + 'px';
                DivFR.style.verticalAlign = 'top';
                DivFR.style.paddingtop = '2px';

                if (isFooter) {
                    var tblfr = tbl.cloneNode(true);
                    tblfr.removeChild(tblfr.getElementsByTagName('tbody')[0]);
                    var tblBody = document.createElement('tbody');
                    tblfr.style.width = '100%';
                    tblfr.cellSpacing = "0";
                    tblfr.border = "0px";
                    tblfr.rules = "none";
                    //*****In the case of Footer Row *******
                    tblBody.appendChild(tbl.rows[tbl.rows.length - 1]);
                    tblfr.appendChild(tblBody);
                    DivFR.appendChild(tblfr);
                }
                //****
                DivHR.appendChild(tbl.cloneNode(true));
            }
        }



        function OnScrollDiv(Scrollablediv) {
            document.getElementById('DivHeaderRow').scrollLeft = Scrollablediv.scrollLeft;
            document.getElementById('DivFooterRow').scrollLeft = Scrollablediv.scrollLeft;
        }

    </script>
</asp:Content>

