$(function () {
    GetResult();
});

function GetResult() {
    var identity = $('#identity').val();
    var columnName = $('#columnName').val();
    var columnValue = $('#columnValue').val();
    $.post('../../Handler/UsersHandler.ashx?action=GetAssist',
        {
            identity: identity,
            columnName: columnName,
            columnValue: columnValue
        },
        function (result) {
            if (result.length >1) {
                var json = JSON.parse(result);
                var str = "根据标识列查询返回数据:<br>" + JSON.stringify(json.dt_identity) + "<br>";
                str += "根据列名查询返回数据:<br>" + JSON.stringify(json.dt_column) + "<br>";
                str += "无条件查询最大的ID:" + json.max + "<br>";
                str += "无条件查询最小的ID:" + json.min + "<br>";
                str += "性别为男的最大ID:" + json.max_condition + "<br>";
                str += "性别为男的最小ID:" + json.min_condition + "<br>";
                $('#div_result').html(str);
            } else {
                $.messager.alert('错误', '后台获取数据发生异常!', 'error');
            }
        });
}  