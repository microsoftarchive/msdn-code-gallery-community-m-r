<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="Proyecto1._0._Default" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Proyecto 1.0</title>
    <script src="Plugin/menu/stuHover.js" type="text/javascript"></script>
    <script src="Plugin/javascript/jProyecto.js" type="text/javascript"></script>
    <script src="Plugin/javascript/jquery.js" type="text/javascript"></script>
    <script src="Plugin/javascript/modernizr-v2.7.1.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="Plugin/menu/pro_drop_1.css" />
    <style type="text/css">
        html, body
        {
            height: 100%;
            overflow: auto;
        }

        body
        {
            padding: 0;
            margin: 0;
        }
    </style>
    <script>
        Menu();
    </script>
</head>


<body>
    <div style="position:fixed; top: 35px; left: 25px;">
        <ul id="nav">
        </ul>
    </div>
    <div style="width: 100%; height: 100%;">
        <div>
            <iframe id="top" name="top" style="height: 80px; width: 100%;" src="Principal/Header.aspx" frameborder="0" scrolling="no"></iframe>
        </div>
        <div style="display: table; height:87%; width: 100%;">
            <iframe id="content" name="content" src="Principal/Dummy.aspx" style="height: 100%; width: 100%;" frameborder="0"></iframe>
        </div>
    </div>


</body>

<%--<frameset rows="80,1*,25" frameborder="no" framespacing="0">

    <frame src="Principal/Header.aspx" name="top" scrolling="NO" noresize marginwidth="0" marginheight="0" >
    <frame src="Principal/Dummy.aspx" name="contenido" >
</frameset>--%>
</html>
