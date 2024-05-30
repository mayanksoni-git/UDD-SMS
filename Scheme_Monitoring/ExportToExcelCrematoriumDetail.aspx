<%@ Page Language="C#" MasterPageFile="~/TemplateMasterAdmin.master" AutoEventWireup="true" CodeFile="ExportToExcelCrematoriumDetail.aspx.cs" Inherits="ExportToExcelCrematoriumDetail" EnableEventValidation="false" ValidateRequest="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <%--Same grid is used on Page RptCrematoriumDetail.aspx PrintCrematoriumDetail.aspx page also, please change there as well if needed--%>
    <asp:GridView ID="gvCrematoriumDetail" runat="server" CssClass="display table table-bordered" AutoGenerateColumns="False" EmptyDataText="No Records Found">
        <Columns>
            <asp:TemplateField HeaderText="Sr. No.">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Zone" DataField="ZoneName" />
            <asp:BoundField HeaderText="District" DataField="CircleName" />
            <asp:BoundField HeaderText="ULB" DataField="DivisionName" />
            <asp:BoundField HeaderText="Year" DataField="Year" />
            <asp:BoundField HeaderText="Month" DataField="MonthName" />
            <asp:BoundField HeaderText="Name of Crematorium" DataField="NameCMTR" />

            <asp:BoundField HeaderText="Location of Crematorium" DataField="LocationCMTR" />
            <%--<asp:BoundField HeaderText="No of Pyres in Crematorium" DataField="NoOfPyres" />--%>
             <asp:BoundField HeaderText="Conventional + Improvised Wood + Gas + Electric =Total No of Pyres in Crematorium" DataField="PyresDetail" />

            <asp:TemplateField HeaderText="Drinking Water Facility">
                <ItemTemplate>
                    <%# Convert.ToBoolean(Eval("DrinkingWater")) ? "Yes" : "No" %>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Is the facility connected to Electricity Grid">
                <ItemTemplate>
                    <%# Convert.ToBoolean(Eval("ElecticityGrid")) ? "Yes" : "No" %>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Parking Space">
                <ItemTemplate>
                    <%# Convert.ToBoolean(Eval("Parking")) ? "Yes" : "No" %>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Shed for pyres">
                <ItemTemplate>
                    <%# Convert.ToBoolean(Eval("Shed")) ? "Yes" : "No" %>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Hearse">
                <ItemTemplate>
                    <%# Convert.ToBoolean(Eval("Hearse")) ? "Yes" : "No" %>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Hand Pump">
                <ItemTemplate>
                    <%# Convert.ToBoolean(Eval("HandPump")) ? "Yes" : "No" %>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Boundary Wall">
                <ItemTemplate>
                    <%# Convert.ToBoolean(Eval("BoundaryWall")) ? "Yes" : "No" %>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Waiting/Prayer Hall">
                <ItemTemplate>
                    <%# Convert.ToBoolean(Eval("PrayerHall")) ? "Yes" : "No" %>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Office/Care Taker Room">
                <ItemTemplate>
                    <%# Convert.ToBoolean(Eval("CareTakerRoom")) ? "Yes" : "No" %>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Ash Storage Rack">
                <ItemTemplate>
                    <%# Convert.ToBoolean(Eval("AshStorage")) ? "Yes" : "No" %>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Bathroom for Bathing">
                <ItemTemplate>
                    <%# Convert.ToBoolean(Eval("Bathrooms")) ? "Yes" : "No" %>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Is Washroom facility available ?">
                <ItemTemplate>
                    <%# Convert.ToBoolean(Eval("Washroom")) ? "Yes" : "No" %>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Does Registration Happens ?">
                <ItemTemplate>
                    <%# 
                        Eval("Registration").ToString() == "1" ? "Yes" : 
                        Eval("Registration").ToString() == "2" ? "Other" : 
                        Eval("Registration").ToString() == "0" ? "No" : "Unknown"
                    %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Name of Filler" DataField="FillerName" />
            <asp:BoundField HeaderText="Contact No of Filler" DataField="FillerContact" />
            <asp:BoundField HeaderText="Total no. of Cremations done in from 1/Jan/2023 - 31/Dec/2023 " DataField="TotalCMTRDone" />
        </Columns>
    </asp:GridView>
</asp:Content>
