
//查询方法
function Select() {
    var tool_DEPTNAME = $('#tool_DEPTNAME').val();
    var tool_STATUSTIME_Q = $('#tool_STATUSTIME_Q').datebox('getValue');
    var tool_STATUSTIME_Z = $('#tool_STATUSTIME_Z').datebox('getValue');
    $('#dg').datagrid('load', {
        tool_DEPTNAME: tool_DEPTNAME,
        tool_STATUSTIME_Q: tool_STATUSTIME_Q,
        tool_STATUSTIME_Z: tool_STATUSTIME_Z
    });
}