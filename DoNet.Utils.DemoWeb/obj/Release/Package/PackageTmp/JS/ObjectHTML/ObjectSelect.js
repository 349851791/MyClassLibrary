
//查询方法
function Select()
{ 
   var  tool_USERNAME = $('#tool_USERNAME').val();
   var  tool_DUTY = $('#tool_DUTY').val();
    
    $('#dg').datagrid('load', { 
        tool_USERNAME: tool_USERNAME,
        tool_DUTY: tool_DUTY
    });  
}