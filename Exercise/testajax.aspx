<%@ Page Language="C#" AutoEventWireup="true" CodeFile="testajax.aspx.cs" Inherits="Exercise_testajax" %>

<!DOCTYPE html>
<script src="../Scripts/jquery-1.11.2.min.js"></script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
<script>
    $.ajax(
                             {
                                 url: "MarkProgramQuestion.aspx",
                                 data: "{'ProgramPath':'dasd','cQID':'sdad','StuCouHWDe_ID':'all','questionNum':'Q4'}",
                                 type: "POST",
                                 cache: false,
                                 async: false,
                                 dataType:"text",
                                 success: function (data) {
                                     alert(data);
                                 },
                                 error:function(){alert('ajax failed')}
                             });

</script>
</html>
