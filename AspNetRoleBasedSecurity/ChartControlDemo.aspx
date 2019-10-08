<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChartControlDemo.aspx.cs" Inherits="ChartControlApplication.ChartControlDemo" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Showing Employee Information in Chart</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Chart ID="EmployeeChartInfo" runat="server" Height="450px" Width="550px">
                <Titles>
                    <asp:Title ShadowOffset="3" Name="Items" />
                </Titles>
                <Legends>
                    <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default" LegendStyle="Row" />
                </Legends>
                <Series>
                    <asp:Series Name="Default" />
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1" BorderWidth="1" />
                </ChartAreas>
            </asp:Chart>
        </div>
    </form>
</body>
</html>
