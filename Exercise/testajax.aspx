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
   /*  $.ajax(
                             {
                                 url: "http://140.116.177.49/ORCS/Exercise/MarkProgramQuestion.aspx",
                                 data: "ProgramPath=C:\\Users\\chiu\\Desktop\\Integrate_Program_comparing_to_Hints\\gradeprogram\\gradeprogram\\HWfile\\資料查詢\\HW1&cQID=tea1_Q_20171123182738&StuCouHWDe_ID=all&questionNum=Q4",
                                 type: "post",
                                 cache: false,
                                 async: false,
                                 dataType:"text",
                                 success: function (data) {
                                     alert(data);
                                 },
                                 error:function(){alert('ajax failed')}
                             });*/
    $.ajax(
                             {
                                 url: "http://140.116.177.49/ORCS/Exercise/CreateFolder.aspx",
                                 data: "cCourseName=sss&HW_Exam_Number=HW1&QuestionNumber=Q4",
                                 type: "post",
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
