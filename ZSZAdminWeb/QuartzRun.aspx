<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuartzRun.aspx.cs" Inherits="ZSZAdminWeb.QuartzRun" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="lib/jquery/1.9.1/jquery.min.js"></script>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript">
        $(window).load(function () {
            setInterval("sTimeout()", 1000);
            setInterval("mySelfqq()", 1100000); //这里设置的时间要比IIS中的要短(当前 iis设置为1分钟过期，即50秒也行)
        });

        function mySelfqq() {
            window.location.reload();//重加载
        }
 
 
        function sTimeout() {
            var a = Date().toString();
            $("#lb").html(a);
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            llll<div id = "lb"></div>lllll<br/>
          <%=DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") %>
        </div>
    </form>
</body>
</html>
