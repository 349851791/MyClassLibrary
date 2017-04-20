var url;

//初始化编辑窗体
$(function () {
    $("#win_user").window({
        title: '增加用户',
        width: 540,
        height: 200,
        left: 150,
        top: 30,
        href: '../../WebForms/ObjectHTML/UserEdit.html'
    });
});

//设置增加功能url,并打开窗口
function Insert_open() {
    url = '../../Handler/UsersHandler.ashx?action=Insert';
    $("#win_user").window({
        title: '增加用户',
        onLoad: function () {
            //Reset();
            //加载时,给所有input增加样式.并增加鼠标事件,在事件中修改样式
            $("input[type='text']", $("#table_user")).each(function () {
                if (this.className.indexOf('text') == -1 && this.className.indexOf('easyui') == -1) {
                    $(this).mouseover(function () {
                        $(this).addClass("myInputMouseOver").removeClass("myInputMouseOut");
                    }).mouseout(function () {
                        $(this).addClass("myInputMouseOut").removeClass("myInputMouseOver");
                    }).addClass("myInput");
                }
            });
            //$("input[type='text']", $("#table_user")).addClass("myInput");
            //$("input[type='text']", $("#table_user")).each(function () {
            //    $(this).mouseover(function () {
            //        //判断是否是easyui的控件,如果不是则添加事件以及样式
            //        if (this.className.indexOf('text') == -1 && this.className.indexOf('easyui') == -1) {
            //            //$(this).css({ height: "18px",  border: "1px solid #95b8e7" })
            //            $(this).addClass("myInputMouseOver").removeClass("myInputMouseOut")
            //        }
            //    }).mouseout(function () {
            //        if (this.className.indexOf('text') == -1 && this.className.indexOf('easyui') == -1) {
            //            //$(this).css({ height: "20px", border: "0px" })
            //            $(this).addClass("myInputMouseOut").removeClass("myInputMouseOver");
            //        }
            //    });
            //});
        }
    });
    $('#win_user').window('open');
}

//设置编辑功能url,并打开窗口
function Update_open() {
    url = '../../Handler/UsersHandler.ashx?action=Update';
    var row = $('#dg').datagrid('getSelected');
    $("#win_user").window({
        title: '修改用户',
        onLoad: function () {
            //加载时,给所有input增加样式.并增加鼠标事件,在事件中修改样式 
            $("input[type='text']", $("#table_user")).each(function () {
                if (this.className.indexOf('text') == -1 && this.className.indexOf('easyui') == -1) {
                    $(this).mouseover(function () {
                        $(this).addClass("myInputMouseOver").removeClass("myInputMouseOut");
                    }).mouseout(function () {
                        $(this).addClass("myInputMouseOut").removeClass("myInputMouseOver");
                    }).addClass("myInput");
                }
            });  
            $('#myForm').form('load', row);//获取窗口数据 
        }
    });
    if (row) {
        $('#win_user').window('open');
    }
    else {
        $.messager.alert('提示', '请选择数据!', 'info');
    }
}


//提交按钮
function SubmitData() {
    $.messager.progress();
    $('#myForm').form('submit', {
        url: url,
        success: function (result) {
            if (result == '1') {
                $.messager.alert('提示', '提交成功!');
                $('#dg').datagrid('reload');
                win_close();
            } else {
                $.messager.alert('提示', '失败,请重新提交!');
            }
        }
    });
    $.messager.progress('close');
}

//删除
function Delete() {
    var row = $('#dg').datagrid("getSelected");
    if (row == null) {
        $.messager.alert('提示', '请选择数据!', 'info');
    }
    else {
        $.messager.confirm('提示', '您确认想要删除记录吗?', function (r) {
            if (r) {
                $.post('../../Handler/UsersHandler.ashx?action=Delete', { ID: row.ID }, function (result) {
                    if (result == '1') {
                        $('#dg').datagrid('reload');
                    } else {
                        $.messager.alert('错误', '失败,请重新提交!', 'error');
                    }
                });
            }
        });
    };
}


function Reset() {
    $("input[type=text]", $("#table_user")).each(function () {
        if (this.id != "") {
            $(this).val();//赋值
        }
    });
}

function win_close() {
    $('#win_user').window('close');
}
