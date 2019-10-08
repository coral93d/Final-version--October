<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="jQueryGoogleChart.aspx.cs" Inherits="jQueryChartApplication.jQueryGoogleChart" %>  
  
<!DOCTYPE html>  
  
<html xmlns="http://www.w3.org/1999/xhtml">  
<head runat="server">  
    <title></title>  
    <script src="http://code.jquery.com/jquery-1.8.2.js"></script>  
    <script src="http://www.google.com/jsapi" type="text/javascript"></script>  
    <script type="text/javascript">           
        google.load('visualization', '1', { packages: ['corechart'] });  
    </script>  
    <script type="text/javascript">  
        $(function () {  
            $.ajax({  
                type: 'POST',  
                dataType: 'json',  
                contentType: 'application/json',  
                url: 'jQueryGoogleChart.aspx/GetChartData',  
                data: '{}',  
                success:  
                function (response) {  
                    drawchart(response.d);  
                },  
  
                error: function () {  
                    alert("Error loading data!");  
                }  
            });  
        })  
        function drawchart(dataValues) {  
            var data = new google.visualization.DataTable();  
            data.addColumn('string', 'Column Name');  
            data.addColumn('number', 'Column Value');  
            for (var i = 0; i < dataValues.length; i++) {  
                data.addRow([dataValues[i].EmployeeCity, dataValues[i].Total]);  
            }  
            new google.visualization.PieChart(document.getElementById('myChartDiv')).  
            draw(data, { title: "Google Chart in Asp.net using jQuery" });  
        }  
    </script>  
</head>  
<body>  
    <form id="form1" runat="server">  
        <div id="myChartDiv" style="width: 700px; height: 450px;">  
        </div>  
    </form>  
    </body>  
</html>  